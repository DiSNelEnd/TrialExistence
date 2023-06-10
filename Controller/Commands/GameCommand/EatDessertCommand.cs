using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatDessertCommand : ICommand
{
    private readonly string serviceName = "Dessert";
    private readonly string conditionName = "Planted";
    private readonly string cutSceenName = "CreateDessert";
    private readonly string engliNotiLocKey = "Noti.Eat.Enlightenment";
    private readonly string obsessionNotiLocKey = "Noti.Obsession";
    private bool IsObsession => GameRoot.Game.Player.Contains("Obsession");
    public void Do()
    {
        EatComButtonsControl.Instance.ToggleDessert(false);
        Computer.Instance.PlayFoodPrinterButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification
            (GameRoot.Game.StateControl.Enlightenment, engliNotiLocKey, Notifications.ComputerShowMessage)) return;
        if (GameRoot.CallNotification
            (IsObsession,obsessionNotiLocKey,Notifications.ComputerShowMessage)) return;
        var isNoti= GameRoot.Game.Eat(serviceName);
        if (isNoti) return;
        GameRoot.Game.ImposeCondition(conditionName);
        Turntable.PlayCutSceen(cutSceenName);
    }
}
