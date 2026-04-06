---
title: Monorepo — Getting started
sidebar_label: Monorepo getting started
description: Target layout (FrikaModFramework, templates, tools) and how it maps to this repo today.
---

# Monorepo — Getting started

The **goal** is a clear split between the framework (`FrikaModFramework/` registry + planned bindings), the **live** MelonLoader code (`framework/`), gameplay mods (`mods/`), FFM plugins (`plugins/`), templates (`templates/` / `Templates/`), and tooling (`tools/`). Migration is incremental: production C# still lives under [`framework/`](https://github.com/mleem97/gregFramework/tree/master/framework).

## Clone and build the framework

```text
dotnet build framework/FrikaMF.csproj
```

Or open [`FrikaMF.sln`](https://github.com/mleem97/gregFramework/blob/master/FrikaMF.sln) in Visual Studio / Rider.

## Hook naming

- **Target convention:** `FMF.<DOMAIN>.<Event>` (see [`CONTRIBUTING.md`](https://github.com/mleem97/gregFramework/blob/master/CONTRIBUTING.md)).
- **Registry:** [`FrikaModFramework/fmf_hooks.json`](https://github.com/mleem97/gregFramework/blob/master/FrikaModFramework/fmf_hooks.json).
- **Legacy runtime strings** may still use `FFM.*` in [`HookNames`](https://github.com/mleem97/gregFramework/blob/master/framework/FrikaMF/HookNames.cs) until migrated.

## Create a mod from the template

1. Copy [`templates/mod/`](https://github.com/mleem97/gregFramework/tree/master/templates/mod) to a new folder (or start from [`Templates/`](https://github.com/mleem97/gregFramework/tree/master/Templates) scaffolds).
2. Edit `fmf/hooks.json` and add sources under `src/`.
3. For a **pilot** layout (Workshop VDF + hooks metadata), see [`HexMod/`](https://github.com/mleem97/gregFramework/tree/master/HexMod); shipped mod examples live under [`mods/`](https://github.com/mleem97/gregFramework/tree/master/mods) (e.g. `mods/FMF.Mod.HexLabelMod/`).

## Documentation site

- **Content:** [`docs/`](https://github.com/mleem97/gregFramework/tree/master/docs)
- **Docusaurus app:** [`wiki/`](https://github.com/mleem97/gregFramework/tree/master/wiki) — `npm install` and `npm run start` (dev) or `npm run build` (static output).

### Docker

- **Dev server with hot reload:** `docker compose up docs` (port **3000**, mounts `./wiki` and `./docs`).
- **Static wiki + MCP in one container:** `docker compose up docs-mcp` — see [`reference/mcp-server`](../../reference/mcp-server.md).

## Assistants / MCP

The repo includes [`mcp-server/`](https://github.com/mleem97/gregFramework/tree/master/mcp-server) for Model Context Protocol (search docs, read `fmf_hooks.json`, CONTRIBUTING). Use **stdio** locally or the **HTTP** endpoint bundled with the `docs-mcp` Docker image — details in [`docs/reference/mcp-server.md`](https://github.com/mleem97/gregFramework/blob/master/docs/reference/mcp-server.md).
