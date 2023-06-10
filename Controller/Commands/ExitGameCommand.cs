using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameCommand : ICommand
{
    public void Do()
    {
        MenuGame.Instance.PlaySongMenuButton1();
        Root.Instance.ExitGame();
    }
}
