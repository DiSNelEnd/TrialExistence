using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepControl
{
    private double sleepInDayCount;
    private int notSleepDayCount;
    private double remainingSleep;
    private readonly string serviceSleepName = "Sleep";
    private readonly string overflowName = "Overflow";
    private readonly string lackOfSleepName = "LackOfSleep";
    private readonly string insomniaName = "Insomnia";
    private readonly string sonyaName = "Sonya";
    private readonly double maxSleepInDayCount =600;
    private readonly double minSleepInDayCount = 360;
    private readonly int maxNotSleepDay = 1;
    private readonly double maxSleepForSonya=1440;
    public string ResultSleep;
    public SleepControl(double sleepInDayCount, int notSleepDayCount,double remainingSleep)
    {
        this.sleepInDayCount = sleepInDayCount;
        this.notSleepDayCount = notSleepDayCount;
        this.remainingSleep = remainingSleep;
    }

    public void Sleep(Service service)
    {
        notSleepDayCount = 0;
        CountSleepInDay(service.TimeInMinutes);
        if (service.Name == serviceSleepName)
            GetResultSleep(service);
    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged += CheckSleepCount;
    }

    public void SaveData()
    {
        GameDataSaver.Instance.SaveSleepData(sleepInDayCount, notSleepDayCount, remainingSleep);
    }

    private void CountSleepInDay(double sleepTimeInMinutes)
    {
        var gameTime = GameRoot.Game.Time.TimeInMinutes;
        if (sleepTimeInMinutes + gameTime > maxSleepForSonya)
        {
            var number = sleepTimeInMinutes + gameTime - maxSleepForSonya;
            sleepInDayCount += sleepTimeInMinutes - number;
            remainingSleep = number;
        }
        else
            sleepInDayCount += sleepTimeInMinutes;
    }

    private  void CheckSleepCount()
    {
        if (GameRoot.Game.StateControl.InWorldDreams) return;
        if (sleepInDayCount >= maxSleepForSonya)
            ImposeCondition(sonyaName);
        else
            DeleteCondition(sonyaName);
        if (sleepInDayCount <= minSleepInDayCount)
            ImposeCondition(lackOfSleepName);
        if (sleepInDayCount >= maxSleepInDayCount)
            ImposeCondition(overflowName);
        if (sleepInDayCount == 0)
            notSleepDayCount++;
        if (notSleepDayCount >= maxNotSleepDay)
            ImposeCondition(insomniaName);
        sleepInDayCount = remainingSleep;
        remainingSleep = 0;
    }

    private void GetResultSleep(Service service)
    {
        var random = new RandomResultOfSleep();
        var result = random.GetSesult();
        service.AddRandomBonus(result.Item2);
        ResultSleep = result.Item1;
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
