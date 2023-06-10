using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainHardWorkoutCommand : ICommand
{
    private readonly string serviceName = "HardWorkout";
    private readonly string locKeyNotiTrauma = "Noti.Trauma";
    private readonly string locKeyNotiPulled = "Noti.Pulled";
    private bool IsTrauma => GameRoot.Game.Player.Contains("Trauma");
    private bool IsPulled => GameRoot.Game.Player.Contains("Pulled");
    public void Do()
    {
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification(IsTrauma, locKeyNotiTrauma, Notifications.TrainShowMessage)) return;
        if (GameRoot.CallNotification(IsPulled, locKeyNotiPulled, Notifications.TrainShowMessage)) return;
        GameRoot.Game.Train(serviceName);
        Turntable.PlayCutSceen("Plug");
    }
}
