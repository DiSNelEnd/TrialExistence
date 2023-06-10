using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceCondition : Condition
{
    private readonly double startDuration;
    public InfluenceCondition
       (string name,
       int nsPoints,
       double timeDurationInMinutes,
       double timeTickInMinutes)
       : base(name, nsPoints, timeDurationInMinutes, timeTickInMinutes)
    {
        startDuration = timeDurationInMinutes;
    }

    public override bool IsOver 
    {
        get => TimeDuration <= 0;
        internal set => throw new NotImplementedException("InfluenceCondition not set isOver"); 
    }

    public override void ReduceTime(double timeInMinutes)
    {
        if (IsOver) return;
        if (timeInMinutes < 0) throw new Exception("Time negative meaning");
        TimeDuration -= timeInMinutes;
    }

    public override void Strengthen()
    {
        TimeDuration = startDuration;
    }
}
