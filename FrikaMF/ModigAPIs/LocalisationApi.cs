using Il2Cpp;

namespace DataCenter.ModigAPIs;

public static class LocalisationApi
{
    public static bool IsAvailable()
    {
        return ModigGame.GetLocalisationRaw() != null;
    }

    public static Localisation GetRaw()
    {
        return ModigGame.GetLocalisationRaw();
    }

    public static string GetCurrentLanguageName()
    {
        var loc = GetRaw();
        return loc != null ? loc.currentlySelectedLanguage.ToString() : "Unknown";
    }

    public static int GetCurrentLanguageUid()
    {
        var loc = GetRaw();
        return loc != null ? loc.loadLanguageUID : -1;
    }

    public static string GetTextById(int uid)
    {
        var loc = GetRaw();
        if (loc == null)
            return string.Empty;

        var text = loc.ReturnTextByID(uid);
        return text ?? string.Empty;
    }

    public static bool TryChangeLanguage(int languageUid)
    {
        var loc = GetRaw();
        if (loc == null)
            return false;

        loc.ChangeLocalisation(languageUid);
        return true;
    }
}