using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEatPageComputerCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        Computer.Instance.ToggleEatPage(true);
    }
}
