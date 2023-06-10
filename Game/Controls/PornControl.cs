using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PornControl
{
    private int lookPornInDayCount;
    private int notLookPornDayCount;
    private readonly int maxLoockPornInDay = 1;
    private readonly int maxNotLoockPornDay = 3;
    private readonly string standingName = "Standing";
    private readonly string lustName = "Lust";
    private readonly string abstinenceName = "Abstinence";
    public PornControl(int loockPornInDayCount,int notLoockPornDayCount)
    {
        this.lookPornInDayCount = loockPornInDayCount;
        this.notLookPornDayCount = notLoockPornDayCount;
    }

    public void GoPorn()
    {
        DeleteCondition(abstinenceName);
        lookPornInDayCount++;
        ImposeCondition(standingName);
        if (lookPornInDayCount > maxLoockPornInDay)
            ImposeCondition(lustName);
    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged += CheckLoockPornCount;
    }

    public void SaveData()
    {
        GameDataSaver.Instance.SavePornData(lookPornInDayCount,notLookPornDayCount);
    }

    private void CheckLoockPornCount()
    {
        if (GameRoot.Game.StateControl.Asexual) return;
        if (lookPornInDayCount == 0)
            notLookPornDayCount++;
        else
            notLookPornDayCount = 0;
        if (notLookPornDayCount > maxNotLoockPornDay)
            ImposeCondition(abstinenceName);
        lookPornInDayCount = 0;
    }

    private void DeleteCondition(string name)
    {
        GameRoot.Game.Player.DeleteCondition(name);
    }

    private void ImposeCondition(string conName)
    {
        GameRoot.Game.ImposeCondition(conName);
    }
}
