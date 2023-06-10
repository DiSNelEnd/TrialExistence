using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunPlayGameCommand : ICommand
{
    private readonly string serviceName = "PlayGame";
    private readonly string conditionName = "Motivation";
    private readonly string cutSceenName = "FunPlayGame";
    private readonly string notiLockey = "Noti.Fun.Flint";
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification
            (GameRoot.Game.StateControl.Flint, notiLockey, Notifications.ComputerShowMessage)) return;
        var isNoti= GameRoot.Game.Fun(serviceName);
        if (isNoti) return;
        GameRoot.Game.ImposeCondition(conditionName);
        Turntable.PlayCutSceen(cutSceenName);
    }
}
