using Il2Cpp;

namespace DataCenter.ModigAPIs;

public static class PlayerApi
{
    public static bool IsAvailable()
    {
        return ModigGame.GetPlayerRaw() != null;
    }

    public static Player GetRaw()
    {
        return ModigGame.GetPlayerRaw();
    }

    public static float GetMoney()
    {
        var player = GetRaw();
        return player != null ? player.money : 0f;
    }

    public static float GetXp()
    {
        var player = GetRaw();
        return player != null ? player.xp : 0f;
    }

    public static float GetReputation()
    {
        var player = GetRaw();
        return player != null ? player.reputation : 0f;
    }

    public static bool TryAddMoney(float amount, bool withoutSound = true)
    {
        var player = GetRaw();
        if (player == null)
            return false;

        player.UpdateCoin(amount, withoutSound);
        return true;
    }

    public static bool TrySetMoney(float targetMoney, bool withoutSound = true)
    {
        var player = GetRaw();
        if (player == null)
            return false;

        var delta = targetMoney - player.money;
        player.UpdateCoin(delta, withoutSound);
        return true;
    }

    public static bool TryAddXp(float amount)
    {
        var player = GetRaw();
        if (player == null)
            return false;

        player.UpdateXP(amount);
        return true;
    }

    public static bool TrySetXp(float targetXp)
    {
        var player = GetRaw();
        if (player == null)
            return false;

        var delta = targetXp - player.xp;
        player.UpdateXP(delta);
        return true;
    }

    public static bool TryAddReputation(float amount)
    {
        var player = GetRaw();
        if (player == null)
            return false;

        player.UpdateReputation(amount);
        return true;
    }

    public static bool TrySetReputation(float targetReputation)
    {
        var player = GetRaw();
        if (player == null)
            return false;

        var delta = targetReputation - player.reputation;
        player.UpdateReputation(delta);
        return true;
    }
}