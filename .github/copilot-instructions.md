# GitHub Copilot Instructions for RimWorld Mod: The Price Is Right (Continued)

Welcome to contributing to **The Price Is Right (Continued)**, a RimWorld mod that enhances the caravan trading experience. This document provides detailed guidance to leverage GitHub Copilot efficiently when contributing to or extending this mod.

## Mod Overview and Purpose

**The Price Is Right (Continued)** rejuvenates Uuugggg's original mod and modifies the trading mechanics within RimWorld. It aims to adjust trade prices for caravans to be more favorable, offering closer to market value, rather than a minimal flat 2% caravan bonus. While enhancing trade efficiency, the mod retains the influence of game difficulty and colonist skills on trading outcomes. Additionally, the mod introduces a mood bonus for colonists on caravans, further amplified if beds are available.

## Key Features and Systems

1. **Enhanced Trade Pricing:** 
   - Caravans benefit from trading bonuses that are closer to actual market prices.
   - Intact balance by considering game difficulty and skill mechanics.

2. **Colonist Mood Improvements:**
   - Caravans provide mood bonuses.
   - Additional benefits if caravans include beds.

## Coding Patterns and Conventions

- **File and Type Structure:** The source files are organized based on functionality:
  - `Listing_StandardExtensions.cs` contains static extension methods for enhanced list handling.
  - `DebugLog.cs` provides static logging utilities for debugging.
  - `Settings.cs` manages mod settings and integrates with RimWorld's settings infrastructure.
  - `The_Price_Is_Right.cs` manages mod entry points and initialization via the Verse.Mod class.
  - `TradePriceOffset.cs` handles trade price calculations and mechanics.
  - `TravelingMood.cs` defines moods associated with traveling.

- **Naming Conventions:**
  - Classes are named in PascalCase.
  - Methods and properties follow PascalCase.
  - Private fields should use camelCase, preferring underscores for instance variables.

- **Documentation and Comments:** Ensure every method has XML documentation comments explaining its parameters and return values. Use inline comments for complex logic sections.

## XML Integration

The mod uses RimWorld's XML files for defining resources and configurations where:
- `ThoughtDefs` for mood effects are defined to compliment `ThoughtWorker_Traveling`.
- Ensure XML def names align with the corresponding C# class names for consistency.

## Harmony Patching

Use Harmony patches for altering core game methods without direct modification:
- Create `HarmonyPatch` attributes above classes or methods to define which parts of the game code to patch.
- Aim for postfix or prefix patches to avoid unexpected behavior or conflicts.
- Ensure patches are applied and removed with the mod's lifecycle methods.

## Suggestions for Copilot

- **Autogenerate Method Templates:** Use Copilot to create boilerplate code for new methods to maintain consistency, especially in settings handling or new mood effect handlers.
- **Inline Suggestions:** Leverage Copilot's ability to suggest inline code snippets, especially helpful for writing complex LINQ queries or managing collections.
- **Comment Generation:** Let Copilot suggest documentation comments based on method names and content to maintain consistency.

By following these guidelines and leveraging GitHub Copilot's capabilities effectively, you can enhance or maintain this mod, ensuring an improved experience for RimWorld players.
