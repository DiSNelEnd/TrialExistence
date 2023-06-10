using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatComButtonsControl
{
    private const string FFDBUTTONNAME= "FastFoodCosmoButton";
    private const string SKBUTTONNAME = "StandardKitchenCosmoButton";
    private const string PNBUTTONNAME = "ProperNutritionCosmoButton";
    private const string DESERTBUTTONNAME = "DessertCosmoButton";
    private const string ALCOHOLBUTTONNAME = "AlcoholCosmoButton";
    public Action OffToggleEatButton { get; private set; }
    public static EatComButtonsControl Instance => lazy.Value;
    private static readonly Lazy<EatComButtonsControl> lazy =
        new Lazy<EatComButtonsControl>(() => new EatComButtonsControl());
    private EatComButtonsControl() { }
    public void ToggleFastFood(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleFastFood(false));
        Switcher.Toggle(FFDBUTTONNAME, flag);
    }

    public void ToggleStandardKitchen(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleStandardKitchen(false));
        Switcher.Toggle(SKBUTTONNAME, flag);
    }

    public void ToggleProperNutrition(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleProperNutrition(false));
        Switcher.Toggle(PNBUTTONNAME, flag);
    }

    public void ToggleDessert(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleDessert(false));
        Switcher.Toggle(DESERTBUTTONNAME, flag);
    }

    public void ToggleAlcohol(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleAlcohol(false));
        Switcher.Toggle(ALCOHOLBUTTONNAME, flag);
    }

    private void SaveOffToggle(Action offToggle)
    {
        OffToggleEatButton?.Invoke();
        OffToggleEatButton = offToggle;
    }
}
