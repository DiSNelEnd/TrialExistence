using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TunerCutSceens
{
    private Dictionary<string, Action<CutSceen>> actions;
    private bool isPlayed;
    private readonly string notiLocKey = "Noti.ScipCutSceen";

    public TunerCutSceens()
    {
        InitialAction();
    }

    public void TuningCuteSceens()
    {
        var cutSceens = CutSceensRepositiry.GetAll();
        foreach(var cutSceen in cutSceens)
        {
            var action = GetAction(cutSceen.type);
            action(cutSceen);
        }
    }

    private void SetPlayedStoppedCutSceen(
        CutSceen cutSceen,
       Action<PlayableDirector> played = null,
       Action<PlayableDirector> stopped = null)
    {
        cutSceen.PlayableDirector.played += played;
        cutSceen.PlayableDirector.stopped += stopped;
    }

    private void SetPlugCutSceen(CutSceen cutSceen)
    {
        SetPlayedStoppedCutSceen(cutSceen,
           (obj) =>
           {
               StartCutSceen(cutSceen.Name);
           },
           (obj) =>
           {
               Room.Instance.ToggleBed(false);
               Room.Instance.ToggleTrain(false);
               Room.Instance.ToggleComputer(false);
               Room.Instance.ToggleResultScreen(false);
               Room.Instance.ToggleResultScreen(true);
               StopCutSceen(cutSceen.Name);
           });
    }

    private void SetComputerCutSceen(CutSceen cutSceen)
    {
        SetPlayedStoppedCutSceen(cutSceen,
            (obj) =>
            {
                StartCutSceen(cutSceen.Name);
                CutSceenSoundsControl.Instance.PlaySoundEffect(cutSceen.Name);
            },
            (obj) =>
            {
                CutSceenSpritesControl.Instance.ToggleSprite(cutSceen.Name, false);
                Room.Instance.ToggleComputer(false);
                Room.Instance.ToggleResultScreen(false);
                Room.Instance.ToggleResultScreen(true);
                Root.Instance.ResetCamera();
                StopCutSceen(cutSceen.Name);
            });
    }

    private void SetStartGameCutSceen(CutSceen cutSceen)
    {
        SetPlayedStoppedCutSceen(cutSceen,
            (obj) =>
            {
                MenuGame.Instance.ToggleMenuPage(false);
                MenuGame.Instance.BeginGameAnimStart(true);
                MenuGame.Instance.BeginMusicPlay();
                CursorLock(true);
            },
            (obj) =>
            {
                MenuButtonsControl.Instance.ToggleBackGameButton(true);
                MenuGame.Instance.NewGame();
                CursorLock(false);
            });
    }

    private void SetStartComputerCutSceen(CutSceen cutSceen)
    {
        SetPlayedStoppedCutSceen(cutSceen,
            (obj) =>
            {
                CutSceenSoundsControl.Instance.PlaySoundEffect(cutSceen.Name);
                CursorLock(true);
            },
            (obj) =>
            {
                CutSceenSpritesControl.Instance.ToggleSprite(cutSceen.Name, false);
                CursorLock(false);
                Computer.Instance.ToggleFirstPage(true);
            });
    }

    private void SetDeathCutSceen(CutSceen cutSceen)
    {
        SetPlayedStoppedCutSceen(cutSceen,
        (obj) =>
        {
            Room.Instance.ToggleResultScreen(false);
            Root.Instance.Music.Pause();
            CutSceenSoundsControl.Instance.PlaySoundEffect(cutSceen.Name);
            CursorLock(true);
        }, 
        (obj) => 
        {
            GameDataSaver.Instance.DeleteAllSaves();
            GameRoot.GameOver();
            Room.Instance.ToggleRoom(false);
            MenuGame.Instance.OpenMenuStartGame();
            CursorLock(false);
        });
    }

    private void SetWinGameCutSceen(CutSceen cutSceen)
    {
        SetPlayedStoppedCutSceen(cutSceen,
        (obj) =>
        {
            Room.Instance.ToggleResultScreen(false);
            Root.Instance.Music.Stop();
            Root.Instance.Effects.Stop();
            CutSceenSoundsControl.Instance.PlaySoundEffect(cutSceen.Name);
            CursorLock(true);
        },
        (obj) =>
        {
            GameEnd.Instance.OpenEnd();
            Room.Instance.ToggleRoom(false);
            CursorLock(false);
        });
    }

    private void SetGoToEndCutSceen(CutSceen cutSceen)
    {
        SetPlayedStoppedCutSceen(cutSceen,
        (obj) =>
        {
            CursorLock(true);
            GameDataSaver.Instance.DeleteAllSaves();
            Root.Instance.Music.Stop();
            Root.Instance.Effects.Stop();
            CutSceenSoundsControl.Instance.PlaySoundEffect(cutSceen.Name);
            GameRoot.GameOver();
        },
        (obj) =>
        {
            GameEnd.Instance.ToggleGameEnd(false);
            MenuGame.Instance.OpenMenuStartGame();
            CursorLock(false);
        });
    }

    private void SetSleepCutSceen(CutSceen cutSceen)
    {
        SetPlayedStoppedCutSceen(cutSceen,
            (obj) =>
            {
                StartCutSceen(cutSceen.Name);
                CutSceenSoundsControl.Instance.PlaySoundEffect(cutSceen.Name);
            },
            (obj) =>
            {
                CutSceenSpritesControl.Instance.ToggleSprite(cutSceen.Name, false);
                Room.Instance.ToggleBed(false);
                Room.Instance.ToggleResultScreen(false);
                Room.Instance.ToggleResultScreen(true);
                Root.Instance.ResetCamera();
                StopCutSceen(cutSceen.Name);
            });
    }

    private void StartCutSceen(string name)
    {
        isPlayed = true;
        Root.Instance.StopCutSceen += () => OnCutSceenStoped(name);
        Root.Instance.Music.Pause();
        CursorLock(true);
        GameRoot.Game.SaveGame();
        GameRoot.CallNotification(isPlayed,notiLocKey,Notifications.CutSceenShowMessage);
    }

    private void StopCutSceen(string name)
    {
        isPlayed=false;
        Root.Instance.StopCutSceen -= () => OnCutSceenStoped(name);
        CursorLock(false);
        Root.Instance.Music.Play();
        if (GameRoot.Game.CheckDeath()) return;
        if (GameRoot.Game.CheckWin()) return;
        PlayerAnimationControl.Instance.PlayPlayerAnimation(name);
    }

    private void CursorLock(bool flag)
    {
        if(flag)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    private void InitialAction()
    {
        actions = new Dictionary<string, Action<CutSceen>>()
        {
            {"StartGame",SetStartGameCutSceen},
            {"Food", SetComputerCutSceen},
            {"Relax", SetComputerCutSceen},
            {"Plug", SetPlugCutSceen },
            {"StartComputer", SetStartComputerCutSceen },
            {"Death", SetDeathCutSceen },
            {"WinGame", SetWinGameCutSceen },
            {"GoToEnd", SetGoToEndCutSceen },
            {"Work", SetComputerCutSceen },
            {"Sleep", SetSleepCutSceen }
        };
    }

    private void OnCutSceenStoped(string cutSceenName)
    {
        if (!isPlayed) return;
        AudioPlayer.StopEffectSound();
        ScipCutSceen();
        Turntable.StopCutSceen(cutSceenName);
    }

    private void ScipCutSceen()
    {
        GameRoot.Game.ScipCutSceen();
    }

    private Action<CutSceen> GetAction(string cutSceenType)
    {
        return actions.TryGetValue(cutSceenType, out var result)
            ? result : throw new KeyNotFoundException("Tuning action not found");
    }
}
