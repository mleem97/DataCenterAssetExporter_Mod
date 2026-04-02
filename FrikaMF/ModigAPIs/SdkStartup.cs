using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using MelonLoader;

namespace DataCenter.ModigAPIs;

internal static class SdkStartup
{
    [SuppressMessage("Usage", "CA2255", Justification = "Intentional startup hook for SDK load-order visibility.")]
    [ModuleInitializer]
    internal static void OnSdkAssemblyLoaded()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";
        MelonLogger.Msg($"[DataCenter.ModigAPIs] SDK loaded (v{version}) - ready before consumer mod initialization.");
    }
}