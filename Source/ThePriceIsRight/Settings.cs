using TD.Utilities;
using UnityEngine;
using Verse;

namespace The_Price_Is_Right;

public class Settings : ModSettings
{
    public bool BestPrice = true;
    public bool FairPrice;

    public bool MoodBonus = true;
    private float moodBonus0 = 25;
    private float moodBonus1 = 35;
    public float TradeBonus = 0.30f;

    public void DoWindowContents(Rect rect)
    {
        var options = new Listing_Standard();
        options.Begin(rect);

        options.SliderLabeled("TD.SettingTradeBonus".Translate(), ref TradeBonus, "{0:P0}", .02f, .50f);
        options.Label("TD.SettingBonusAlso".Translate());
        options.CheckboxLabeled("TD.SettingBestPrice".Translate(), ref BestPrice,
            "TD.SettingBestPriceDesc".Translate());
        FairPrice &= !BestPrice;
        options.CheckboxLabeled("TD.SettingFairPrice".Translate(), ref FairPrice,
            "TD.SettingFairPriceDesc".Translate());
        options.Label("TD.SettingOtherwisePrice".Translate());
        BestPrice &= !FairPrice;

        options.Gap();

        options.CheckboxLabeled("TD.SettingMoodBonus".Translate(), ref MoodBonus);
        if (MoodBonus)
        {
            options.SliderLabeled("TD.SettingMoodBonusNoBeds".Translate(), ref moodBonus0, "+{0:0}", 0, 50);
            options.SliderLabeled("TD.SettingMoodBonusBeds".Translate(), ref moodBonus1, "+{0:0}", 0, 50);
            ThoughtDefOf.Traveling.stages[0].baseMoodEffect = moodBonus0;
            ThoughtDefOf.Traveling.stages[1].baseMoodEffect = moodBonus1;
        }

        if (PriceIsRightMod.CurrentVersion != null)
        {
            options.Gap();
            GUI.contentColor = Color.gray;
            options.Label("TD.CurrentModVersion".Translate(PriceIsRightMod.CurrentVersion));
            GUI.contentColor = Color.white;
        }

        options.End();
    }

    public override void ExposeData()
    {
        Scribe_Values.Look(ref TradeBonus, "tradeBonus", 0.30f);
        Scribe_Values.Look(ref BestPrice, "bestPrice", true);
        Scribe_Values.Look(ref FairPrice, "fairPrice");

        Scribe_Values.Look(ref MoodBonus, "moodBonus", true);
        Scribe_Values.Look(ref moodBonus0, "moodBonus0", 25f);
        Scribe_Values.Look(ref moodBonus1, "moodBonus1", 35f);

        if (Scribe.mode != LoadSaveMode.PostLoadInit)
        {
            return;
        }

        ThoughtDefOf.Traveling.stages[0].baseMoodEffect = moodBonus0;
        ThoughtDefOf.Traveling.stages[1].baseMoodEffect = moodBonus1;
    }
}