using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsControl
{
    private const string BACKGAMEBUTTONNAME = "BackGameButton";
    private const string PROCEEDBUTTONNAME = "ProceedButton";
    public static MenuButtonsControl Instance => lazy.Value;
    private static readonly Lazy<MenuButtonsControl> lazy =
        new Lazy<MenuButtonsControl>(() => new MenuButtonsControl());
    private MenuButtonsControl() { }

    public void ToggleBackGameButton(bool flag)
    {
        Switcher.Toggle(BACKGAMEBUTTONNAME, flag);
    }

    public void ToggleProcesedButton(bool flag)
    {
        Switcher.Toggle(PROCEEDBUTTONNAME, flag);
    }
}
