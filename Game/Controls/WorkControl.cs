using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkControl
{
    private int workInDayCount;
    private int notWorkDayCount;
    private int inVacationDayCount;
    private readonly int maxWorkInDay = 1;
    private readonly int maxNotWorkDay = 3;
    private readonly int maxInVacationDay = 15;
    private readonly int motivationIncrease = 10;
    private readonly int specialistIncrease = 100;
    private readonly string fatigueName = "Fatigue";
    private readonly string idlerName = "Idler";
    private readonly string dismissalName = "Dismissal";
    private readonly string dealerName = "Dealer";
    private readonly string locKeyGetToWork = "Noti.Work.Proceed";
    private bool IsDismissal => GameRoot.Game.Player.Contains(dismissalName);
    private bool IsMotivation => GameRoot.Game.Player.Contains("Motivation");
    private bool IsSpecialist => GameRoot.Game.Player.Contains("Specialist");
    private bool IsDealer => GameRoot.Game.Player.Contains(dealerName);
    public int WorkInDayCount => workInDayCount;
    public WorkData WorkData { get; }
    public event Action OnResultChanged;
    public WorkControl(
        WorkData workData,
        int workInDayCount,
        int notWorkDayCount,
        int inVacationDayCount)
    {
        this.workInDayCount = workInDayCount;
        this.notWorkDayCount = notWorkDayCount;
        this.inVacationDayCount = inVacationDayCount;
        WorkData = workData;
    }

    public void GetToWork(Service service)
    {
        if (!IsDismissal && WorkData.WorkStatus == WorkStatus.Dismissal)
            WorkData.ChengeStatus(WorkStatus.InWork);
        if (GameRoot.CallNotification(
            WorkData.WorkStatus != WorkStatus.InWork
            && inVacationDayCount < maxInVacationDay, locKeyGetToWork,
            Notifications.ComputerShowMessage))
            return;
        WorkData.ChengeStatus(WorkStatus.InWork);
        RunRandom(service);
        workInDayCount++;
        if (WorkInDayCount > maxWorkInDay)
            ImposeCondition(fatigueName);
        DeleteCondition(dealerName);
        inVacationDayCount = 0;
    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged += CheckWorkDay;
        WorkData.SubscribeChanged();
    }

    public void SaveData()
    {
        GameDataSaver.Instance.SaveWorkData
            (WorkData.VacationDayCount, WorkData.DayOffCount,
            (int)WorkData.WorkStatus,workInDayCount,notWorkDayCount,inVacationDayCount);
    }

    public void ResetNotWorkDayCount()
    {
        notWorkDayCount = 0;
    }

    private void RunRandom(Service service)
    {
        WorkData.RunRandomResult();
        var number =WorkData.ResultOfWork.Item2;
        var money = number;
        if (IsMotivation) money += motivationIncrease;
        if (IsSpecialist) money += specialistIncrease;
        service.AddRandomBonus(number, money);
        OnResultChanged?.Invoke();
    }

    private void CheckWorkDay()
    {
        if (GameRoot.Game.StateControl.Boss) return;
        CheckNotWork();
        workInDayCount = 0;
        CloseDayOff();
    }

    private void CheckNotWork()
    {
        if (workInDayCount == 0)
            CountNotWork();
        else
            ResetNotWorkDayCount();
        if (notWorkDayCount > maxNotWorkDay)
            ImposeCondition(idlerName);
    }

    private void CloseDayOff()
    {
        if (WorkData.WorkStatus == WorkStatus.DayOff)
            WorkData.ChengeStatus(WorkStatus.InWork);
    }

    private void CountNotWork()
    {
        notWorkDayCount++;
        if (WorkData.WorkStatus == WorkStatus.InWork)
            ImposeDismissal();
        if (WorkData.WorkStatus == WorkStatus.InVacation)
            CountInVacation();
        if (inVacationDayCount > maxInVacationDay && !IsDealer)
            ImposeCondition(dealerName);
    }

    private void CountInVacation()
    {
        inVacationDayCount++;
        WorkData.CountInVacation();
    }

    private void ImposeDismissal()
    {
        ImposeCondition(dismissalName);
        WorkData.ChengeStatus(WorkStatus.Dismissal);
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