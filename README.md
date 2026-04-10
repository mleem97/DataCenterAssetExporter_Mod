# gregDataCenterExporter (DataCenterExporter)

Legacy monolith after the **old monorepo split**: exporter, migration, and tooling assets. Active framework runtime development lives in **gregCore**.

---

## Part of gregFramework

This directory is part of the **[gregFramework](https://github.com/mleem97/gregFramework)** workspace. Clone sibling repositories side by side so each project lives at `gregFramework/<RepoName>/`. See the workspace [README](https://github.com/mleem97/gregFramework/blob/master/README.md) for the full layout and migration notes.

| | |
|:---|:---|
| **Remote** | [`mleem97/gregFramework`](https://github.com/mleem97/gregFramework) (history; content may align with this folder) |

---

## What remains here

- `FrikaModFramework/`
- `mcp-server/`, `tools/`, `scripts/`, `Templates/`, `lib/references/` (as applicable)

---

## Building the active framework

For runtime work on the framework:

```powershell
Set-Location ".\..\gregCore"
dotnet build "FrikaMF.sln" -c Release
```

(Path is relative from `gregDataCenterExporter/` to the sibling `gregCore` repository.)

---

## Policies

- [CONTRIBUTING.md](CONTRIBUTING.md)
- [SECURITY.md](SECURITY.md)
- [SUPPORT.md](SUPPORT.md)
- [AI_POLICY.md](AI_POLICY.md)

Migration: see **`../MONOREPO-LEGACY.md`** at the workspace root (if present).
