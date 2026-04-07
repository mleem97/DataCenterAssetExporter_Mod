using System;
using System.IO;
using System.Threading;
using MelonLoader;
using MelonLoader.Utils;

[assembly: MelonInfo(typeof(ModPathRedirector.ModPathRedirectorMod), "ModPathRedirector", "1.5.1", "DataCenterExporter")]
[assembly: MelonGame("Waseku", "Data Center")]
[assembly: MelonPriority(-10000)]

namespace ModPathRedirector;

/// <summary>
/// Runs with high priority so <see cref="OnPreModsLoaded"/> executes before other plugins.
/// After Il2Cpp assembly generation (see Latest.log: Il2CppAssemblyGenerator), blocks MelonMods load until
/// each subscribed Workshop item is downloaded and present under
/// <c>{GameRoot}/{ExeName}_Data/StreamingAssets/mods/workshop_&lt;id&gt;/WorkshopUploadContent</c>
/// (falls back to <c>workshop_&lt;id&gt;</c> alone if the nested folder is not created yet).
/// </summary>
public sealed class ModPathRedirectorMod : MelonPlugin
{
	private const int SteamInitWaitMs = 90_000;
	private const int WorkshopSyncMaxWaitMs = 600_000;
	private const int FolderWaitAfterSteamMs = 180_000;
	private const int PollMs = 100;
	private const int ProgressLogIntervalMs = 10_000;

	public override void OnPreModsLoaded()
	{
		LoggerInstance.Msg(
			"ModPathRedirector: After Il2Cpp assembly step — waiting for Workshop content before MelonMods load.");

		if (!WaitForSteamUgc(SteamInitWaitMs))
		{
			LoggerInstance.Warning(
				"ModPathRedirector: Steam UGC not available; continuing without Workshop wait (start Steam & the game from Steam).");
			return;
		}

		if (SteamFlatUgc.FailedResolve)
		{
			LoggerInstance.Warning("ModPathRedirector: ISteamUGC not resolved; skipping Workshop wait.");
			return;
		}

		WaitForSubscribedWorkshopOnDisk();
	}

	private bool WaitForSteamUgc(int maxMs)
	{
		var deadline = DateTime.UtcNow.AddMilliseconds(maxMs);
		while (DateTime.UtcNow < deadline)
		{
			SteamFlatUgc.RunCallbacks();
			if (SteamFlatUgc.TryEnsureUgc(out var steamOk) && steamOk)
				return true;

			if (SteamFlatUgc.FailedResolve)
				return false;

			Thread.Sleep(PollMs);
		}

		return SteamFlatUgc.TryEnsureUgc(out _);
	}

	private void WaitForSubscribedWorkshopOnDisk()
	{
		var count = SteamFlatUgc.GetNumSubscribedItems();
		if (count == 0)
		{
			LoggerInstance.Msg("ModPathRedirector: No subscribed Workshop items; loading MelonMods.");
			return;
		}

		var items = new ulong[count];
		var filled = SteamFlatUgc.GetSubscribedItems(items, count);

		RequestDownloadsPerItem(items, filled);

		var modsRoot = GetStreamingModsRoot();
		var deadline = DateTime.UtcNow.AddMilliseconds(WorkshopSyncMaxWaitMs);
		var lastLog = DateTime.UtcNow;
		DateTime? allSteamReadySince = null;

		while (DateTime.UtcNow < deadline)
		{
			SteamFlatUgc.RunCallbacks();

			if (!SteamFlatUgc.TryEnsureUgc(out var ok) || !ok)
			{
				Thread.Sleep(PollMs);
				continue;
			}

			var allSteam = true;
			for (var i = 0; i < (int)filled; i++)
			{
				if (!IsSteamItemReady(items[i]))
				{
					allSteam = false;
					break;
				}
			}

			if (!allSteam)
			{
				allSteamReadySince = null;
				if ((DateTime.UtcNow - lastLog).TotalMilliseconds >= ProgressLogIntervalMs)
				{
					LogPendingItems(items, filled, modsRoot);
					lastLog = DateTime.UtcNow;
				}

				Thread.Sleep(PollMs);
				continue;
			}

			if (allSteamReadySince == null)
				allSteamReadySince = DateTime.UtcNow;

			if (AllWorkshopFoldersExist(items, filled, modsRoot))
			{
				LoggerInstance.Msg(
					$"ModPathRedirector: All {filled} Workshop item(s) are installed (Steam) and workshop_* content exists under StreamingAssets/mods. Loading MelonMods.");
				return;
			}

			// Steam client finished, but game may copy to StreamingAssets slightly later
			if ((DateTime.UtcNow - allSteamReadySince.Value).TotalMilliseconds >= FolderWaitAfterSteamMs)
			{
				LoggerInstance.Warning(
					"ModPathRedirector: Steam reports all items installed, but workshop_* folders are still missing under StreamingAssets/mods. " +
					"Continuing MelonMods load — if mods are missing, restart once the game has synced Workshop content.");
				return;
			}

			if ((DateTime.UtcNow - lastLog).TotalMilliseconds >= ProgressLogIntervalMs)
			{
				LoggerInstance.Msg($"ModPathRedirector: Waiting for workshop_*/WorkshopUploadContent under: {modsRoot}");
				lastLog = DateTime.UtcNow;
			}

			Thread.Sleep(PollMs);
		}

		LoggerInstance.Warning(
			"ModPathRedirector: Timed out waiting for Workshop downloads (Steam). " +
			"MelonMods will load anyway — let Steam finish, then restart.");
	}

