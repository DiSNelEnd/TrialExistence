using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Computer
{
    private const string MAINPAGENAME = "MainPage";
    private const string FIRSTPAGENAME = "FirstPage";
    private const string INFPAGENAME = "Information";
    private const string EATPAGENAME = "Eat";
    private const string WORKPAGENAME = "Work";
    private const string RELAXPAGENAME = "Relax";
    private const string MAILPAGENAME = "Mail";
    private readonly string buttonSoundName = "ComputerButtonSound";
    private readonly string mainButtonSoundName = "ComputerMainButtonSound";
    private readonly string foodPrinterButtonSoundName = "FoodPrinterButtonSound";
    private Action offToggle;
    public static Computer Instance => lazy.Value;
    private static readonly Lazy<Computer> lazy =
        new Lazy<Computer>(() => new Computer());
    private Computer() { }

    public void ToggleWorkPage(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleWorkPage(false));
        Switcher.Toggle(WORKPAGENAME, flag);
    }

    public void ToggleRelaxPage(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleRelaxPage(false));
        Switcher.Toggle(RELAXPAGENAME, flag);
    }

    public void ToggleEatPage(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleEatPage(false));
        Switcher.Toggle(EATPAGENAME, flag);   
    }

    public void ToggleFirstPage(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleFirstPage(false));
        Switcher.Toggle(FIRSTPAGENAME, flag);
    }

    public void ToggleInfPage(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleInfPage(false));
        Switcher.Toggle(INFPAGENAME, flag);
        MainComButtonsControl.Instance.PressInfButton(flag);
        EatComButtonsControl.Instance.OffToggleEatButton?.Invoke();
    }

    public void ToggleMailPage(bool flag)
    {
        if (flag)
            SaveOffToggle(() => ToggleMailPage(false));
        Switcher.Toggle(MAILPAGENAME, flag);
        MainComButtonsControl.Instance.PressMailButton(flag);
        EatComButtonsControl.Instance.OffToggleEatButton?.Invoke();
    }

    public void ToggleMainPage(bool flag)
    {
        if (flag)
            SaveOffToggle(()=>ToggleMainPage(false));
        Switcher.Toggle(MAINPAGENAME, flag);
        MainComButtonsControl.Instance.PressMainButton(flag);
        EatComButtonsControl.Instance.OffToggleEatButton?.Invoke();
    }

    public void PlayMainButtonSound()
    {
        AudioPlayer.PlayUISound(mainButtonSoundName);
    }

    public void PlayButtonSound()
    {
        AudioPlayer.PlayUISound(buttonSoundName);
    }

    public void PlayFoodPrinterButtonSound()
    {
        AudioPlayer.PlayUISound(foodPrinterButtonSoundName);
    }

    private void SaveOffToggle(Action offToggle)
    {
        this.offToggle?.Invoke();
        this.offToggle = offToggle;
    }
}
