using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStandardKitchenButtonCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        EatComButtonsControl.Instance.ToggleStandardKitchen(true);
    }
}
