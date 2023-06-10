using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceedGameCommand : ICommand
{
    private readonly string cutSceenName = "StartGame";
    public void Do()
    {
        MenuGame.Instance.PlaySongMenuButton1();
        GameRoot.LoadGame();
        Turntable.PlayCutSceen(cutSceenName);
    }
}
