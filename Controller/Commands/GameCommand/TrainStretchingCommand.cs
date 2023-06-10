using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStretchingCommand : ICommand
{
    private readonly string serviceName = "Stretching";
    private readonly string locKeyNotiPulled = "Noti.Pulled";
    private bool IsPulled => GameRoot.Game.Player.Contains("Pulled");
    public void Do()
    {
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification(IsPulled, locKeyNotiPulled, Notifications.TrainShowMessage)) return;
        GameRoot.Game.Train(serviceName);
        Turntable.PlayCutSceen("Plug");
    }
}
