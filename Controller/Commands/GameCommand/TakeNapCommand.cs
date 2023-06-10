using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeNapCommand : ICommand
{
    private readonly string serviceName = "Nap";
    private readonly string notiLocKey = "Noti.GoToBed.InWorldDreams";
    private readonly string obsessionNotiLocKey = "Noti.Obsession";
    private bool IsObsession => GameRoot.Game.Player.Contains("Obsession");
    public void Do()
    {
        Room.Instance.PlayBedButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification
            (GameRoot.Game.StateControl.InWorldDreams, notiLocKey, Notifications.BedShowMessage)) return;
        if (GameRoot.CallNotification
            (IsObsession, obsessionNotiLocKey, Notifications.BedShowMessage)) return;
        GameRoot.Game.GoToBed(serviceName);
        Turntable.PlayCutSceen("Plug");
    }
}
