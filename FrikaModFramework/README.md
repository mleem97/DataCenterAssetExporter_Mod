# FrikaModFramework (Zielstruktur)

Dieser Ordner ist die **Single Source of Truth** für die geplante Monorepo-Gliederung aus dem Workspace-Plan:

- **`fmf_hooks.json`** — deklarative Hook-Registry (`FMF.DOMAIN.Event` — siehe `CONTRIBUTING.md`).
- **`src/core/`** — domänenbasierte Namens-Helfer (`FmfDomains`, `FmfHookName`), per MSBuild in das bestehende C#-Projekt [`framework/FrikaMF.csproj`](../framework/FrikaMF.csproj) eingebunden.
- **`src/bindings/`** — Platzhalter für spätere Sprach-Bindings (Lua, Rust, Python, TypeScript, …).
- **`src/game2framework/`** — Platzhalter für die Legacy-Kompatibilitätsschicht (Game2Framework → FMF).
- **`docs/wiki/`** — optionale **Quell-Stubs**; die öffentliche Docusaurus-Site liegt weiterhin im Repo-Root unter [`wiki/`](../wiki/) mit Inhalten in [`docs/`](../docs/).

## Bestehende Pfade (noch nicht verschoben)

| Bereich | Aktueller Ort im Repo |
|--------|------------------------|
| MelonLoader-Framework (C#) | `framework/` |
| Mods | `mods/` (z. B. HexLabel: `mods/FMF.Mod.HexLabelMod/`) |
| Plugins | `plugins/` |
| Docusaurus-App | `wiki/` |
| Wiki-Markdown/MDX | `docs/` |

Eine vollständige physische Verschiebung von `framework/` → `FrikaModFramework/src/core/` würde Solution, CI und Pfade massiv ändern; sie ist absichtlich **nicht** in einem Schritt erfolgt. Siehe [`CONTRIBUTING.md`](../CONTRIBUTING.md) und [`Refactoring.md`](../Refactoring.md).
