# Changelog

Alle relevanten Änderungen am Projekt werden in dieser Datei dokumentiert.

## [Unreleased]

### Added

- Strukturierter Export nach Ordnern: `Models`, `Textures`, `Sprites`, `Materials`, `Scripts`, `Settings`.
- Optionaler Export nicht verwendeter Assets nach `NotUsed/Models` und `NotUsed/Textures`.
- Zusatzdateien: `components.txt`, `materials.txt`, `objects.txt`, `summary.txt`.

### Changed

- Runtime-Kompatibilität für IL2CPP verbessert (String-Vergleich ohne problematische Overloads).
- Build-Konfiguration: `il2cpp-unpack`-Dateien von Kompilierung ausgeschlossen.

### Fixed

- Fehlerbild mit `ReadOnlySpan<T>.GetPinnableReference()` im Exportpfad reduziert/behoben.
