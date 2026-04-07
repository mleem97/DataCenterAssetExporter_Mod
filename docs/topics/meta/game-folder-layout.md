---
title: Game folder layout (FMF / MelonLoader)
sidebar_label: Game folder layout
description: Canonical paths for mod configs, FMF plugins, and MelonLoader mods under the Data Center game root.
---

# Game folder layout (FMF / MelonLoader)

This page is the **single reference** for where mod-related files live next to the game executable (`{GameRoot}`). MelonLoader sets `{GameRoot}/UserData` via `MelonEnvironment.UserDataDirectory`, `{GameRoot}/Mods` for **MelonMods**, and `{GameRoot}/Plugins` for **MelonPlugins** (e.g. `FMF.ModPathRedirector.dll`).

## Summary

| Inhalt | Pfad | Format / Hinweis |
|--------|------|------------------|
| **Mod-Konfiguration & Sidecars** | `{GameRoot}/UserData/ModCfg/` | **JSON** für Konfigurationsdateien; weitere Sidecar-Dateien (z. B. `custom_employees_hired.txt`) liegen ebenfalls hier, damit alles Mod-Bezogene an einem Ort liegt. |
| **FMF Framework-Plugins** (FFM.Plugin.*) | `{GameRoot}/FMF/Plugins/` | DLLs; **MelonLoader** lädt standardmäßig nur `{GameRoot}/Mods` — siehe unten. |
| **Plugins** (MelonLoader, z. B. ModPathRedirector) | `{GameRoot}/Plugins/` | MelonLoader `Plugins`-Ordner — nur **MelonPlugin**-DLLs. |
| **Mods** (MelonLoader, z. B. FMF.Mod.*) | `{GameRoot}/Mods/` | MelonLoader `Mods`-Ordner — **MelonMod**-DLLs. |

## UserData/ModCfg

- Alle **mod-relevanten** Konfigurationen und JSON-Sidecars werden unter **`UserData/ModCfg`** geführt.
- Beim ersten Start werden fehlende Dateien angelegt; bei bestehenden Installationen werden ältere Dateien aus **`UserData/`** (Root) nach **`ModCfg/`** übernommen, sofern noch vorhanden.
- Beispiele: `multiplayer-sync.config.json`, `pluginsync.config.json`.
- Framework-Metadaten (z. B. Save-Compat-Stamp) liegen unter **`UserData/ModCfg/FrikaFM/`** (Migration von `UserData/FrikaFM`).

## FMF/Plugins und MelonLoader

**FFM-Plugin-DLLs** liegen kanonisch unter **`{GameRoot}/FMF/Plugins`**. MelonLoader enumeriert **standardmäßig** nur **`Mods/`**. Praktische Optionen:

1. **DLLs zusätzlich** (oder verlinkt) **`Mods/`** ablegen — üblicher Weg für automatisches Laden.
2. **Unterordner** von `Mods` nutzen, falls eure MelonLoader-Version Mods in Unterverzeichnissen lädt (Version je nach Release prüfen).
3. **PluginSync**-Downloads des Frameworks landen unter **`FMF/Plugins/PluginSync/...`**.

## Mods (FMF-basiert)

Normale **MelonLoader-Mods** (einschließlich FMF-Mods) werden wie gewohnt in **`{GameRoot}/Mods/`** installiert.

## Steam Workshop (Spiel) vs. MelonLoader

Das Spiel legt abonnierte Workshop-Inhalte unter **`{GameRoot}/{ExeName}_Data/StreamingAssets/mods/workshop_<PublishedFileId>/WorkshopUploadContent`** ab (nativer `ModLoader`, nicht MelonLoader).

- **MelonLoader** durchsucht **`{GameRoot}/Mods`** (inkl. Unterordner, je nach Einstellung), **nicht** beliebige Pfade über `Loader.cfg`.
- **UserData:** MelonLoader-Konfiguration liegt unter **`{GameRoot}/UserData/`** (z. B. **`MelonLoader.cfg`** / je nach Version **`UserData/MelonLoader/Loader.cfg`** — bei Install prüfen). Relevant für Unterordner-Laden: **`disable_subfolder_load = false`**, optional **`disable_subfolder_manifest = true`**.
- **Workshop-DLLs in den Melon-Scan einbinden:** Junction (oder Symlink) von einem Ordner unter **`Mods/`** auf den **`WorkshopUploadContent`**-Pfad desselben Items, z. B. (PowerShell, Pfade anpassen):

```powershell
$game = "C:\Path\To\Data Center"
$id = "12345678901234567"
$target = Join-Path $game "Data Center_Data\StreamingAssets\mods\workshop_$id\WorkshopUploadContent"
$link = Join-Path $game "Mods\workshop_$id"
cmd /c mklink /J "$link" "$target"
```

Ohne Junction müssen MelonMods weiter physisch unter **`Mods/`** liegen oder über eure Verteilung dort landen.

**WorkshopUploader-Vorlagen (modded):** Unter **`content/`** werden **`Mods/`**, **`Plugins/`** und ein **`ModFramework/`**-Baum angelegt — **`ModFramework/FMF/Plugins`** entspricht dabei **`{GameRoot}/FMF/Plugins`**, wenn ihr **`FMF`** per Junction auf **`…/WorkshopUploadContent/ModFramework/FMF`** zeigen lasst. Weitere Framework-Dateien (Konfiguration, Assets) können unter **`ModFramework/`** gebündelt werden.

## Siehe auch

- [Meta & operations](/wiki/topics/meta/overview)
- [Legacy: Mod Config System](/wiki/wiki-import/Mod-Config-System) (API-Contract; Pfade auf **ModCfg** beziehen)
