using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGame
{
    private const string MENUBACKNAME = "MenuBackground";
    private const string MENUPAGENAME= "MenuPage";
    private const string SETTINGPAGENAME = "SettingsPage";
    private const string BEGINBOOLNAME = "BeginGame";
    private const string INGAMEBOOLNAME = "InGame";
    private const string BACKGAMEBUTTONNAME = "BackGameButton";
    private const string PROCEEDBUTTONNAME = "ProceedButton";
    private const  string MENUNOTINAME = "MenuNotification";
    private readonly string mainMusicName = "Atlantis";
    private readonly string beginMusicName = "SCPx2x";
    private readonly string inGameMeusicName = "InGameMenuMusic";
    private readonly string inGameEffectName = "InGameMenuSong";
    private readonly string startMenuIdleLampSong0 = "StartMenuIdleLamp0";
    private readonly string startMenuIdleLampSong1 = "StartMenuIdleLamp1";
    private readonly string beginGameLampSong0 = "BeginLamp0";
    private readonly string beginGameLampSong1 = "BeginLamp1";
    private readonly string menuButtonSong0 = "MenuButtonSound0";
    private readonly string menuButtonSong1 = "MenuButtonSound1";
    private readonly string openAndCloseDoorSound = "OpenAndCloseDoor";
    private readonly string openDoorInGameSound = "OpenDoorInGame";
    private Action delayedAction;
    public static MenuGame Instance => lazy.Value;
    private static readonly Lazy<MenuGame> lazy =
        new Lazy<MenuGame>(() => new MenuGame());
    private MenuGame() { }

    public void NewGame()
    {
        ToggleMenu(false);
        Room.Instance.ToggleRoom(true);
        Room.Instance.PlayRoomMusic();
        PlaySoundOpenAndCloseDoor();
    }

    public void OpenMenuInGame()
    {
        ToggleMenu(true);
        Turntable.SetBoolAnimator(MENUBACKNAME, INGAMEBOOLNAME, true);
        ToggleProceedButton(false);
        ToggleBackGameButton(true);
        AudioPlayer.PlayMusic(inGameMeusicName);
        PlaySoundOpenDoorInGame();
    }

    public void ToggleSettingPage(bool flag)
    {
        Switcher.Toggle(SETTINGPAGENAME, flag);
        ToggleMenuPage(!flag);
        Settings.Instance.OpenSettings();
        Settings.Instance.ToggleWarningText(false);
        Settings.Instance.ToggleCreditsPage(false);
    }

    public void ToggleMenuPage(bool flag)
    {
        Switcher.Toggle(MENUPAGENAME, flag);
    }

    public void OpenMenuStartGame()
    {
        ToggleMenu(true);
        ToggleProceedButton(GameDataSaver.Instance.IsSaved);
        ToggleBackGameButton(false);
        AudioPlayer.StopEffectSound();
        AudioPlayer.PlayMusic(mainMusicName);
    }
    
    public void ToggleMenuNotification(bool flag)
    {
        Switcher.Toggle(MENUNOTINAME, flag);
    }

    public bool  SaveAction(Action action)
    {
        delayedAction = action;
        return true;
    }

    public void PressNoti(bool flag)
    {
        if(flag) 
            delayedAction?.Invoke();
        ToggleMenuNotification(false);
    }

    public void ToggleMenu(bool flag)
    {
        Switcher.Toggle(MENUBACKNAME, flag);
        ToggleMenuPage(flag);
    }

    public void BeginGameAnimStart(bool flag)
    {
        Turntable.SetBoolAnimator(MENUBACKNAME, BEGINBOOLNAME, flag);
    }

    public void PlayInGameEffect()
    {
        AudioPlayer.PlayEffectLoop(inGameEffectName);
    }

    public void BeginMusicPlay()
    {
        AudioPlayer.PlayMusic(beginMusicName);
    }

    public void PlaySoundStartMenuLamp0()
    {
        AudioPlayer.PlayEffectOneShot(startMenuIdleLampSong0);
    }

    public void PlaySoundStartMenuLamp1()
    {
        AudioPlayer.PlayEffectOneShot(startMenuIdleLampSong1);
    }

    public void PlaySoundBeginLamp0()
    {
        AudioPlayer.PlayEffectOneShot(beginGameLampSong0);
    }

    public void PlaySoundBeginLamp1()
    {
        AudioPlayer.PlayEffectOneShot(beginGameLampSong1);
    }

    public void PlaySongMenuButton0()
    {
        AudioPlayer.PlayUISound(menuButtonSong0);
    }

    public void PlaySongMenuButton1()
    {
        AudioPlayer.PlayUISound(menuButtonSong1);
    }

    public void PlaySoundOpenAndCloseDoor()
    {
        AudioPlayer.PlayEffectOneShot(openAndCloseDoorSound);
    }

    public void PlaySoundOpenDoorInGame()
    {
        AudioPlayer.PlayEffectOneShot(openDoorInGameSound);
    }

    private void ToggleProceedButton(bool flag)
    {
        Switcher.Toggle(PROCEEDBUTTONNAME, flag);
    }

    private void ToggleBackGameButton(bool flag)
    {
        Switcher.Toggle(BACKGAMEBUTTONNAME, flag);
    }
}
