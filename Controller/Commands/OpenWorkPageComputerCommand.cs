using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorkPageComputerCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        Computer.Instance.ToggleWorkPage(true);
    }
}
