using System.Reflection;
using HarmonyLib;
using Mlie;
using UnityEngine;
using Verse;

namespace The_Price_Is_Right;

public class PriceIsRightMod : Mod
{
    public static Settings Settings;
    public static string CurrentVersion;

    public PriceIsRightMod(ModContentPack content) : base(content)
    {
        // initialize settings, but after loading done for def existence.
        LongEventHandler.ExecuteWhenFinished(() => { Settings = GetSettings<Settings>(); });
        new Harmony("Uuugggg.rimworld.The_Price_Is_Right.main").PatchAll(Assembly.GetExecutingAssembly());
        CurrentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        Settings.DoWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "TD.ThePriceIsRight".Translate();
    }
}