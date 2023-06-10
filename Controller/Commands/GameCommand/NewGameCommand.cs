using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameCommand : ICommand
{
    private readonly int startNspoints=100;
    private readonly int startMoney = 100;
    private readonly double startTimeInMinute = 0;
    private readonly string notiLocKey = "Noti.Menu.NewGame";
    private readonly string cutSceenName = "StartGame";
    public void Do()
    {
        MenuGame.Instance.PlaySongMenuButton1();
        Action action = () =>
        {
            GameRoot.StartGame(startNspoints, startMoney, startTimeInMinute);
            Turntable.PlayCutSceen(cutSceenName);
        };
        if (GameDataSaver.Instance.IsSaved)
            GameRoot.CallNotification(MenuGame.Instance.SaveAction(action), notiLocKey, Notifications.MenuShowMessage);
        else
            action?.Invoke();
    }
}
