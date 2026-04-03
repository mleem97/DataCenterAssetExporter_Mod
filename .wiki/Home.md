# Start Page

Welcome to the project wiki.

## Overview

- Purpose: Practical documentation for building and extending the framework.
- Repository: `mleem97/FrikaModFramework`
- Core runtime bridge code: `FrikaMF/JoniMF/`
- Native game-object content root: `Data Center_Data/StreamingAssets/Mods`

## Suggested Next Pages

- `Setup.md` for local environment setup
- `Architecture.md` for project structure
- `Modding-Guide.md` for workflow and examples

## How to Help

- Add/validate hook candidates in `FrikaMF/JoniMF/HarmonyPatches.cs`.
- Extend event IDs and typed payload dispatch.
- Improve docs and examples for first-time mod developers.
- Report reproducible issues with logs from `MelonLoader/Latest.log`.

## Rust Plugin Reference

- `https://github.com/Joniii11/DataCenter-RustBridge`

## Notes

- Keep pages concise and practical.
- Link related pages to improve navigation.

## Wiki Sync Automation

- Changes in `.wiki/**` on `master` are auto-synced to the GitHub wiki via workflow.
- Manual sync is still available and preferred for controlled maintenance:
- PowerShell: `pwsh -ExecutionPolicy Bypass -File .\scripts\Sync-Wiki.ps1`
- cmd: `powershell -ExecutionPolicy Bypass -File .\scripts\Sync-Wiki.ps1`
- bash/sh: `pwsh -ExecutionPolicy Bypass -File ./scripts/Sync-Wiki.ps1`

- Optional wrappers if package managers are installed:
- `npm run wiki:sync`
- `pnpm wiki:sync`
