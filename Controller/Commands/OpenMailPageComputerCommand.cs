using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMailPageComputerCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayMainButtonSound();
        Computer.Instance.ToggleMailPage(true);
    }
}
