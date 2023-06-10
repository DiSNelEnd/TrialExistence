using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSPoints
{
    private const int STEP1 = 3000;
    private const int STEP2 = 7000;
    private const int SCOREFORWIN = 11000;
    private int score;
    private int currentLv;
    public event Action OnLvChanged;
    public event Action OnScoreChanged;
    public NSPoints(int startScore=0)
    {
        score = startScore;
        currentLv = Lv;
        CurtainsSpriteControl.ToggleCurtains(Lv);
    }

    public int Lv => score < STEP1 ? 1 : score <= STEP2 ? 2 : 3;
    public string ScoreText => score.ToString();
    public int Score => score;
    public string LvTag => $".Lv{Lv}";
    public bool IsWin => score > SCOREFORWIN;
    public int CurrentLv => currentLv;
    public void Add(int nsPoints)
    {
        score += nsPoints;
        OnScoreChanged?.Invoke();
        if (currentLv != Lv)
            ChangeCurrentLv(); 
    }

    private void ChangeCurrentLv()
    {
        OnLvChanged?.Invoke();
        currentLv = Lv;
        CurtainsSpriteControl.ToggleCurtains(currentLv);
        Room.Instance.PlayRoomMusic();
    }
}
