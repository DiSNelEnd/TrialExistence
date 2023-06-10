using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service
{
    private readonly int nsPointsLv1;
    private readonly int nsPointsLv2;
    private readonly int nsPointsLv3;
    public string Name { get; private set; }
    public int NsPoints => GetNsPoints();
    public int BonusNsPoints { get; private set; }
    public int BonusMoney { get; private set; }
    public int Money { get; private set; }
    public double TimeInMinutes { get; private set; }
    public Service(
        string name,
        int nsPointsLv1,
        int nsPointsLv2,
        int nsPointsLv3,
        int money, 
        double timeInMinutes)
    {
        Name = name;
        this.nsPointsLv1 = nsPointsLv1;
        this.nsPointsLv2 = nsPointsLv2;
        this.nsPointsLv3 = nsPointsLv3;
        Money = money;
        TimeInMinutes = timeInMinutes;
    }

    public void Apply()
    {

        GameRoot.Game.AddNsPoints(NsPoints+BonusNsPoints);
        GameRoot.Game.AddMoney(Money+BonusMoney);
        GameRoot.Game.AddTime(TimeInMinutes);
    }

    public void AddRandomBonus(int nsPoints,int money=0)
    {
        BonusNsPoints = nsPoints;
        BonusMoney = money;
    }

    private int GetNsPoints()
    {
        return GameRoot.Game.Ns.Lv switch
        {
            2 => nsPointsLv2,
            3 => nsPointsLv3,
            _ => nsPointsLv1,
        };
    }
}
