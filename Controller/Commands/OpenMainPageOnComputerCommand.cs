using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMainPageOnComputerCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayMainButtonSound();
        Computer.Instance.ToggleMainPage(true);
    }
}
