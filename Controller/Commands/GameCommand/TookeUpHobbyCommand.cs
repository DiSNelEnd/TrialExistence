using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TookeUpHobbyCommand : ICommand
{
    private readonly string serviceName = "Hobby";
    private readonly string baffName = "BrainActivity";
    private readonly string debaffName = "SoreHead";
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        var isNoti= GameRoot.Game.TookeUpHobby(serviceName);
        if (isNoti) return;
        if(GameRoot.Game.WorkControler.WorkData.WorkStatus==WorkStatus.InVacation 
            || GameRoot.Game.StateControl.Boss
            || GameRoot.Game.WorkControler.WorkData.WorkStatus == WorkStatus.Dismissal)
            GameRoot.Game.ImposeCondition(baffName);
        if (GameRoot.Game.WorkControler.WorkData.WorkStatus == WorkStatus.InWork && !GameRoot.Game.StateControl.Boss)
            GameRoot.Game.ImposeCondition(debaffName);
        Turntable.PlayCutSceen("Plug");
    }
}
