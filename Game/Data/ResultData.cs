using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResultData 
{
    public int ID { get; private set; }
    public int Score { get; private set; }
    public int ResultScore { get; private set; }
    public int Money { get; private set; }
    public int ResultMoney { get; private set; }
    public bool IsScipCutSceen { get; private set; }
    public Service Service { get; private set; }
    public List<string> ImposeConditions { get; private set; }
    public Dictionary<string,int> ApplyConditions { get; private set; }
    public ResultData()
    {
        ImposeConditions = new List<string>();
        ApplyConditions = new Dictionary<string, int>();
    }

    public void SetService(Service service)
    {
        ResetData();
        Service = service;
        ID++;
        Score = GameRoot.Game.Ns.Score;
        Money = GameRoot.Game.Money.Money;
    }

    public void AddImposeCondition(string name)
    {
        ImposeConditions.Add(name);
    }

    public void AddNsPoints(int nsPoints)
    {
        ResultScore += nsPoints;
    }

    public void AddMoney(int money)
    {
        ResultMoney += money;
    }

    public void AddApplyCondition(string name,int nsPoints)
    {
        ApplyConditions.Add(name,nsPoints);
    }

    public void SetScipCutSceen()
    {
        IsScipCutSceen = true;
    }

    private void ResetData()
    {
        ResultScore = 0;
        ResultMoney = 0;
        IsScipCutSceen = false;
    }
    public void ClearImposeApply()
    {
        ImposeConditions.Clear();
        ApplyConditions.Clear();
    }
}
