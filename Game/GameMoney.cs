using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMoney
{
    private int money;
    private readonly int bossMoney = 10000;
    public string MoneyText => money.ToString();
    public int Money => money;
    public event Action OnMoneyChanged;
    public GameMoney(int money)
    {
        this.money = money;
    }

    public void Add(int money)
    {
        this.money += money;
        OnMoneyChanged?.Invoke();
        CheckBossStatus();
    }

    private void CheckBossStatus()
    {
        if (GameRoot.IsGameNotStart) return;
        if(money>=bossMoney && !GameRoot.Game.StateControl.Boss && GameRoot.Game.BillControler.BillCount<=0)
            GameRoot.Game.StateControl.GetBoss();
    }
}
