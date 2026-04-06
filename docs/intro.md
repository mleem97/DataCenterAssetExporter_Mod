---
id: intro
title: FrikaMF Docs Start
slug: /docs
---

This documentation matches the **current monorepo**: MelonLoader framework under `framework/`, gameplay mods under `mods/`, FFM plugins under `plugins/`, Docusaurus content in `docs/`, and the wiki app in `wiki/`. Content is organized by experience level so newcomers can start fast while advanced contributors still get deep references.

## Choose your path

- New to modding: go to **Newbies** (Topics → By audience)
- Built a few mods: go to **Intermediates**
- Working on runtime internals: go to **Pros**
- Official modules: **Mods → Framework** and **Mods** (plugins & mod wiki)

## Mods Hub

- Framework entry: [`Framework`](./mods/framework.md)
- Standalone / mod list: [`Standalone Mods`](./mods/standalone/index.md)

## Hooks and releases

- [FMF hook naming](./reference/fmf-hook-naming.md) — `FMF.<Domain>.…` and legacy `FFM.*`
- [FMF hooks catalog](./reference/fmf-hooks-catalog.md) — generated from `framework/FrikaMF/HookNames.cs`
- [Release channels](./reference/release-channels.md) — Steam Workshop vs GitHub (beta)
- [MCP server](./reference/mcp-server.md) — LLM/IDE tools over docs + `fmf_hooks.json` (optional Docker bundle)

## Repository layout (contributors)

- [Repo inventory](./contributors/repo-inventory.md) — projects, solution scope, entry points
- [Monorepo target layout](./contributors/monorepo-target-layout.md) — phased migration goals

## Source model

- **Authoring:** Markdown and MDX live in the repo-root **`docs/`** folder (this tree). See the **[`docs/` layout map](./README.md)** for what lives where (curated topics vs `wiki-import/` vs `wiki/` stubs).
- **Site:** The Docusaurus application is in **`wiki/`**; routes use base **`/wiki`** (see [Docusaurus workflow](./contributors/docusaurus-workflow.md)).
- **Legacy GitHub Wiki:** Mirrored under **`docs/wiki-import/`**; refresh from **`.wiki/`** with `npm run wiki:refresh` in `wiki/`. Details: [Legacy wiki import](./topics/wiki-import/overview.md).
