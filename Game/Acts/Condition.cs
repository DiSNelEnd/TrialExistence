using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
    public const string LOCNAME = "ConName.";
    public const string LOCDESCRIPT = "ConDescription.";
    public string Name { get; }
    public int NSPoints { get; internal set; }
    public double TimeDuration { get; internal set; }
    public double TimeTick { get; internal set; }
    public abstract bool IsOver { get; internal set; }
    public int HeroicDayCount { get; internal set; }
    public Condition(string name,int nsPoints,double timeDurationInMinutes,double timeTickInMinutes)
    {
        Name = name;
        NSPoints = nsPoints;
        TimeDuration = timeDurationInMinutes;
        TimeTick = timeTickInMinutes;
    }

    public void Delete()
    {
        IsOver = true;
    }

    public void SetParams(int nsPoints, double timeDurationInMinutes, double timeTickInMinutes,int dayCount)
    {
        NSPoints = nsPoints;
        TimeDuration = timeDurationInMinutes;
        TimeTick = timeTickInMinutes;
        HeroicDayCount = dayCount;
    }

    public void Impose()
    {
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.Player.AddCondition(this);
    }

    public abstract void ReduceTime(double timeInMinutes);
    public abstract void Strengthen();
    public static string CorrectFormat(Tuple<int, int> hm)
    {
        return hm.Item1.ToString() + ":" + GameTime.CorrectFormat(hm.Item2);
    }
}
