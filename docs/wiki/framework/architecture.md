---
title: Monorepo — Architecture
sidebar_label: Monorepo architecture
description: Core vs bindings vs mods; hook scanner; Game2Framework compatibility.
---

# Monorepo — Architecture

## Layers

| Layer | Role |
|------|------|
| **Core** | MelonLoader mod + event dispatch — today under `framework/FrikaMF/` (C#). Target layout: `FrikaModFramework/src/core/`. |
| **Bindings** | Language-specific surfaces — placeholders under `FrikaModFramework/src/bindings/`. |
| **Mods / plugins** | Shipped sources in `mods/` and `plugins/`; optional pilot tree `HexMod/` (VDF + hooks metadata). |
| **Docs** | Docusaurus consumes repo-root `docs/`; app lives in `wiki/`. |

## Hook registry

`FrikaModFramework/fmf_hooks.json` is the declarative **single source of truth** for documented `FMF.*` hooks. The runtime still exposes legacy `FFM.*` strings where not yet migrated.

## Tools

- **`tools/fmf-hook-scanner`** — emit the [FMF Hook Reference](./fmf-hooks) page from the registry.
- **`tools/game2framework-migrator`** — dry-run mapping using `tools/fmf-hook-scanner/mapping/game2framework-map.json`.
- **`mcp-server/`** — Model Context Protocol server (docs + registry) for IDEs; can run in Docker with the static wiki — see [MCP server](../../reference/mcp-server.md).

## Steam & Workshop

Workshop templates: `templates/workshop/`. CLI/upload scripts: `tools/steam-workshop-upload/`. Desktop uploader (Windows MAUI): `WorkshopUploader/` (see `WorkshopUploader/README.md`).
