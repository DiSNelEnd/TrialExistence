using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeVacationCommand : ICommand
{
    private readonly string notiLocKey = "Noti.GetToWork.Boss";
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification
            (GameRoot.Game.StateControl.Boss, notiLocKey,Notifications.ComputerShowMessage)) return;
        GameRoot.Game.WorkControler.WorkData.GetVacation();

    }
}
