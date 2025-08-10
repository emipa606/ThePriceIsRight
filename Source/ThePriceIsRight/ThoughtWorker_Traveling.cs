using System.Linq;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace The_Price_Is_Right;

public class ThoughtWorker_Traveling : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn pawn)
    {
        if (!PriceIsRightMod.Settings.MoodBonus || pawn.Map is { IsPlayerHome: true } ||
            pawn.GetCaravan() is not { } caravan)
        {
            return ThoughtState.Inactive;
        }

        var caravanPawnsListForReading = caravan.PawnsListForReading;
        var bedCount = caravanPawnsListForReading.Select(countBeds).Sum();
        var colonistCount = caravanPawnsListForReading.FindAll(p => p.IsColonist).Count;
        return ThoughtState.ActiveAtStage(bedCount >= colonistCount ? 1 : 0);
    }

    private static int countBeds(Pawn pawn)
    {
        return pawn.inventory.innerContainer.Count(t => t.GetInnerIfMinified() is Building_Bed);
    }
}