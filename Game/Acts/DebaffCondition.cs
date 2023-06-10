using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebaffCondition : Condition
{
    private readonly int startNsPoints;
    private readonly double startTimeTick;
    private bool Intoxication => GameRoot.Game.Player.Contains("Intoxication");
    private bool isOver;
    public bool IsProviso { get; private set; }
    public override bool IsOver
    {
        get => isOver;
        internal set => isOver=value;
    }

    public DebaffCondition
        (string name,
        int nsPoints,
        double timeDurationInMinutes,
        double timeTickInMinutes)
        : base(name, nsPoints, timeDurationInMinutes, timeTickInMinutes)
    {
        startNsPoints = nsPoints;
        startTimeTick = timeTickInMinutes;
        if (timeDurationInMinutes == 0) IsProviso = true;
    }

    public override void Strengthen()
    {
        NSPoints += startNsPoints;
    }

    public override void ReduceTime(double timeInMinutes)
    {
        if (IsOver) return;
        if (timeInMinutes < 0) throw new Exception("Time negative meaning");
        if(!IsProviso) TimeDuration -= timeInMinutes;
        if (!IsProviso && TimeDuration <= 0 ) IsOver = true;
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
