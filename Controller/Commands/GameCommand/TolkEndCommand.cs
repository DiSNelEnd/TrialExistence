using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TolkEndCommand : ICommand
{
    private readonly string locKey;
    private readonly int maxTexts;
    public TolkEndCommand(string locKey,int maxTexts)
    {
        this.locKey = locKey;
        this.maxTexts = maxTexts;
    }

    public void Do()
    {
        MenuGame.Instance.PlaySongMenuButton1();
        GameEnd.Instance.SetActualTolk(locKey,maxTexts);
        GameEnd.Instance.ToggleTolk(true);
    }
}
