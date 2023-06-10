using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholControl 
{
    private int drinkInDayCount;
    private int drinkDayCount;
    private int notDrinkDayCount;
    private readonly int maxDrinkDay = 2;
    private readonly int maxDrinkInDay = 1;
    private readonly int maxNotDrinkDay = 10;
    private readonly string withHangoverName = "WithHangover";
    private readonly string alcoholicName = "Alcoholic";
    private readonly string drunkennessName = "Drunkenness";
    private readonly string teetotalerName = "Teetotaler";
    private bool Alcoholic => GameRoot.Game.Player.Contains(alcoholicName);
    public AlcoholControl(int drinkInDayCount,int drinkDayCount, int notDrinkDayCount) 
    {
        this.drinkInDayCount = drinkInDayCount;
        this.drinkDayCount = drinkDayCount;
        this.notDrinkDayCount = notDrinkDayCount;
    }

    public void Drink()
    {
        drinkInDayCount++;
        DeleteCondition(teetotalerName);
        if (Alcoholic)
            ImposeCondition(alcoholicName);
        if (drinkInDayCount > maxDrinkInDay)
            ImposeCondition(drunkennessName);
    }

    public void SaveData()
    {
        GameDataSaver.Instance.SaveAlcoholData(drinkInDayCount, drinkDayCount, notDrinkDayCount);
    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged += CheckDrinkCount;
    }

    private void CheckDrinkCount()
    {
        if (drinkInDayCount > 0)
            Drank();
        else
            DidNotDrank();
        drinkInDayCount=0;
    }

    private void DidNotDrank()
    {
        drinkDayCount = 0;
        notDrinkDayCount++;
        if (notDrinkDayCount > maxNotDrinkDay)
            ImposeTeetotaler();
    }

    private void Drank()
    {
        notDrinkDayCount = 0;
        ImposeWithHangover();
        drinkDayCount++;
        if (drinkDayCount > maxDrinkDay && !Alcoholic)
            ImposeCondition(alcoholicName);
    }

    private void ImposeTeetotaler()
    {
        ImposeCondition(teetotalerName);
        DeleteCondition(alcoholicName);
    }

    private void ImposeWithHangover()
    {
        for (int i = 0; i < drinkInDayCount; i++)
            ImposeCondition(withHangoverName);
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
