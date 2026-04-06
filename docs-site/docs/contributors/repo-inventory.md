---
id: repo-inventory
title: Repository inventory
sidebar_label: Repo inventory
description: Current monorepo layout, projects, and known solution drift (contributors).
---

# Repository inventory

This page is the **source of truth snapshot** for how the DataCenterExporter / gregFramework monorepo is organized today. Use it before large refactors or when onboarding.

## Top-level areas

| Area | Path | Role |
|------|------|------|
| Framework core | [`FrikaMF.csproj`](https://github.com/mleem97/gregFramework/blob/master/FrikaMF.csproj) (root) | MelonLoader mod hosting runtime hooks, Harmony, bridge, events |
| Workshop tooling | [`WorkshopUploader/`](https://github.com/mleem97/gregFramework/tree/master/WorkshopUploader) | Steam Workshop upload helper (local/CI) |
| Mods & plugins (sources) | [`ModsAndPlugins/`](https://github.com/mleem97/gregFramework/tree/master/ModsAndPlugins) | Standalone mods and FFM plugins (HexLabel, LangCompat, etc.) |
| Templates | [`Templates/`](https://github.com/mleem97/gregFramework/tree/master/Templates) | Scaffolds for new mods/plugins |
| Documentation site | [`docs-site/`](https://github.com/mleem97/gregFramework/tree/master/docs-site) | Docusaurus wiki, landing, `/mods` catalog |
| Scripts | [`scripts/`](https://github.com/mleem97/gregFramework/tree/master/scripts) | Release metadata, changelog (e.g. `Update-ReleaseMetadata.ps1`) |
| Wiki import (legacy) | [`docs-site/docs/wiki-import/`](./../wiki-import/Home.md) | Imported `.wiki` content; still linked from many pages |

## .NET projects on disk (`*.csproj`)

| Project | Location | In `FrikaMF.sln`? |
|---------|----------|-------------------|
| FrikaMF | `./FrikaMF.csproj` | Yes |
| WorkshopUploader | `WorkshopUploader/WorkshopUploader.csproj` | Yes |
| FFM.Plugin.* (x5) | `ModsAndPlugins/FFM.Plugin.*/` | **No** — solution still references **missing** `StandaloneMods/FFM.Plugin.*/*.csproj` |
| FMF.HexLabelMod | `ModsAndPlugins/FMF.Mod.HexLabelMod/` | No |
| FMF.ConsoleInputGuard | `ModsAndPlugins/FMF.ConsoleInputGuard/` | No |
| FMF.GregifyEmployees | `ModsAndPlugins/FMF.Mod.GregifyEmployees/` | No |
| FMF.JoniMLCompatMod | `ModsAndPlugins/FMF.Plugin.LangCompatBridge/` | No |
| Templates | `Templates/FMF.*`, `Templates/StandaloneModTemplate/` | No |

## Build status (framework project)

- `dotnet build FrikaMF.csproj` may **fail** on a clean machine because the SDK default glob picks up **`WorkshopUploader/`** WinForms sources next to the project file (missing `UseWindowsForms` / desktop targeting in that layout). CI or a future refactor should either exclude that folder from `FrikaMF.csproj` or build `WorkshopUploader` as its own project only.
- `dotnet build FrikaMF.sln` additionally fails while solution entries point at missing `StandaloneMods\FFM.Plugin.*` paths (see above).

## `FrikaMF.sln` drift (action items)

1. **Broken project paths**: The solution references `StandaloneMods\FFM.Plugin.*\` but the repo currently holds matching projects under **`ModsAndPlugins/`**. Either:
   - Re-point solution entries to `ModsAndPlugins\...`, or
   - Restore `StandaloneMods/` layout and move projects (see [Monorepo target layout](./monorepo-target-layout.md)).

2. **Templates in `FrikaMF.csproj`**: Template sources under `Templates/` may fail `dotnet build FrikaMF.csproj` with `CS0122` if `Core` visibility does not match template expectations — treat templates as **samples** until the project graph is cleaned up.

3. **Full solution build**: `dotnet build FrikaMF.sln` fails until (1) is fixed.

## Documentation (Docusaurus)

- **Entry**: `/wiki` → [`intro`](../intro.md)
- **Sidebar**: [`sidebars.js`](https://github.com/mleem97/gregFramework/blob/master/docs-site/sidebars.js)
- **Module catalog** (downloads table): [`docs-site/src/data/moduleCatalog.ts`](https://github.com/mleem97/gregFramework/blob/master/docs-site/src/data/moduleCatalog.ts)
- **Landing**: `/` → [`src/pages/index.tsx`](https://github.com/mleem97/gregFramework/blob/master/docs-site/src/pages/index.tsx)
- **Static catalog page**: `/mods`

## Hook / event sources of truth (code)

- String constants: [`FrikaMF/HookNames.cs`](https://github.com/mleem97/gregFramework/blob/master/FrikaMF/HookNames.cs) (`FFM.*` hook IDs today).
- Numeric IDs: [`FrikaMF/EventIds.cs`](https://github.com/mleem97/gregFramework/blob/master/FrikaMF/EventIds.cs).
- Generated wiki mirror: run [`tools/Generate-FmfHookCatalog.ps1`](https://github.com/mleem97/gregFramework/blob/master/tools/Generate-FmfHookCatalog.ps1) → [`fmf-hooks-catalog`](../reference/fmf-hooks-catalog.md).

## Related

- [Monorepo target layout](./monorepo-target-layout.md) — phased folder goals
- [FMF hook naming](../reference/fmf-hook-naming.md) — naming convention
- [Release channels](../reference/release-channels.md) — Steam vs GitHub beta
