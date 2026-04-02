using Il2Cpp;

namespace DataCenter.ModigAPIs;

public static class ModigGame
{
    public static bool IsGameReady()
    {
        return PlayerManager.instance != null && PlayerManager.instance.playerClass != null;
    }

    public static Player GetPlayerRaw()
    {
        return PlayerManager.instance?.playerClass;
    }

    public static NetworkMap GetNetworkMapRaw()
    {
        return NetworkMap.instance;
    }

    public static StaticUIElements GetUiRaw()
    {
        return StaticUIElements.instance;
    }

    public static TimeController GetTimeRaw()
    {
        return TimeController.instance;
    }

    public static Localisation GetLocalisationRaw()
    {
        return Localisation.instance;
    }
}