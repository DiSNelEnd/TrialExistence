using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFastFoodCommand : ICommand
{
    private readonly string serviceName = "FastFood";
    private readonly string conditionName = "DigestiveProblems";
    private readonly string cutSceenName = "CreateFastFood";
    private readonly string notiLocKey = "Noti.Eat.Enlightenment";
    private readonly string obsessionNotiLocKey = "Noti.Obsession";
    private bool IsObsession => GameRoot.Game.Player.Contains("Obsession");
    public void Do()
    {
        EatComButtonsControl.Instance.ToggleFastFood(false);
        Computer.Instance.PlayFoodPrinterButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification
            (GameRoot.Game.StateControl.Enlightenment, notiLocKey, Notifications.ComputerShowMessage)) return;
        if (GameRoot.CallNotification
            (IsObsession, obsessionNotiLocKey, Notifications.ComputerShowMessage)) return;
        var isNoti= GameRoot.Game.Eat(serviceName);
        if (isNoti) return;
        GameRoot.Game.ImposeCondition(conditionName);
        Turntable.PlayCutSceen(cutSceenName);
    }
}
