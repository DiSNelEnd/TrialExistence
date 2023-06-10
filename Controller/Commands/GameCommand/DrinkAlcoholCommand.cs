using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkAlcoholCommand : ICommand
{
    private readonly string serviceName = "Alcohol";
    private readonly string conditionName = "Intoxication";
    private readonly string cutSceenName = "CreateAlcohol";
    public void Do()
    {
        EatComButtonsControl.Instance.ToggleAlcohol(false);
        Computer.Instance.PlayFoodPrinterButtonSound();
        if (GameRoot.IsGameNotStart) return;
        var isNoti= GameRoot.Game.Drink(serviceName);
        if (isNoti) return;
        GameRoot.Game.ImposeCondition(conditionName);
        Turntable.PlayCutSceen(cutSceenName);
    }
}
