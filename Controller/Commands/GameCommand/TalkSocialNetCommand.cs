using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkSocialNetCommand : ICommand
{
    private readonly string serviceName = "SocNet";
    private readonly string conditionName = "Social";
    private readonly string notiLocKey = "Noti.SocNet.Schizo";
    private readonly int lvForCon = 3;
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification
            (GameRoot.Game.StateControl.Schizo, notiLocKey, Notifications.ComputerShowMessage)) return;
        GameRoot.Game.Talk(serviceName);
        if (GameRoot.Game.Ns.Lv == lvForCon)
            GameRoot.Game.ImposeCondition(conditionName);
        Turntable.PlayCutSceen("Plug");
    }
}
