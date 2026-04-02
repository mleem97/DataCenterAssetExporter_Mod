using Il2Cpp;

namespace DataCenter.ModigAPIs;

public static class TimeApi
{
    public static bool IsAvailable()
    {
        return ModigGame.GetTimeRaw() != null;
    }

    public static TimeController GetRaw()
    {
        return ModigGame.GetTimeRaw();
    }

    public static int GetCurrentDay()
    {
        var time = GetRaw();
        return time != null ? time.day : 0;
    }

    public static float GetCurrentHour()
    {
        var time = GetRaw();
        return time != null ? time.CurrentTimeInHours() : 0f;
    }

    public static float GetTimeMultiplier()
    {
        var time = GetRaw();
        return time != null ? time.timeMultiplier : 1f;
    }

    public static bool TrySetTimeMultiplier(float multiplier)
    {
        var time = GetRaw();
        if (time == null)
            return false;

        time.timeMultiplier = multiplier;
        return true;
    }

    public static bool IsBetween(float startHour, float endHour)
    {
        var time = GetRaw();
        return time != null && time.TimeIsBetween(startHour, endHour);
    }
}