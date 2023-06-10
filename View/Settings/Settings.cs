using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Settings 
{
    private const string WARNINGTEXTNAME= "WarningText";
    private const string CREDITSPAGENAME = "CreditsPage";
    private readonly string settingPath= Path.Combine(Application.persistentDataPath,"Settings.json");
    private SettingsData settingsData;
    private readonly float startValume = 0.8f;
    public static Settings Instance => lazy.Value;
    private static readonly Lazy<Settings> lazy =
        new Lazy<Settings>(() => new Settings());
    private Settings() { }
    public SettingsData SettingsData => settingsData;

    public void ToggleWarningText(bool flag)
    {
        Switcher.Toggle(WARNINGTEXTNAME, flag);
    }

    public void ToggleCreditsPage(bool flag)
    {
        Switcher.Toggle(CREDITSPAGENAME, flag);
    }

    public void SetResolutionIndex(int value)
    {
        settingsData.indexResolution = value;
    }

    public void SetIsFullscreen(bool value)
    {
        settingsData.isFullscreen = value;
    }

    public void SetLanguageIndex(int value)
    {
        settingsData.indexLanguage = value;
    }

    public void SetValume(float value)
    {
        settingsData.volume = value;
    }

    public void OpenSettings()
    {
        settingsData = JsonSaver.LoadingJson<SettingsData>(settingPath);
    }

    public void ApplySettings()
    {
        JsonSaver.SaveFileJson(settingsData, settingPath);
        Root.Instance.ApplySettings(settingsData);
    }

    public void Start()
    {
        OpenSettings();
        if (settingsData == null)
            CreateStartSettingsData();
        ApplySettings();
    }

    private void CreateStartSettingsData()
    {
        settingsData = new SettingsData();
        SetResolutionIndex(Root.Instance.Resolutions.Length - 1);
        SetIsFullscreen(true);
        SetLanguageIndex(0);
        SetValume(startValume);
    }
}
