﻿using UnityEngine;
using Verse;

namespace TD.Utilities;

public static class Listing_StandardExtensions
{
    public static void SliderLabeled(this Listing_Standard ls, string label, ref float val, string format,
        float min = 0f, float max = 1f, string tooltip = null)
    {
        var rect = ls.GetRect(Text.LineHeight);
        var rect2 = rect.LeftPart(.70f).Rounded();
        var rect3 = rect.RightPart(.30f).Rounded().LeftPart(.67f).Rounded();
        var rect4 = rect.RightPart(.10f).Rounded();

        var anchor = Text.Anchor;
        Text.Anchor = TextAnchor.MiddleLeft;
        Widgets.Label(rect2, label);

        var result = Widgets.HorizontalSlider(rect3, val, min, max, true);
        val = result;
        Text.Anchor = TextAnchor.MiddleRight;
        Widgets.Label(rect4, string.Format(format, val));
        if (!tooltip.NullOrEmpty())
        {
            TooltipHandler.TipRegion(rect, tooltip);
        }

        Text.Anchor = anchor;
        ls.Gap(ls.verticalSpacing);
    }
}