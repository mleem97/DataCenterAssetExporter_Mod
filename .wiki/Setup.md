# Setup

This page describes the current local setup for `FrikaModFramework`.

## Requirements

- `Data Center` installed
- `MelonLoader` installed for the game
- `.NET 6 SDK`
- Optional: decompiled sources in `il2cpp-unpack`

Before building the framework for the first time, start the game once with MelonLoader so required generated assemblies and runtime metadata are present.

## Repository Structure (current)

- `FrikaMF.csproj` and `FrikaMF.sln` for the framework mod
- `.wiki/` for project wiki pages

## Build Commands

Use PowerShell from repository root:

```powershell
dotnet build .\FrikaMF.csproj -c Debug
dotnet build .\FrikaMF.csproj -c Release
```

Preferred command shells by platform:

- Windows: `PowerShell` (preferred), `cmd` (supported)
- Linux: `bash` / `sh` (preferred)

Optional package-run wrappers (only if `npm` or `pnpm` is installed):

```powershell
# PowerShell / cmd
npm run wiki:sync
pnpm wiki:sync

npm run release:prepare:minor
pnpm release:prepare:minor
```

```bash
# bash / sh
npm run wiki:sync
pnpm wiki:sync

npm run release:prepare:minor
pnpm release:prepare:minor
```

Direct script execution remains the default recommendation for maintainers.

## GitHub Runner Strategy (DLL builds)

The repository workflows are configured so DLL builds can run on a local self-hosted runner with game-specific resources.

- Preferred runner labels for build jobs:
- `self-hosted`
- `windows`
- `x64`
- Workflow behavior:
- `.github/workflows/dotnet-ci.yml`: uses self-hosted runner by default.
- `.github/workflows/rustbridge-sync.yml`: uses self-hosted runner by default.
- Both workflows provide manual `workflow_dispatch` fallback to hosted Windows via `useHostedRunner=true`.

Recommended for local runner setup:

```powershell
# run in your self-hosted runner working copy
dotnet --info
dotnet restore .\FrikaMF.csproj
dotnet build .\FrikaMF.csproj -c Release -nologo
```

Optional deploy helper:

```powershell
. .\scripts\Invoke-DataCenterModDeploy.ps1
Invoke-Deploy --1   # framework only
Invoke-Deploy --2   # HexLabelMod only
Invoke-Deploy --all # both
```

Local release upload (maintainer flow):

```powershell
. .\scripts\Publish-LocalRelease.ps1
$env:GITHUB_TOKEN = "<github_token_with_repo_scope>"
Publish-LocalRelease -Tag "vX.Y.Z"
```

## Runtime Placement

- Copy framework output (`FrikaModdingFramework.dll`) into game `Mods` folder
- Copy `HexLabelMod.dll` into game `Mods` folder

## Rust Mod Placement (separate from C# Mods)

- Rust/native plugins go to: `Data Center/Mods/RustMods`
- C# mods stay in: `Data Center/Mods`

## Game-Object Content Placement (no extra mod required)

To add custom game objects that are grouped with their assets/config:

- Use: `Data Center/Data Center_Data/StreamingAssets/Mods/<YourPack>`
- Keep files of one object pack together in one folder.

### Scaffold command

```powershell
pwsh -ExecutionPolicy Bypass -File .\scripts\New-StreamingAssetModPack.ps1 -GamePath "C:\Program Files (x86)\Steam\steamapps\common\Data Center" -ModName "MyServerPack"
```

This creates:

- `config.json`
- `model.obj`
- `model.mtl`
- `texture.png`
- `icon.png`

Then replace placeholders with real assets and align `config.json` with current `ExampleMod` schema.

## Notes

- `HexLabelMod` is configured for release-only builds.
- Keep game paths in `.csproj` aligned with your local Steam installation.
- Validate load state through `MelonLoader/Latest.log`.
