using System.Collections.Generic;
using Il2Cpp;
using UnityEngine;

namespace DataCenter.ModigAPIs;

public static class WorldApi
{
    public static List<ComputerShop> FindComputerShops()
    {
        var result = new List<ComputerShop>();
        var shops = Object.FindObjectsOfType<ComputerShop>();
        if (shops == null)
            return result;

        for (var i = 0; i < shops.Count; i++)
        {
            if (shops[i] != null)
                result.Add(shops[i]);
        }

        return result;
    }

    public static ComputerShop FindFirstShopWithNetworkMapScreen()
    {
        var shops = FindComputerShops();
        for (var i = 0; i < shops.Count; i++)
        {
            var shop = shops[i];
            if (shop != null && shop.networkMapScreen != null)
                return shop;
        }

        return null;
    }

    public static GameObject GetNetworkMapScreen()
    {
        var shop = FindFirstShopWithNetworkMapScreen();
        return shop?.networkMapScreen;
    }
}