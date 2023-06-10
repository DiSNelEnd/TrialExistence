using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepCommand : ICommand
{
    public static readonly string ServiceName = "Sleep";
    private readonly string notiLocKey = "Noti.GoToBed.InWorldDreams";
    private readonly string obsessionNotiLocKey = "Noti.Obsession";
    private readonly string propheticCutSceenName = "PropheticDream";
    private bool IsObsession => GameRoot.Game.Player.Contains("Obsession");
    public void Do()
    {
        Room.Instance.PlayBedButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification
            (GameRoot.Game.StateControl.InWorldDreams, notiLocKey, Notifications.BedShowMessage)) return;
        if (GameRoot.CallNotification
            (IsObsession, obsessionNotiLocKey, Notifications.BedShowMessage)) return;
        GameRoot.Game.GoToBed(ServiceName);
        if(GameRoot.Game.SleepControler.ResultSleep == "Prophetic")
            Turntable.PlayCutSceen(propheticCutSceenName);
        else
            Turntable.PlayCutSceen("Plug");
    }
}
