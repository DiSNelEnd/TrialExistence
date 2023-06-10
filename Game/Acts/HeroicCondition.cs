using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicCondition : Condition
{
    private readonly bool isMinimum;
    private readonly double startTimeTick;
    private bool isOver;
    private readonly int minNsPoint = -5;
    private readonly int gainCount = 10;
    public HeroicCondition
        (string name,
        int nsPoints,
        double timeDurationInMinutes,
        double timeTickInMinutes)
        : base(name, nsPoints, timeDurationInMinutes, timeTickInMinutes)
    {

        isMinimum = nsPoints == minNsPoint;
        startTimeTick = timeTickInMinutes;
    }

    public override bool IsOver 
    { 
        get => isOver;
        internal set => isOver = value; 
    }

    public override void ReduceTime(double timeInMinutes)
    {
        if (IsOver) return;
        if (timeInMinutes < 0) throw new Exception("Time negative meaning");
        TimeTick -= timeInMinutes;
        if (TimeTick <= 0)
            Apply();
    }

    public override void Strengthen()
    {
        HeroicDayCount++;
        if (HeroicDayCount % gainCount == 0)
            NSPoints = isMinimum ? NSPoints * 2 : NSPoints / 2;
        if (HeroicDayCount == 3*gainCount)
            ImposeState();
    }

    private void ImposeState()
    {
        if (!GameRoot.IsGameNotStart)
            GameRoot.Game.StateControl.EndedHeroic(Name);
        isOver = true;
    }

    private void Apply()
    {
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.AddNsPoints(NSPoints);
        GameRoot.Game.ResultData.AddApplyCondition(Name, NSPoints);
        TimeTick = startTimeTick+TimeTick;
    }
}
