using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTrainCommand : ICommand
{
    private Doing doing;
    public ToggleTrainCommand(Doing doing)
    {
        this.doing = doing;
    }

    public void Do()
    {
        Room.Instance.ToggleTrain(doing == Doing.On);
    }
}
