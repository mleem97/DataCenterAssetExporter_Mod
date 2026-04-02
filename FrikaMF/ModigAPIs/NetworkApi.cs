using System.Collections.Generic;
using DataCenter.ModigAPIs.Models;
using Il2Cpp;

namespace DataCenter.ModigAPIs;

public static class NetworkApi
{
    public static bool IsAvailable()
    {
        return ModigGame.GetNetworkMapRaw() != null;
    }

    public static NetworkMap GetRaw()
    {
        return ModigGame.GetNetworkMapRaw();
    }

    public static List<Server> GetServersSnapshot()
    {
        var result = new List<Server>();
        var map = GetRaw();
        if (map == null || map.servers == null)
            return result;

        foreach (var kv in map.servers)
        {
            if (kv.Value != null)
                result.Add(kv.Value);
        }

        return result;
    }

    public static List<NetworkSwitch> GetSwitchesSnapshot()
    {
        var result = new List<NetworkSwitch>();
        var map = GetRaw();
        if (map == null || map.switches == null)
            return result;

        foreach (var kv in map.switches)
        {
            if (kv.Value != null)
                result.Add(kv.Value);
        }

        return result;
    }

    public static List<Server> GetBrokenServersSnapshot()
    {
        var result = new List<Server>();
        var map = GetRaw();
        if (map == null || map.brokenServers == null)
            return result;

        foreach (var kv in map.brokenServers)
        {
            if (kv.Value != null)
                result.Add(kv.Value);
        }

        return result;
    }

    public static List<NetworkSwitch> GetBrokenSwitchesSnapshot()
    {
        var result = new List<NetworkSwitch>();
        var map = GetRaw();
        if (map == null || map.brokenSwitches == null)
            return result;

        foreach (var kv in map.brokenSwitches)
        {
            if (kv.Value != null)
                result.Add(kv.Value);
        }

        return result;
    }

    public static bool TryBreakServer(Server server)
    {
        if (server == null)
            return false;

        server.ItIsBroken();
        return true;
    }

    public static bool TryBreakSwitch(NetworkSwitch networkSwitch)
    {
        if (networkSwitch == null)
            return false;

        networkSwitch.ItIsBroken();
        return true;
    }

    public static bool TryRepairServer(Server server, bool powerOn = true)
    {
        if (server == null)
            return false;

        server.RepairDevice();
        server.ClearWarningSign(false);
        server.ClearErrorSign();
        if (powerOn)
            server.PowerButton(true);

        return true;
    }

    public static bool TryRepairSwitch(NetworkSwitch networkSwitch, bool powerOn = true)
    {
        if (networkSwitch == null)
            return false;

        networkSwitch.RepairDevice();
        networkSwitch.ClearWarningSign(false);
        networkSwitch.ClearErrorSign();
        if (powerOn)
            networkSwitch.PowerButton(true);

        return true;
    }

    public static int RepairAllBrokenDevices(bool powerOn = true)
    {
        var repaired = 0;

        var brokenServers = GetBrokenServersSnapshot();
        for (var i = 0; i < brokenServers.Count; i++)
        {
            if (TryRepairServer(brokenServers[i], powerOn))
                repaired++;
        }

        var brokenSwitches = GetBrokenSwitchesSnapshot();
        for (var i = 0; i < brokenSwitches.Count; i++)
        {
            if (TryRepairSwitch(brokenSwitches[i], powerOn))
                repaired++;
        }

        return repaired;
    }

    public static NetworkCounts GetCounts()
    {
        return new NetworkCounts
        {
            TotalServers = GetServersSnapshot().Count,
            BrokenServers = GetBrokenServersSnapshot().Count,
            TotalSwitches = GetSwitchesSnapshot().Count,
            BrokenSwitches = GetBrokenSwitchesSnapshot().Count,
        };
    }
}