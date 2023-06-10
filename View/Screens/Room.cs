using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private const string BACKGROUNDMAINNAME = "BackgroundMain";
    private const string MAINSCREENNAME= "Main";
    private const string COMPUTERSCREENNAME = "Computer";
    private const string RESULTSCREENNAME = "ResultScreen";
    private const string BEDSCREENNAME = "Bed";
    private const string TRAINSCREENNAME = "Train";
    private readonly string splashScreenName = "SplashScreen";
    private readonly string roomMusicName = "GameMusicLv";
    private readonly string roomEffectsName = "RoomNoiseSound";
    private readonly string computerEffectsName = "ComputerEffectsSong";
    private readonly string bedEffectsName = "BedEffectsSound";
    private readonly string bedButtonName = "BedButtonSound";
    private readonly string trainEffectsName = "TrainEffectsSound";
    public bool IsFirstComputerStart { get; private set; }
    public static Room Instance => lazy.Value;
    private static readonly Lazy<Room> lazy =
        new Lazy<Room>(() => new Room());
    private Room() { }

    public void OpenMenu()
    {
        ToggleRoom(false);
        MenuGame.Instance.OpenMenuInGame();
    }

    public void ToggleFirstStart(bool flag)
    {
        IsFirstComputerStart = flag;
    }

    public void ToggleResultScreen(bool flag)
    {
        Switcher.Toggle(RESULTSCREENNAME,flag);
    }

    public void ToggleComputer(bool flag)
    {
        Switcher.Toggle(COMPUTERSCREENNAME, flag);
        MainComButtonsControl.Instance.TogglePlayNewLetterAnim(Mail.IsNewLetters);
        MainComButtonsControl.Instance.PressMailButtonAnim();
        ToggleRoom(!flag);
        if (IsFirstComputerStart && flag)
            FirstStartComputer();
        if (flag)
            PlayComputerEffects();
    }

    public void ToggleBed(bool flag)
    {
        Switcher.Toggle(BEDSCREENNAME, flag);
        ToggleRoom(!flag);
        if(flag)
            PlayBedEffects();
    }

    public void ToggleTrain(bool flag)
    {
        Switcher.Toggle(TRAINSCREENNAME, flag);
        ToggleRoom(!flag);
        if(flag)
            PlayTrainEffects();
    }

    public void ToggleRoom(bool flag)
    {
        Switcher.Toggle(BACKGROUNDMAINNAME, flag);
        Switcher.Toggle(MAINSCREENNAME, flag);
        if(flag)
            PlayRoomEffects();
    }

    public void PlayRoomEffects()
    {
        AudioPlayer.PlayEffectLoop(roomEffectsName);
    }

    public void PlayRoomMusic()
    {
        if (GameRoot.IsGameNotStart) return;
        var name= roomMusicName+GameRoot.Game.Ns.Lv.ToString();
        AudioPlayer.PlayMusic(name);
    }

    public void PlayBedButtonSound()
    {
        AudioPlayer.PlayUISound(bedButtonName);
    }

    private void PlayComputerEffects()
    {
        AudioPlayer.PlayEffectLoop(computerEffectsName);
    }

    private void PlayBedEffects()
    {
        AudioPlayer.PlayEffectLoop(bedEffectsName);
    }

    private void PlayTrainEffects()
    {
        AudioPlayer.PlayEffectLoop(trainEffectsName);
    }

    private void FirstStartComputer()
    {
        Turntable.PlayCutSceen(splashScreenName);
        ToggleFirstStart(false);
    }
}
