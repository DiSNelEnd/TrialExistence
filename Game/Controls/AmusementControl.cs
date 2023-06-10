using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmusementControl
{
    private int differentPlayCount;
    private int differentSeeCount;
    private double timeFunInDay;
    private int notFunDayCount;
    private readonly string playServiceName = "PlayGame";
    private readonly string seeServiceName = "SeeSomething";
    private readonly int maxDifferentFunCountInDay = 1;
    private readonly double maxTimeFunInDay = 720;
    private readonly int maxNotFunDay = 3;
    private readonly string tiredName = "Tired";
    private readonly string uselessName = "Useless";
    private readonly string boredomName = "Boredom";
    public AmusementControl(
        double timeFunInDay,
        int notFunDayCount,
        int differentPlayCount, 
        int differentSeeCount)
    {
        this.differentSeeCount = differentSeeCount;
        this.differentPlayCount = differentPlayCount;
        this.timeFunInDay = timeFunInDay;
        this.notFunDayCount = notFunDayCount;
    }

    public void Fun(Service service)
    {
        DeleteCondition(boredomName);
        CountDifferentFun(service.Name);
        timeFunInDay += service.TimeInMinutes;
        if (timeFunInDay > maxTimeFunInDay)
            ImposeCondition(uselessName);
    }

    public void SaveData()
    {
        GameDataSaver.Instance.SaveAmusementData
            (timeFunInDay,notFunDayCount,differentPlayCount, differentSeeCount);
    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged += CheckFunCount;
    }

    private void CountDifferentFun(string serviceName)
    {
        if (serviceName == playServiceName)
            differentPlayCount++;
        if (serviceName == seeServiceName)
            differentSeeCount++;
        if (differentPlayCount > maxDifferentFunCountInDay 
            || differentSeeCount > maxDifferentFunCountInDay)
            ImposeCondition(tiredName);
    }

    private void CheckFunCount()
    {
        ImposeBoredom();
        timeFunInDay = 0;
        differentPlayCount = 0;
        differentSeeCount = 0;
    }

    private void ImposeBoredom()
    {
        if (GameRoot.Game.StateControl.Flint) return;
        if (timeFunInDay == 0)
            notFunDayCount++;
        else
            notFunDayCount = 0;
        if (notFunDayCount > maxNotFunDay)
            ImposeCondition(boredomName);
    }

    private void ImposeCondition(string conName)
    {
        GameRoot.Game.ImposeCondition(conName);
    }

    private void DeleteCondition(string name)
    {
        GameRoot.Game.Player.DeleteCondition(name);
    }
}
