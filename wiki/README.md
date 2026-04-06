# FrikaMF Docusaurus site

This folder is the **Docusaurus application** (theme, `sidebars.js`, React pages). **Markdown content** is authored in the repo-root **`docs/`** folder (`routeBasePath` → `/wiki`).

## Local workflow

```bash
cd wiki
npm install
npm run start
```

Build production static files:

```bash
npm run build
```

Optional: `npm run wiki:sync` / `wiki:normalize-i18n` — see `package.json` scripts.

## Docker Compose (repository root)

### Live dev server (hot reload)

Maps **http://localhost:3000** — mounts `./wiki` and `./docs` into the image.

```bash
docker compose up docs
```

### One-shot build (CI-style)

```bash
docker compose run --rm docs-build
```

### Static wiki + MCP (single container)

Serves the **built** site and exposes MCP on `/mcp` — **http://localhost:3040** on the host (see [`docs/reference/mcp-server.md`](../docs/reference/mcp-server.md)).

```bash
docker compose up docs-mcp
```

The root [`Dockerfile`](../Dockerfile) builds Docusaurus and bundles [`mcp-server/`](../mcp-server/).
