using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialControl 
{
    private double talkTimeInDay;
    private int notTalkDayCount;
    private readonly double maxTalkTimeInDay = 480;
    private readonly int maxNotTalkDay = 3;
    private readonly string burnoutName = "Burnout";
    private readonly string lonelinessName = "Loneliness";
    private bool IsBurnout=>GameRoot.Game.Player.Contains(burnoutName);
    public SocialControl(double talkTimeInDay,int notTalkDayCount)
    {
        this.talkTimeInDay = talkTimeInDay;
        this.notTalkDayCount = notTalkDayCount;
    }

    public void Talk(Service service)
    {
        DeleteCondition(lonelinessName);
        talkTimeInDay += service.TimeInMinutes;
        if (talkTimeInDay > maxTalkTimeInDay || IsBurnout)
            ImposeCondition(burnoutName);
    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged += CheckSocialCount;
    }

    public void SaveData()
    {
        GameDataSaver.Instance.SaveSocialData(talkTimeInDay,notTalkDayCount);
    }

    private void ImposeCondition(string conName)
    {
        GameRoot.Game.ImposeCondition(conName);
    }

    private void DeleteCondition(string name)
    {
        GameRoot.Game.Player.DeleteCondition(name);
    }

    private void CheckSocialCount()
    {
        if (GameRoot.Game.StateControl.Schizo) return;
        if (talkTimeInDay == 0)
            notTalkDayCount++;
        else
            notTalkDayCount = 0;
        if (notTalkDayCount > maxNotTalkDay)
            ImposeCondition(lonelinessName);
        talkTimeInDay = 0;
    }
}
