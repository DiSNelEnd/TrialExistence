using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInfPageComputerCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayMainButtonSound();
        Computer.Instance.ToggleInfPage(true);
    }
}
