using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowProperNutritionButtonCommand : ICommand
{
    public void Do()
    {
        Computer.Instance.PlayButtonSound();
        EatComButtonsControl.Instance.ToggleProperNutrition(true);
    }
}
