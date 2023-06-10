using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPornSiteCommand : ICommand
{
    private readonly string serviceName = "PornSite";
    private readonly string conditionName = "Weakness";
    private readonly string notiLocKey = "Noti.GoPornSite.Asexual";
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification
            (GameRoot.Game.StateControl.Asexual, notiLocKey, Notifications.ComputerShowMessage)) return;
        var isNoti= GameRoot.Game.LookPorn(serviceName);
        if (isNoti) return;
        GameRoot.Game.ImposeCondition(conditionName);
        Turntable.PlayCutSceen("Plug");
    }
}
