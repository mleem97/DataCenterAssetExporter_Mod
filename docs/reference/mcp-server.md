---
title: MCP server (LLM-friendly modding)
sidebar_label: MCP server
description: Model Context Protocol server bundled with the docs Docker image for assistants and IDEs.
---

# MCP server (FrikaMF modding)

The repository ships a **Model Context Protocol (MCP)** server (`mcp-server/`) that exposes:

- **Docs search & read** — Markdown under `docs/` (same source as Docusaurus).
- **Hook registry** — `fmf_hooks.json` (and the same registry path in the monorepo).
- **CONTRIBUTING** — conventions and workflow.
- **Repo layout** — static overview and a **`fmf_modding_context`** prompt for quick context.

Use it when you want an assistant (Cursor, Claude Code, or any MCP client) to **ground answers** in this repo instead of guessing APIs.

## Docker (single container)

The root `Dockerfile` builds **Docusaurus** into static files and runs **one Node process** that:

- Serves the wiki at **`http://<host>:3000/`** (static HTML).
- Exposes **Streamable HTTP MCP** at **`POST /mcp`** and **`GET /mcp`** (SSE), plus **`GET /health`**.

From the repo root:

```bash
docker compose up docs-mcp
```

Default mapping: **`http://localhost:3040`** (host) → container port **3000**.

- Docs: `http://localhost:3040/wiki/`
- Health: `http://localhost:3040/health`
- MCP: `http://localhost:3040/mcp` (client must speak MCP Streamable HTTP; session via `mcp-session-id` header).

:::note Security

Binding to `0.0.0.0` exposes the MCP endpoint on your network. Use only on trusted networks or behind a reverse proxy with authentication.

:::

## Local (stdio) for Cursor

From `mcp-server/` with the repo root as data (so `docs/`, `CONTRIBUTING.md`, and `FrikaModFramework/fmf_hooks.json` resolve):

```bash
cd mcp-server
node src/index.mjs --stdio --data-root ..
```

Point your MCP client at this command (stdio transport). Adjust `--data-root` if your layout differs.

## Tools (summary)

| Tool | Purpose |
|------|--------|
| `fmf_search_docs` | Substring search across `docs/**/*.md` |
| `fmf_read_doc` | Read one file by path under `docs/` |
| `fmf_list_doc_paths` | Discover Markdown paths |
| `fmf_hook_registry` | Raw `fmf_hooks.json` |
| `fmf_read_contributing` | `CONTRIBUTING.md` |
| `fmf_repo_layout` | Short monorepo overview |

## Related

- [Hook naming](./fmf-hook-naming.md)
- [Hooks catalog](./fmf-hooks-catalog.md)
- [Monorepo target layout](../contributors/monorepo-target-layout.md)
