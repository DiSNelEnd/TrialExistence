using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleResultScreenCommand : ICommand
{
    private Doing doing;
    public ToggleResultScreenCommand(Doing doing)
    {
        this.doing = doing;
    }

    public void Do()
    {
        Room.Instance.ToggleResultScreen(doing == Doing.On);
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.ResultData.ClearImposeApply();
    }
}
