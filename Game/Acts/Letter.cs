using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Letter
{
    public static Action OnStatusChanged;
    public string Day { get; }
    public string Time { get; }
    public LetterStatus Status { get; private set; }
    public Letter(string day,string time,LetterStatus status=LetterStatus.New)
    {
        Day = day;
        Time = time;
        Status = status;
    }

    internal void ChangeStatus(LetterStatus letterStatus)
    {
        Status = letterStatus;
        OnStatusChanged?.Invoke();
    }
}

public enum LetterStatus
{
    New,
    Read,
    Unread,
    PaidUp,
    NotPaid
}
