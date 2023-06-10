using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TEGame
{
    private readonly ServicesRepository servicesRepository;
    private readonly ConditionsRepository conditionsRepository;
    private readonly LettersRepository lettersRepository;
    private readonly FoodControl foodControler;
    private readonly AlcoholControl alcoholControler;
    private readonly AmusementControl funControler;
    private readonly SocialControl talkControler;
    private readonly PornControl goPornControler;
    private readonly HobbyControl hobbyControler;
    private readonly DiseaseControl sickControler;
    private readonly TrainingControl trainControler;
    private readonly string notiKeyNotMoney = "Noti.NotMoney";
    private readonly string deathHangingCutSceenName = "DeathHanging";
    private readonly string winGameCutSceenName = "WinGame";
    public StoryLettersControl LettersControler { get; }
    public BillControl BillControler { get; }
    public SleepControl SleepControler { get; }
    public WorkControl WorkControler { get; }
    public ResultData ResultData { get; }
    public StatusControl StateControl { get; }
    public Player Player { get; }
    public NSPoints Ns { get; private set; }
    public GameTime Time { get; private set; }
    public GameMoney Money { get; private set; }
    public TEGame(
        NSPoints nSPoints,
        GameTime gameTime,
        GameMoney money,
        Player player,
        FoodControl foodControl,
        AlcoholControl alcoholControl,
        StatusControl statusControl,
        AmusementControl amusementControl,
        SocialControl socialControl,
        PornControl pornControl,
        HobbyControl hobbyControl,
        WorkControl workControl,
        SleepControl sleepControl,
        TrainingControl trainingControl,
        BillControl billControl,
        StoryLettersControl storyLettersControl)
    {
        Ns = nSPoints;
        Time = gameTime;
        Money = money;
        servicesRepository = new ServicesRepository();
        conditionsRepository = new ConditionsRepository();
        lettersRepository = new LettersRepository();
        ResultData = new ResultData();
        Player = player;
        foodControler = foodControl;
        alcoholControler = alcoholControl;
        StateControl = statusControl;
        funControler = amusementControl;
        talkControler = socialControl;
        goPornControler = pornControl;
        hobbyControler = hobbyControl;
        WorkControler = workControl;
        SleepControler = sleepControl;
        sickControler = new DiseaseControl();
        trainControler = trainingControl;
        BillControler = billControl;
        LettersControler = storyLettersControl;
    }

    public void SubscribeAllControler()
    {
        foodControler.SubscribeOnDayChanged();
        alcoholControler.SubscribeOnDayChanged();
        funControler.SubscribeOnDayChanged();
        talkControler.SubscribeOnDayChanged();
        goPornControler.SubscribeOnDayChanged();
        hobbyControler.SubscribeOnDayChanged();
        WorkControler.SubscribeOnDayChanged();
        SleepControler.SubscribeOnDayChanged();
        sickControler.SubscribeOnDayChanged();
        trainControler.SubscribeOnDayChanged();
        BillControler.SubscribeOnMounthChanged();
        LettersControler.SubscribeOnDayChanged();
    }

    public void AddTime(double timeInMinutes)
    {
        Time.AddTime(timeInMinutes);
        Player.ReduceTimeConditions(timeInMinutes);
    }

    public void AddNsPoints(int nsPoints)
    {
        Ns.Add(nsPoints);
        ResultData.AddNsPoints(nsPoints);
    }

    public void AddMoney(int money)
    {
        if (StateControl.Boss) return;
        Money.Add(money);
        ResultData.AddMoney(money);
    }

    public void ImposeCondition(string name)
    {
        ResultData.AddImposeCondition(name);
        var condition = conditionsRepository.Get(name);
        condition.Impose();
    }

    #region Letter
    public void SendLetter(Letter letter)
    {
        var id = Mail.CreateRegister(letter);
        SaveLetter(letter, id);
    }

    private void SaveLetter(Letter letter, int id)
    {
        lettersRepository.AddLetter(id, letter);
    }

    public Bill GetBill(int id)
    {
        return lettersRepository.GetBill(id);
    }

    public StoryLetter GetStoryLetter(int id)
    {
        return lettersRepository.GetStoryLetter(id);
    }
    #endregion

    #region GameAction

    public Service GetService(string name)
    {
        var service = servicesRepository.Get(name);
        return service ?? throw new NullReferenceException($"{name} not found");
    }

    public bool Drink(string serviceName)
    {
        var service = GetService(serviceName);
        var acsses = CheckingFunds(service.Money);
        if (acsses) return acsses;
        alcoholControler.Drink();
        ApplyService(service);
        return false;
    }

    public bool Eat(string serviceName)
    {
        var service = GetService(serviceName);
        var acsses = CheckingFunds(service.Money);
        if (acsses) return acsses;
        foodControler.Eat(serviceName);
        ApplyService(service);
        return false;
    }

    public bool Fun(string serviceName)
    {
        var service = GetService(serviceName);
        var acsses = CheckingFunds(service.Money);
        if (acsses) return acsses;
        funControler.Fun(service);
        ApplyService(service);
        return false;
    }

    public void Talk(string serviceName)
    {
        var service = GetService(serviceName);
        talkControler.Talk(service);
        ApplyService(service);
    }

    public bool LookPorn(string serviceName)
    {
        var service = GetService(serviceName);
        var acsses = CheckingFunds(service.Money);
        if (acsses) return acsses;
        goPornControler.GoPorn();
        ApplyService(service);
        return false;
    }

    public bool TookeUpHobby(string serviceName)
    {
        var service = GetService(serviceName);
        var acsses = CheckingFunds(service.Money);
        if (acsses) return acsses;
        hobbyControler.TookUpHobby();
        ApplyService(service);
        return false;
    }

    public void GetToWork(string serviceName)
    {
        var service = GetService(serviceName);
        WorkControler.GetToWork(service);
        if (WorkControler.WorkData.WorkStatus == WorkStatus.InWork)
            ApplyService(service);
    }

    public void GoToBed(string serviceName)
    {
        var service = GetService(serviceName);
        SleepControler.Sleep(service);
        ApplyService(service);
    }

    public void Train(string serviceName)
    {
        var service = GetService(serviceName);
        trainControler.Train(service);
        ApplyService(service);
    }

    public void ScipCutSceen()
    {
        ResultData.SetScipCutSceen();
        AddNsPoints(GameRoot.ScipCutSceenNS);
    }

    private void ApplyService(Service service)
    {
        ResultData.SetService(service);
        service.Apply();
    }
    #endregion

    #region Check

    public bool CheckingFunds(int money)
    {
        var balance = Money.Money + money;
        return GameRoot.CallNotification(balance < 0, notiKeyNotMoney, Notifications.ComputerShowMessage);
    }

    public bool CheckDeath()
    {
        if (Ns.Score <= 0)
            Turntable.PlayCutSceen(deathHangingCutSceenName);
        return Ns.Score <= 0;
    }

    public bool CheckWin()
    {
        if (StateControl.IsWin && Ns.IsWin)
            Turntable.PlayCutSceen(winGameCutSceenName);
        return StateControl.IsWin && Ns.IsWin;
    }
    #endregion

    #region Load

    public void LoadConditions(List<ConditionSaveData> conditionSaveDatas)
    {
        foreach (var conData in conditionSaveDatas)
            Player.AddNew(conditionsRepository.Create(conData));
    }

    public void LoadLetters(List<StoryLetterSaveData> letterSaveDatas,int unreadEmailsCount)
    {
        foreach(var data in letterSaveDatas.OrderBy(x=>x.id))
        {
            if (data.storyLetterPiecesCount > 0)
                LoadStoryLetter(data);
            else
                SendLetter(new Bill(data.day,data.time,(LetterStatus)data.status));
        }
        Mail.SetUnreadEmailsCount(unreadEmailsCount);
    }

    private void LoadStoryLetter(StoryLetterSaveData data)
    {
        var storyLetterPieces = GameDataSaver.Instance
            .LoadLetterPieceDatas(data.id, data.storyLetterPiecesCount)
            .Select(d => new StoryLetterPiece(d.subLocKey,d.pieceNumber,d.nsPoint,d.money,d.buttonNextFlag,d.buttonReadFlag))
            .ToList();
        SendLetter(new StoryLetter
            (data.senderName, data.day, data.time, data.letterNumber, storyLetterPieces, (LetterStatus)data.status));
    }
    #endregion

    public void SaveGame()
    {
        GameDataSaver.Instance.SaveGameData
            (Ns.Score, Time.TimeInMinutes, Time.DayInTime, 
            Money.Money,Player.Conditions.Count,Room.Instance.IsFirstComputerStart);
        GameDataSaver.Instance.SaveLettersData
           (LettersControler.LetterNumberNow, LettersControler.UnreadLettersCount,
           LettersControler.UnreadDayCount, BillControler.BillCount,
           lettersRepository.Letters.Count, BillControler.LastDayBill);
        GameDataSaver.Instance.SaveConditionDatas
            (Player.Conditions);
        GameDataSaver.Instance.SaveLetterRegistrationDatas(lettersRepository.Letters);
        foodControler.SaveData();
        alcoholControler.SaveData();
        funControler.SaveData();
        hobbyControler.SaveData();
        goPornControler.SaveData();
        SleepControler.SaveData();
        talkControler.SaveData();
        StateControl.SaveData();
        trainControler.SaveData();
        WorkControler.SaveData();
    }
}
