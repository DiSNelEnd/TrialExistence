using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd
{
    private readonly string gameEndSwitchbleName = "GameEnd";
    private readonly string gameEndLocKey = "GameEnd.";
    private readonly string goToEndCutSceenName = "GoToEnd";
    private readonly string animatorName = "GameEndSprite";
    private readonly string paramName = "Tolk";
    private readonly string musicName = "SCP-x6x";
    private readonly string effectsSoundName = "GameEndEffectsSound";
    private readonly string tolkSoundName = "GameEndTolkSound";
    private string tolkLocKey = "TolkTextStart";
    private bool isRedyGo;
    public string ActualTolkLocKey => gameEndLocKey + tolkLocKey;
    public int ActualTolkMaxTexts { get; private set; }
    public static GameEnd Instance => lazy.Value;
    private static readonly Lazy<GameEnd> lazy =
        new Lazy<GameEnd>(() => new GameEnd());
    private GameEnd() {}

    public void SetActualTolk(string locKey,int maxTexts)
    {
        tolkLocKey = locKey;
        ActualTolkMaxTexts = maxTexts;
        isRedyGo = true;
    }

    public void ToggleTolk(bool flag)
    {
        Turntable.SetBoolAnimator(animatorName, paramName, flag);
    }

    public void GoToEnd()
    {
        if(isRedyGo)
            Turntable.PlayCutSceen(goToEndCutSceenName);
    }

    public void OpenEnd()
    {
        ToggleGameEnd(true);
        PlayMusic();
        PlayEffects();
        ToggleTolk(true);
    }

    public void ToggleGameEnd(bool flag)
    {
        Switcher.Toggle(gameEndSwitchbleName, flag);
    }

    public void PlayTolkSound()
    {
        AudioPlayer.PlayEffectOneShot(tolkSoundName);
    }

    private void PlayMusic()
    {
        AudioPlayer.PlayMusic(musicName);
    }

    private void PlayEffects()
    {
        AudioPlayer.PlayEffectLoop(effectsSoundName);
    }
}
