using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSettingPageCommand : ICommand
{
    private Doing doing;
    public ToggleSettingPageCommand(Doing doing)
    {
        this.doing = doing;
    }

    public void Do()
    {
        MenuGame.Instance.PlaySongMenuButton1();
        MenuGame.Instance.ToggleSettingPage(doing == Doing.On);
    }
}
