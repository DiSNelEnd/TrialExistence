using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HobbyControl
{
    private int hobbyInDayCount;
    private int hobbyDayCount;
    private readonly int maxHobbyInDay=2;
    private readonly int maxHobbyDay = 15;
    private readonly string obsessionName = "Obsession";
    private readonly string specialistName = "Specialist";
    private readonly string fatigueName = "Fatigue";
    private bool IsSpecialist => GameRoot.Game.Player.Contains(specialistName);
    public HobbyControl(int hobbyInDayCount,int hobbyDayCount)
    {
        this.hobbyInDayCount = hobbyInDayCount;
        this.hobbyDayCount = hobbyDayCount;
    }

    public void TookUpHobby()
    {
        hobbyInDayCount++;
        if (hobbyInDayCount > maxHobbyInDay)
            ImposeObsessionAndFatigue();
        GameRoot.Game.WorkControler.ResetNotWorkDayCount();
    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged += CheckHobbyCount;
    }

    public void SaveData()
    {
        GameDataSaver.Instance.SaveHobbyData(hobbyInDayCount, hobbyDayCount);
    }

    private void ImposeObsessionAndFatigue()
    {
        ImposeCondition(obsessionName);
        if (GameRoot.Game.WorkControler.WorkInDayCount>0)
            ImposeCondition(fatigueName);
    }

    private void CheckHobbyCount()
    {
        if (hobbyInDayCount > 0)
            hobbyDayCount++;
        else
            hobbyDayCount = 0;
        if (hobbyDayCount > maxHobbyDay && !IsSpecialist)
            ImposeSpecialist();
        hobbyInDayCount = 0;
    }

    private void ImposeSpecialist()
    {
        ImposeCondition(specialistName);
        hobbyDayCount = 0;
    }

    private void ImposeCondition(string conName)
    {
        GameRoot.Game.ImposeCondition(conName);
    }
}
