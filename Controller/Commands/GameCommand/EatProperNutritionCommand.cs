using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatProperNutritionCommand : ICommand
{
    private readonly string serviceName = "ProperNutrition";
    private readonly string conditionName = "LikeAClock";
    private readonly string cutSceenName = "CreateProperNutrition";
    private readonly string notiLocKey = "Noti.Eat.Enlightenment";
    private readonly string obsessionNotiLocKey = "Noti.Obsession";
    private bool IsObsession => GameRoot.Game.Player.Contains("Obsession");
    public void Do()
    {
        EatComButtonsControl.Instance.ToggleProperNutrition(false);
        Computer.Instance.PlayFoodPrinterButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification
            (GameRoot.Game.StateControl.Enlightenment, notiLocKey, Notifications.ComputerShowMessage)) return;
        if (GameRoot.CallNotification
            (IsObsession, obsessionNotiLocKey, Notifications.ComputerShowMessage)) return;
        var isNoti = GameRoot.Game.Eat(serviceName);
        if (isNoti) return;
        GameRoot.Game.ImposeCondition(conditionName);
        Turntable.PlayCutSceen(cutSceenName);
    }
}
