using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CutSceenSoundsControl
{
    private readonly string playGameMenuWalkSound = "PlayGameMenuWalkSong";
    private readonly string playGameMenuSelectSound = "PlayGameMenuSelectSong";
    private readonly string playGameJumpSound = "PlayGameJumpSound";
    private readonly string playGameDeadSound = "PlayGamePlayerDeadSound";
    private Dictionary<string, string> cutSceenSoundNames;
    public static CutSceenSoundsControl Instance => lazy.Value;
    private static readonly Lazy<CutSceenSoundsControl> lazy =
        new Lazy<CutSceenSoundsControl>(() => new CutSceenSoundsControl());
    private CutSceenSoundsControl()
    {
        InitialSoundName();
    }

    public void PlaySoundEffect(string cutSceenName)
    {
        var soundName = GetSoundName(cutSceenName);
        if (soundName is null) return;
        AudioPlayer.PlayEffectOneShot(soundName);
    }

    private void InitialSoundName()
    {
        cutSceenSoundNames = new Dictionary<string, string>()
        {
            {"CreateFastFood","FoodCutSceenSound" },
            {"CreateStandardKitchen","FoodCutSceenSound" },
            {"CreateProperNutrition","FoodCutSceenSound" },
            {"CreateDessert","FoodCutSceenSound" },
            {"CreateAlcohol", "FoodCutSceenSound" },
            {"SplashScreen","StartComputerCutSceenSound" },
            {"FunPlayGame","PlayGameCutSceenSound" },
            {"FunSeeSomething","SeeSomethingCutSceenSound" },
            {"DeathHanging","DeathHangingCutSceenSound" },
            {"WinGame","WinCutSceenSound" },
            {"GoToEnd","GoToEndCutSceenSound" },
            {"GetToWork","GetToWorkCutSceenSound" },
            {"PropheticDream", "SleepPropheticCutSceen" }
             
        };
    }

    private string GetSoundName(string cutSceenName)
    {
        return cutSceenSoundNames.TryGetValue(cutSceenName, out var result)
            ? result : null;
    }

    public void PlaySongPlayGameMenuWalk()
    {
        AudioPlayer.PlayUISound(playGameMenuWalkSound);
    }

    public void PlaySongPlayGameMenuSelect()
    {
        AudioPlayer.PlayUISound(playGameMenuSelectSound);
    }

    public void PlaySongPlayGameJump()
    {
        AudioPlayer.PlayEffectOneShot(playGameJumpSound);
    }

    public void PlaySongPlayGameDead()
    {
        AudioPlayer.PlayEffectOneShot(playGameDeadSound);
    }
}
