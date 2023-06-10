using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRelaxPageComputerCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        Computer.Instance.ToggleRelaxPage(true);
    }
}
