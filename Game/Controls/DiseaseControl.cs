using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseControl
{
    private readonly int maxCriticalAndHeroicCount = 3;
    private readonly string gotSickName = "GotSick";
    public DiseaseControl()
    {

    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged +=CheckCritAndHeroicCounts;
    }

    private void CheckCritAndHeroicCounts()
    {
        var critAndHeroic = GameRoot.Game.Player.GetCriticalAndHeroicCounts();
        if (critAndHeroic.Item1 > maxCriticalAndHeroicCount
            || critAndHeroic.Item2 >= maxCriticalAndHeroicCount)
            ImposeCondition(gotSickName);
    }

    private void ImposeCondition(string conName)
    {
        GameRoot.Game.ImposeCondition(conName);
    }
}
