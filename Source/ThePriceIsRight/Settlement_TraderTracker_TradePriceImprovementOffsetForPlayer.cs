using HarmonyLib;
using RimWorld.Planet;

namespace The_Price_Is_Right;

[HarmonyPatch(typeof(Settlement_TraderTracker), nameof(Settlement_TraderTracker.TradePriceImprovementOffsetForPlayer),
    MethodType.Getter)]
internal static class Settlement_TraderTracker_TradePriceImprovementOffsetForPlayer
{
    public static void Postfix(ref float __result)
    {
        __result = PriceIsRightMod.Settings.TradeBonus; //not 0.02f :/
    }
}