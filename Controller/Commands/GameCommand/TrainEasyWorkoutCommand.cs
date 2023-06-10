using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainEasyWorkoutCommand : ICommand
{
    private readonly string serviceName = "EasyWorkout";
    private readonly string locKeyNotiTrauma = "Noti.Trauma";
    private bool IsTrauma => GameRoot.Game.Player.Contains("Trauma");
    public void Do()
    {
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification(IsTrauma, locKeyNotiTrauma, Notifications.TrainShowMessage)) return;
        GameRoot.Game.Train(serviceName);
        Turntable.PlayCutSceen("Plug");
    }
}
