using Il2Cpp;
using UnityEngine;

namespace DataCenter.ModigAPIs;

public static class UiApi
{
    public static bool IsAvailable()
    {
        return ModigGame.GetUiRaw() != null;
    }

    public static StaticUIElements GetRaw()
    {
        return ModigGame.GetUiRaw();
    }

    public static bool TryNotify(string text, int localisationUid = -1, Sprite sprite = null)
    {
        var ui = GetRaw();
        if (ui == null)
            return false;

        ui.SetNotification(localisationUid, sprite, text ?? string.Empty);
        return true;
    }

    public static bool TryAddMessage(string message)
    {
        var ui = GetRaw();
        if (ui == null)
            return false;

        ui.AddMeesageInField(message ?? string.Empty);
        return true;
    }
}