	private static bool AllWorkshopFoldersExist(ulong[] items, uint filled, string modsRoot)
	{
		for (var i = 0; i < (int)filled; i++)
		{
			if (!WorkshopItemPresentOnDisk(modsRoot, items[i]))
				return false;
		}

		return true;
	}

	/// <summary>
	/// Game syncs Workshop content under <c>StreamingAssets/mods/workshop_&lt;id&gt;/WorkshopUploadContent</c>.
	/// Accept <c>workshop_&lt;id&gt;</c> alone for older layouts or before nested content appears.
	/// </summary>
	private static bool WorkshopItemPresentOnDisk(string modsRoot, ulong id)
	{
		var workshopDir = Path.Combine(modsRoot, "workshop_" + id);
		var uploadContent = Path.Combine(workshopDir, "WorkshopUploadContent");
		return Directory.Exists(uploadContent) || Directory.Exists(workshopDir);
	}

	private static string GetStreamingModsRoot()
	{
		var root = MelonEnvironment.GameRootDirectory;
		var exe = MelonEnvironment.GameExecutableName;
		if (string.IsNullOrEmpty(exe))
			exe = "Data Center";

		return Path.Combine(root, exe + "_Data", "StreamingAssets", "mods");
	}

	private static void RequestDownloadsPerItem(ulong[] items, uint filled)
	{
		for (var i = 0; i < (int)filled; i++)
		{
			var id = items[i];
			var state = SteamFlatUgc.GetItemState(id);
			var installed = (state & SteamFlatUgc.ItemState.Installed) != 0;
			var downloading = (state & SteamFlatUgc.ItemState.Downloading) != 0;
			var downloadPending = (state & SteamFlatUgc.ItemState.DownloadPending) != 0;
			var needsUpdate = (state & SteamFlatUgc.ItemState.NeedsUpdate) != 0;

			if (installed && !needsUpdate)
				continue;

			if (!downloading && !downloadPending)
			{
				if (SteamFlatUgc.DownloadItem(id, true))
					MelonLogger.Msg($"ModPathRedirector: Requested download for workshop_{id}");
			}
			else
			{
				MelonLogger.Msg($"ModPathRedirector: workshop_{id} already downloading / pending.");
			}
		}
	}

	private static bool IsSteamItemReady(ulong id)
	{
		var state = SteamFlatUgc.GetItemState(id);
		var installed = (state & SteamFlatUgc.ItemState.Installed) != 0;
		var downloading = (state & SteamFlatUgc.ItemState.Downloading) != 0;
		var downloadPending = (state & SteamFlatUgc.ItemState.DownloadPending) != 0;
		var needsUpdate = (state & SteamFlatUgc.ItemState.NeedsUpdate) != 0;
		return installed && !needsUpdate && !downloading && !downloadPending;
	}

	private void LogPendingItems(ulong[] items, uint filled, string modsRoot)
	{
		for (var i = 0; i < (int)filled; i++)
		{
			var id = items[i];
			var state = SteamFlatUgc.GetItemState(id);
			var workshopDir = Path.Combine(modsRoot, "workshop_" + id);
			var uploadContent = Path.Combine(workshopDir, "WorkshopUploadContent");
			LoggerInstance.Msg(
				$"  workshop_{id}: state=0x{state:X}  workshopDir={Directory.Exists(workshopDir)}  WorkshopUploadContent={Directory.Exists(uploadContent)}");
		}
	}
}
