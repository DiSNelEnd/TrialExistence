using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkData
{
    private const int VACATIONDAYS = 30;
    private const int DAYSOFF = 5;
    private readonly int moneyNotWork = 150;
    private readonly string locKeyDayOff = "Noti.Work.Dayoff";
    private readonly string locKeyVacation = "Noti.Work.Vacation";
    private readonly RandomResultOfWork randomResult;
    private bool IsWorkToDay => GameRoot.Game.WorkControler.WorkInDayCount > 0;
    public int VacationDayCount { get; private set; }
    public int DayOffCount { get; private set; }
    public WorkStatus WorkStatus { get; private set; }
    public event Action OnVacationChenged;
    public event Action OnDayOffChanged;
    public event Action OnStatusChanged;
    public Tuple<string,int> ResultOfWork { get; private set; }
    public WorkData(int vacationDays=VACATIONDAYS,int daysOff = DAYSOFF,WorkStatus workStatus=0)
    {
        VacationDayCount = vacationDays;
        DayOffCount = daysOff;
        WorkStatus = workStatus;
        randomResult = new RandomResultOfWork();
    }

    public void RunRandomResult()
    {
        ResultOfWork = randomResult.GetResult();
    }

    public void ResetRandomResult()
    {
        ResultOfWork = null;
    }

    public void ChengeStatus(WorkStatus workStatus)
    {
        WorkStatus = workStatus;
        OnStatusChanged?.Invoke();
    }

    public void GetDayOff()
    {
        if (GameRoot.CallNotification(
            WorkStatus != WorkStatus.InWork 
            || DayOffCount == 0|| IsWorkToDay, locKeyDayOff, Notifications.ComputerShowMessage))
            return;
        DayOffCount--;
        OnDayOffChanged?.Invoke();
        ChengeStatus(WorkStatus.DayOff);
        GameRoot.Game.AddMoney(moneyNotWork);
    }

    public void GetVacation()
    {
        if (GameRoot.CallNotification(
            WorkStatus != WorkStatus.InWork 
            || VacationDayCount == 0 || IsWorkToDay, locKeyVacation,Notifications.ComputerShowMessage)) 
            return;
        ChengeStatus(WorkStatus.InVacation);
        GameRoot.Game.AddMoney(moneyNotWork);
    }

    public void CountInVacation()
    {
        if (VacationIsOver()) return;
        VacationDayCount--;
        OnVacationChenged?.Invoke();
        GameRoot.Game.AddMoney(moneyNotWork);
    }

    public void SubscribeChanged()
    {
        GameRoot.Game.Time.OnMonthChanged += ResetDayOff;
        GameRoot.Game.Time.OnYearChanged += ResetVacation;
    }

    private void ResetDayOff()
    {
        DayOffCount = DAYSOFF;
        OnDayOffChanged?.Invoke();
    }

    private void ResetVacation()
    {
        VacationDayCount = VACATIONDAYS;
        OnVacationChenged?.Invoke();
    }

    private bool VacationIsOver()
    {
        var provision = VacationDayCount == 0;
        if (provision)
            ChengeStatus(WorkStatus.InWork);
        return provision;
    }
}

public enum WorkStatus
{
    InWork,
    InVacation,
    DayOff,
    Dismissal
}
