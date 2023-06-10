using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySettingsCommand : ICommand
{
    public void Do()
    {
        MenuGame.Instance.PlaySongMenuButton1();
        Settings.Instance.ApplySettings();
        Settings.Instance.ToggleWarningText(false);
    }
}
