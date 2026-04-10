# FrikaModFramework (target layout)

This folder is the **single source of truth** for the planned monorepo layout from the workspace plan:

- **`fmf_hooks.json`** — declarative hook registry (`FMF.DOMAIN.Event` — see `CONTRIBUTING.md`).
- **`src/core/`** — domain-based naming helpers (`FmfDomains`, `FmfHookName`), wired into the existing C# project [`framework/FrikaMF.csproj`](../framework/FrikaMF.csproj) via MSBuild.
- **`src/bindings/`** — placeholder for future language bindings (Lua, Rust, Python, TypeScript, …).
- **`src/game2framework/`** — placeholder for the legacy compatibility layer (Game2Framework → FMF).
- **`docs/wiki/`** — optional **source stubs**; the public Docusaurus site remains at the repository root under [`wiki/`](../wiki/) with content in [`docs/`](../docs/).

## Existing paths (not yet moved)

| Area | Current location in repo |
|------|--------------------------|
| MelonLoader framework (C#) | `framework/` |
| Mods | `mods/` (e.g. HexLabel: `mods/FMF.Mod.HexLabelMod/`) |
| Plugins | `plugins/` |
| Docusaurus app | `wiki/` |
| Wiki Markdown/MDX | `docs/` |

A full physical move of `framework/` → `FrikaModFramework/src/core/` would change solution, CI, and paths substantially; it was intentionally **not** done in one step. See [`CONTRIBUTING.md`](../CONTRIBUTING.md) and [`Refactoring.md`](../Refactoring.md).
