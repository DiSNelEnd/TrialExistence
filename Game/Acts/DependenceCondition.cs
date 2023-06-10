using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependenceCondition : Condition
{
    private readonly double startTimeTick;
    private bool isOver;
    public override bool IsOver
    {
        get => isOver;
        internal set => isOver=value;
    }

    public DependenceCondition
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
        if (NSPoints > 0) return;
        TimeTick = startTimeTick;
    }

    public override void ReduceTime(double timeInMinutes)
    {
        if (IsOver) return;
        if (timeInMinutes < 0) throw new Exception("Time negative meaning");
        TimeTick -= timeInMinutes;
        if (TimeTick <= 0)
            Apply();
    }

    private void Apply()
    {
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.AddNsPoints(NSPoints);
        GameRoot.Game.ResultData.AddApplyCondition(Name, NSPoints);
        TimeTick = startTimeTick + TimeTick;
    }
}
