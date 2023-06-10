using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleComputerCommand : ICommand
{
    private Doing doing;
    public ToggleComputerCommand(Doing doing)
    {
        this.doing = doing;
    }

    public void Do()
    {
        Room.Instance.ToggleComputer(doing == Doing.On);
    }
}
