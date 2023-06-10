using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBedCommand : ICommand
{
    private Doing doing;
    public ToggleBedCommand(Doing doing)
    {
        this.doing = doing;
    }

    public void Do()
    {
        Room.Instance.ToggleBed(doing == Doing.On);
    }
}
