# Contributing

Vielen Dank für dein Interesse an Beiträgen zu `DataCenterExporter`.

## Grundsätze

- Beiträge müssen dem legitimen Modding-Zweck dienen.
- Keine Features oder Änderungen, die Urheberrechtsverletzungen, Asset-Diebstahl oder unrechtmäßige Weiterverbreitung unterstützen.
- Änderungen bitte klein, fokussiert und nachvollziehbar halten.

## Lokale Entwicklung

1. Repository forken und Branch erstellen (`feature/...`, `fix/...`).
2. Änderungen umsetzen.
3. Lokal bauen:

```powershell
dotnet build DataCenterExporter.sln -v:minimal
```

4. Funktional prüfen (Hotkeys `F8`, `F9`, `F10`).
5. Pull Request mit klarer Beschreibung öffnen.

## Coding-Richtlinien

- Bestehenden Stil in `Main.cs` beibehalten.
- Keine unnötigen Abhängigkeiten hinzufügen.
- IL2CPP-/MelonLoader-Kompatibilität beachten.

## Pull-Request-Checkliste

- [ ] Build ist erfolgreich.
- [ ] Änderung ist auf das Thema begrenzt.
- [ ] README/Docs wurden bei Bedarf aktualisiert.
- [ ] Keine ethisch/rechtlich problematischen Inhalte.
