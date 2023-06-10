using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDessertButtonCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        EatComButtonsControl.Instance.ToggleDessert(true);
    }
}
