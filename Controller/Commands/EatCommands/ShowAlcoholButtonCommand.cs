using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAlcoholButtonCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        EatComButtonsControl.Instance.ToggleAlcohol(true);
    }
}
