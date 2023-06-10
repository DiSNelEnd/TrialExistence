using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaffCondition : Condition
{
    private readonly int startNsPoints;
    private readonly double startTimeTick;
    private bool isOver;
    public override bool IsOver 
    {
        get => isOver;
        internal set => isOver=value;
    }

    public BaffCondition
        (string name,
        int nsPoints,
        double timeDurationInMinutes,
        double timeTickInMinutes)
        : base(name,nsPoints, timeDurationInMinutes, timeTickInMinutes) 
    {
        startNsPoints = nsPoints;
        startTimeTick = timeTickInMinutes;
    }

    public override void Strengthen()
    {
        NSPoints += startNsPoints;
    }

    public override void ReduceTime(double timeInMinutes)
    {
        if (IsOver) return;
        if (timeInMinutes < 0) throw new Exception("Time negative meaning");
        TimeDuration-= timeInMinutes;
        if (TimeDuration <= 0) IsOver = true;
        TimeTick -= timeInMinutes;
        if (TimeTick <= 0)
            Apply();
    }

    private void Apply()
    {
        if (GameRoot.IsGameNotStart) return;
        var corectNsPoints = NSPoints;
        GameRoot.Game.AddNsPoints(corectNsPoints);
        GameRoot.Game.ResultData.AddApplyCondition(Name, corectNsPoints);
        TimeTick = startTimeTick + TimeTick;
    }
}
