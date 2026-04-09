# DataCenterExporter

## Repository Status

This repository is in a **post-monorepo split** state.

The runtime framework has been migrated to the standalone repository path:

- `c:\Users\marvi\source\repos\gregFramework\gregCore`

This means framework runtime development (`FrikaMF`, runtime plugins, templates, MCP runtime stack) now belongs to that standalone location.

## What remains here

`DataCenterExporter` now focuses on exporter/migration-related assets and supporting project history needed for the transition.

Primary retained areas include:

- `FrikaModFramework/`
- `mcp-server/`
- `tools/`
- `scripts/`
- `Templates/`
- `lib/references/`

## Migration note

If you were previously building from this repo root, switch to the standalone framework workspace for active runtime work:

```powershell
Set-Location "c:\Users\marvi\source\repos\gregFramework\gregCore"
dotnet build "FrikaMF.sln" -c Release
```

## Policies

- [Contributing](CONTRIBUTING.md)
- [Security](SECURITY.md)
- [Support](SUPPORT.md)
- [AI Policy](AI_POLICY.md)
