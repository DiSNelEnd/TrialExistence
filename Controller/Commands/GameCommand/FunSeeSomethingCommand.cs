using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunSeeSomethingCommand : ICommand
{
    private readonly string servicename = "SeeSomething";
    private readonly string conditionName = "Inspiration";
    private readonly string cutSceenName = "FunSeeSomething";
    private readonly string notiLockey = "Noti.Fun.Flint";
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification
            (GameRoot.Game.StateControl.Flint, notiLockey, Notifications.ComputerShowMessage)) return;
        var isNoti= GameRoot.Game.Fun(servicename);
        if (isNoti) return;
        GameRoot.Game.ImposeCondition(conditionName);
        Turntable.PlayCutSceen(cutSceenName);
    }
}
