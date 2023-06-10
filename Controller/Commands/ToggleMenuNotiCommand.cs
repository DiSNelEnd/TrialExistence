using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenuNotiCommand : ICommand
{
    private readonly Doing doing;
    public ToggleMenuNotiCommand(Doing doing)
    {
        this.doing = doing;
    }

    public void Do()
    {
        MenuGame.Instance.PlaySongMenuButton1();
        MenuGame.Instance.PressNoti(doing == Doing.On);
    }
}
