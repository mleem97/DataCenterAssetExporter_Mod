# Documentation layout (`docs/`)

This folder is the **single source of truth** for the public Docusaurus site. The app lives in [`wiki/`](../wiki/); built pages are served under the **`/wiki`** base path.

## How content is organized

| Area | Path | Purpose |
|------|------|--------|
| **Landing** | [`intro.md`](./intro.md) | Site home (`/wiki/docs`). |
| **Curated topics** | [`topics/`](./topics/) | Hubs: **Rollen** (Spieler, Moddevs, Contributor, Sponsoren → [`audiences/overview`](./topics/audiences/overview.md)), Roadmap, Security, Wiki-Import. |
| **Mods & plugins** | [`mods/`](./mods/) | Framework, plugin wiki, mod wiki, standalone index. |
| **Releases** | [`releases/`](./releases/) | Per-artifact release notes. |
| **Reference** | [`reference/`](./reference/) | Hooks, naming, MCP, generated catalogs. |
| **Contributors** | [`contributors/`](./contributors/) | Repo layout, Docusaurus workflow, design system. |
| **Audiences** | [`audiences/`](./audiences/) | Newbies / intermediates / professionals. |
| **Roadmap** | [`roadmap/`](./roadmap/) | Planning docs. |
| **Meta** | [`meta/`](./meta/) | Workshop, devserver, backlog. |
| **Monorepo wiki stubs** | [`wiki/`](./wiki/) | Short pages tied to the target repo layout (e.g. Hexmod). **Not** the GitHub Wiki import. |
| **Legacy GitHub Wiki import** | [`wiki-import/`](./wiki-import/) | Bulk import from `.wiki/` (see below). |

## Legacy GitHub Wiki (`docs/wiki-import/`)

Long-form pages that originally lived in the **GitHub Wiki** are mirrored here so they are searchable and versioned with the repo.

1. **Clone or update** the wiki working tree at the repo root as **`.wiki/`** (separate clone: `https://github.com/<org>/<repo>.wiki.git`).
2. From **`wiki/`**, refresh the mirror and split locales:
   - `npm run wiki:refresh`  
   - Or stepwise: `npm run wiki:sync` → `npm run wiki:normalize-i18n` (optional `--dry-run` first).
3. **German** translations for paired pages live under `wiki/i18n/de/docusaurus-plugin-content-docs/current/wiki-import/` after normalization.
4. **New curated docs** should usually be added as normal Markdown under `docs/` (topics, reference, mods), not only under `wiki-import/`, so they stay easy to find. Use `wiki-import/` for bulk legacy material and incremental fixes; migrate important pages into `docs/topics/` or `docs/reference/` when you rewrite them.

Details: [`topics/wiki-import/overview.md`](./topics/wiki-import/overview.md).

## URLs

- Doc id `intro` → `/wiki/docs` (see front matter).
- Most docs → `/wiki/<doc-path>` (e.g. `mods/framework` → `/wiki/mods/framework`).
- The monorepo stub folder uses ids like `wiki/mods/hexmod` → `/wiki/wiki/mods/hexmod` (double `wiki` in the path). Prefer linking by **doc id** or stable titles rather than hand-typing URLs.

## Scripts (repo root / `wiki/`)

| Script | Location | Role |
|--------|------------|------|
| Sync `.wiki` → `docs/wiki-import/` | `wiki/scripts/sync-wiki-to-docs.mjs` | Copies `*.md` from `.wiki/`. |
| Split DE/EN pairs | `wiki/scripts/normalize-wiki-import-i18n.mjs` | EN default locale, DE under `wiki/i18n/de/...`. |
| Sidebar category keys | `wiki/scripts/write-wiki-import-category-keys.mjs` | Regenerates `_category_.json` keys under Guides/Reference/Troubleshooting. |
