using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;

namespace The_Price_Is_Right;

[HarmonyPatch(typeof(Tradeable), "InitPriceDataIfNeeded")]
internal static class Tradeable_InitPriceDataIfNeeded
{
    private static readonly AccessTools.FieldRef<Tradeable, float> pricePlayerBuy =
        AccessTools.FieldRefAccess<Tradeable, float>("pricePlayerBuy");

    private static readonly AccessTools.FieldRef<Tradeable, float> pricePlayerSell =
        AccessTools.FieldRefAccess<Tradeable, float>("pricePlayerSell");

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var priceBuyInfo = AccessTools.Field(typeof(Tradeable), "pricePlayerBuy");
        var priceSellInfo = AccessTools.Field(typeof(Tradeable), "pricePlayerSell");

        var adjustPricesInfo = AccessTools.Method(typeof(Tradeable_InitPriceDataIfNeeded), nameof(AdjustPrices));

        var instList = instructions.ToList();
        for (var i = 0; i < instList.Count; i++)
        {
            var inst = instList[i];
            //IL_00e9: ldarg.0      // this
            //IL_00ea: ldarg.0      // this
            //IL_00eb: ldfld float32 RimWorld.Tradeable::pricePlayerBuy
            //IL_00f0: stfld float32 RimWorld.Tradeable::pricePlayerSell
            if (inst.StoresField(priceSellInfo)
                && instList[i - 1].LoadsField(priceBuyInfo))
            {
                //Tradeable this, pricePlayerBuy on stack
                yield return new CodeInstruction(OpCodes.Call, adjustPricesInfo); //AdjustPrices(Tradeable)
            }
            else
            {
                yield return inst;
            }
        }
    }

    public static void AdjustPrices(Tradeable item, float buyPrice)
    {
        if (PriceIsRightMod.Settings.BestPrice)
        {
            return;
        }

        var sellPrice = pricePlayerSell(item);

        switch (PriceIsRightMod.Settings.FairPrice)
        {
            case true when item.FirstThingColony == null:
                sellPrice = buyPrice;
                break;
            case true when item.FirstThingTrader == null:
                buyPrice = sellPrice;
                break;
            default:
                buyPrice = sellPrice = (buyPrice + sellPrice) / 2;
                break;
        }

        pricePlayerBuy(item) = buyPrice;
        pricePlayerSell(item) = sellPrice;
    }
}