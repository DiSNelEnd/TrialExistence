using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCreditsPageCommand : ICommand
{
    private Doing doing;
    public ToggleCreditsPageCommand(Doing doing)
    {
        this.doing = doing;
    }

    public void Do()
    {
        Settings.Instance.ToggleCreditsPage(doing == Doing.On);
    }

}
