using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetToWorkCommand : ICommand
{
    private readonly string serviceName = "ProceedWork";
    private readonly string notiLocKey = "Noti.GetToWork.Boss";
    private readonly string obsessionNotiLocKey = "Noti.Obsession";
    private readonly string cutSceenName = "GetToWork";
    private bool isObsession => GameRoot.Game.Player.Contains("Obsession");
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.CallNotification(GameRoot.Game.StateControl.Boss, notiLocKey, Notifications.ComputerShowMessage)) return;
        if (GameRoot.CallNotification
            (isObsession, obsessionNotiLocKey, Notifications.ComputerShowMessage)) return;
        GameRoot.Game.GetToWork(serviceName);
        if(GameRoot.Game.WorkControler.WorkData.WorkStatus==WorkStatus.InWork)
            Turntable.PlayCutSceen(cutSceenName);
    }
}
