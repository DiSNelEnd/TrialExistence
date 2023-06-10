using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalCondition : Condition
{
    private readonly double startTimeTick;
    private bool Intoxication => GameRoot.Game.Player.Contains("Intoxication");
    public override bool IsOver
    {
        get => TimeDuration <= 0;
        internal set => throw new NotImplementedException("CriticalCondition not set isOver");
    }

    public CriticalCondition
        (string name,
        int nsPoints,
        double timeDurationInMinutes,
        double timeTickInMinutes)
        : base(name, nsPoints, timeDurationInMinutes, timeTickInMinutes)
    {
        startTimeTick = timeTickInMinutes;
    }

    public override void Strengthen()
    {
        NSPoints *=2;
    }

    public override void ReduceTime(double timeInMinutes)
    {
        if (IsOver) return;
        if (timeInMinutes < 0) throw new Exception("Time negative meaning");
        TimeDuration -= timeInMinutes;
        TimeTick -= timeInMinutes;
        if (TimeTick <= 0)
            Apply();
    }

    private void Apply()
    {
        if (GameRoot.IsGameNotStart) return;
        var corectNsPoints = NSPoints;
        if (NSPoints < 0 && Intoxication)
            corectNsPoints /= 2;
        GameRoot.Game.AddNsPoints(corectNsPoints);
        GameRoot.Game.ResultData.AddApplyCondition(Name, corectNsPoints);
        TimeTick = startTimeTick+TimeTick;
    }
}
