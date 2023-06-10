using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceenSpritesControl
{
    private Dictionary<string, string> spriteNames;
    public static CutSceenSpritesControl Instance => lazy.Value;
    private static readonly Lazy<CutSceenSpritesControl> lazy =
        new Lazy<CutSceenSpritesControl>(() => new CutSceenSpritesControl());
    private CutSceenSpritesControl()
    {
        InitialSpriteName();
    }

    public void ToggleSprite(string cutSceenName, bool flag)
    {
        var spriteName = GetSpriteName(cutSceenName);
        Switcher.Toggle(spriteName, flag);
    }

    private void InitialSpriteName()
    {
        spriteNames = new Dictionary<string, string>()
        {
            {"CreateFastFood","FastFoodSprite" },
            {"CreateStandardKitchen","StandardKitchenSprite" },
            {"CreateProperNutrition","ProperNutritionSprite" },
            {"CreateDessert","DessertSprite" },
            {"CreateAlcohol", "AlcoholSprite" },
            {"FunPlayGame", "PlayGameSprite"},
            {"FunSeeSomething", "SeeSomethingSprite" },
            {"SplashScreen","SplashScreenSprite" },
            {"WinGame","WinGameSprite" },
            {"GetToWork","WorkSprite" },
            {"PropheticDream","SleepPropheticSprite" }
        };
    }

    private string GetSpriteName(string cutSceenName)
    {
        return spriteNames.TryGetValue(cutSceenName, out var result) 
            ? result : throw new KeyNotFoundException("sprite not found");
    }
}
