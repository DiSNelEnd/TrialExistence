using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCondition : Condition
{
    private readonly double startTimeTick;
    public override bool IsOver
    {
        get => false;
        internal set => throw new NotImplementedException("StatusCondition not set isOver");
    }

    public StatusCondition
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
        return;
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
