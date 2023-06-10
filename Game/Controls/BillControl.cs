using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillControl
{
    private int billCount;
    private int lastDayBill;
    private readonly string conscienceName = "Conscience";
    public Action OnStatusDebtChanged;
    public string DebtStatus => billCount > 0? "InDebt" : "Conscience";
    public Bill Bill { get; private set; }
    public bool BillNotSet => Bill is null;
    public int BillCount => billCount;
    public int LastDayBill => lastDayBill;  
    public BillControl(int billCount, int lastDayBill=0)
    {
        this.billCount = billCount;
        this.lastDayBill = lastDayBill;
    }

    public void SubscribeOnMounthChanged()
    {
        GameRoot.Game.Time.OnMonthChanged += SendBill;
        GameRoot.Game.Time.OnDayChanged += CheckDebt;
    }
    public void SetBill(int id)
    {
        Bill = GameRoot.Game.GetBill(id);
        if (Bill.Status == LetterStatus.New)
            FirstOpenBill();     
    }

    public void PayBill()
    {
        var acsees= Bill.Pay();
        if (!acsees) return;
        Mail.TogglePaidUp(true);
        billCount--;
        if (billCount <= 0)
            DeleteCondition(conscienceName);
        OnStatusDebtChanged?.Invoke();
    }

    private void FirstOpenBill()
    {
        Bill.StatusNotPaid();
        Mail.OpenNewLetter();
    }

    private void SendBill()
    {
        if (lastDayBill == GameRoot.Game.Time.DayInTime) return;
        lastDayBill = GameRoot.Game.Time.DayInTime;
        var bill = new Bill(GameRoot.Game.Time.Day, GameRoot.Game.Time.TimeText);
        GameRoot.Game.SendLetter(bill);
        billCount++;
    }

    private void CheckDebt()
    {
        if (billCount > 0)
            ImposeCondition(conscienceName);
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
