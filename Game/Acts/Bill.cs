using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bill : Letter
{
    private readonly int TotalMoney = -2700;
    public bool IsPaidUp => Status == LetterStatus.PaidUp;
    public Bill(string day, string time, LetterStatus status = LetterStatus.New) :base(day,time,status)
    {
    }

    public bool Pay()
    {
        if (IsPaidUp) return false;
        if (GameRoot.Game.CheckingFunds(TotalMoney)) return false;
        ChangeStatus(LetterStatus.PaidUp);
        GameRoot.Game.AddMoney(TotalMoney);
        return true;
    }

    public void StatusNotPaid()
    {
       ChangeStatus(LetterStatus.NotPaid);
    }
}
