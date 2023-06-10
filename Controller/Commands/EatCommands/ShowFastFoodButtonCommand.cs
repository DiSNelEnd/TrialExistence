using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFastFoodButtonCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        EatComButtonsControl.Instance.ToggleFastFood(true);
    }
}
