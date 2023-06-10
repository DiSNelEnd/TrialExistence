using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainComButtonsControl
{
    private const string MAINBUTTONNAME = "MainPageButton";
    private const string INFBUTTONNAME = "InformationButton";
    private const string MAILBUTTONNAME = "MailButton";
    private const string MAINSPRITEBUTTONNAME = "ComputerMainButtonPressed";
    private const string INFSPRITEBUTTONNAME = "ComputerInfButtonPressed";
    private const string MAILSPRITEBUTTONNAME = "ComputerMailButtonPressed";
    private const string MAINLIGHTBULBNAME = "LightBulbMain";
    private const string INFLIGHTBULBNAME = "LightBulbInf";
    private const string MAILLIGHTBULBNAME = "LightBulbMail";
    private static readonly string animatorName = "Sprites";
    private static readonly string paramsAnimNewLetterName = "NewLetter";
    private static readonly string paramsAnimPressButtonName = "PressButton";
    private bool isPressMailButton;

    public static MainComButtonsControl Instance => lazy.Value;
    private static readonly Lazy<MainComButtonsControl> lazy =
        new Lazy<MainComButtonsControl>(() => new MainComButtonsControl());
    private MainComButtonsControl() { }
    public void PressMainButton(bool flag)
    {
        Switcher.Toggle(MAINBUTTONNAME,!flag);
        Switcher.Toggle(MAINSPRITEBUTTONNAME, flag);
        Switcher.Toggle(MAINLIGHTBULBNAME, flag);
    }

    public void PressInfButton(bool flag)
    {
        Switcher.Toggle(INFBUTTONNAME, !flag);
        Switcher.Toggle(INFSPRITEBUTTONNAME, flag);
        Switcher.Toggle(INFLIGHTBULBNAME, flag);
    }

    public void PressMailButton(bool flag)
    {
        Switcher.Toggle(MAILBUTTONNAME, !flag);
        Switcher.Toggle(MAILSPRITEBUTTONNAME, flag);
        isPressMailButton = flag;
        PressMailButtonAnim();
    }

    public void ShowButtons()
    {
        Switcher.Toggle(MAINBUTTONNAME, true);
        Switcher.Toggle(INFBUTTONNAME, true);
        Switcher.Toggle(MAILBUTTONNAME, true);
    }

    public void TogglePlayNewLetterAnim(bool flag)
    {
        Turntable.SetBoolAnimator(animatorName, paramsAnimNewLetterName, flag);
    }

    public void PressMailButtonAnim()
    {
        Turntable.SetBoolAnimator(animatorName, paramsAnimPressButtonName, isPressMailButton);
    }
}
