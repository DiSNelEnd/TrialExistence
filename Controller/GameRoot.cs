using Assets.SimpleLocalization;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRoot
{ 
    public static TEGame Game { get; private set; }
    public static bool IsGameNotStart => Game is null;
    public static readonly int ScipCutSceenNS = -30;
    public static void StartGame(int nsPointsScore, int money, double minutes)
    {
        Game = CreateGame(nsPointsScore, minutes, 0, money, new WorkData());
        Game.SubscribeAllControler();
        Game.GoToBed(SleepCommand.ServiceName);
        Information.DeleteAllRegister();
        Mail.DeleteAllRegister();
        Room.Instance.ToggleFirstStart(true);
        Game.SaveGame();
    }

    public static void LoadGame()
    {
        var gameData = GameDataSaver.Instance.LoadGameData();
        var foodData = GameDataSaver.Instance.LoadFoodData();
        var alcoholData = GameDataSaver.Instance.LoadAlcoholData();
        var statusData = GameDataSaver.Instance.LoadStatusData();
        var amusementData = GameDataSaver.Instance.LoadAmusementData();
        var socialData = GameDataSaver.Instance.LoadSocialData();
        var pornData = GameDataSaver.Instance.LoadPornData();
        var hobbyData = GameDataSaver.Instance.LoadHobbyData();
        var workData = GameDataSaver.Instance.LoadWorkData();
        var sleepData = GameDataSaver.Instance.LoadSleepData();
        var trainingData = GameDataSaver.Instance.LoadTrainingData();
        var lettersData = GameDataSaver.Instance.LoadLettersData();
        var conditionDatas = GameDataSaver.Instance.LoadCoditionDatas(gameData.conditionCount);
        var storyLetterDatas = GameDataSaver.Instance.LoadLetterDatas(lettersData.lettersCount);
        Game = CreateGame
            (gameData.nsPointsScore,
            gameData.minutes,
            gameData.day,
            gameData.money,
            new WorkData(workData.vacationDays,workData.daysOff,(WorkStatus)workData.workStatus),
            foodData.foodCount,
            foodData.differentFoodCount,
            foodData.lastEatFood,
            alcoholData.drinkInDayCount,
            alcoholData.drinkDayCount,
            alcoholData.notDrinkDayCount,
            statusData.inWorldDreams,
            statusData.enlightenment,
            statusData.flint,
            statusData.schizo,
            statusData.asexual,
            statusData.boss,
            statusData.athlete,
            amusementData.timeFunInDay,
            amusementData.notFunDayCount,
            amusementData.differentPlayCount,
            amusementData.differentSeeCount,
            socialData.talkTimeInDay,
            socialData.notTalkDayCount,
            pornData.lookPornInDayCount,
            pornData.notLookPornDayCount,
            hobbyData.hobbyInDayCount,
            hobbyData.hobbyDayCount,
            workData.workInDayCount,
            workData.notWorkDayCount,
            workData.inVacationDayCount,
            sleepData.sleepInDayCount,
            sleepData.notSleepDayCount,
            sleepData.remainingSleep,
            trainingData.easyWorkoutCount,
            trainingData.hardWorkoutCount,
            trainingData.stretchingCount,
            trainingData.trainInDayCount,
            trainingData.notTrainDayCount,
            trainingData.notFrailDayCount,
            trainingData.lastTrainingName,
            lettersData.billCount,
            lettersData.letterNumberNow,
            lettersData.unreadLettersCount,
            lettersData.unreadDayCount,
            lettersData.lastDayBill);
        Game.LoadConditions(conditionDatas);
        Game.LoadLetters(storyLetterDatas,lettersData.unreadEmailsCount);
        Game.SubscribeAllControler();
        Computer.Instance.ToggleMainPage(true);
        Room.Instance.ToggleFirstStart(gameData.isFirstStart);
    }

    public static void GameOver()
    {
        Game = null;
    }

    public static bool CallNotification(bool provision, string locKey, Action<string> showMessage)
    {
        if (provision)
            showMessage(locKey);
        return provision;
    }

    private static TEGame CreateGame
        (int nsPointsScore,
        double minutes,
        double day,
        int money,
        WorkData workData,
        int foodCount = 0,
        int differentFoodCount = 0,
        string lastEatFood = null,
        int drinkCount = 0,
        int drinkDayCount = 0,
        int notDrinkDayCount = 0,
        bool inWorldDreams = false,
        bool enlightenment = false,
        bool flint = false,
        bool schizo = false,
        bool asexual = false,
        bool boss = false,
        bool athlete = false,
        double timeFunInDay = 0,
        int notFunDayCount = 0,
        int differentPlayCount = 0,
        int differentSeeCount = 0,
        double talkTimeInDay = 0,
        int notTalkDayCount = 0,
        int lookPornInDayCount = 0,
        int notLookPornDayCount = 0,
        int hobbyInDayCount = 0,
        int hobbyDayCount = 0,
        int workInDayCount = 0,
        int notWorkDayCount = 0,
        int inVacationDayCount = 0,
        double sleepInDayCount = 0, 
        int notSleepDayCount = 0, 
        double remainingSleep = 0,
        int easyWorkoutCount = 0,
        int hardWorkoutCount = 0,
        int stretchingCount = 0,
        int trainInDayCount = 0,
        int notTrainDayCount = 0,
        int notFrailDayCount = 0,
        string lastTrainingName = null,
        int billCount=0,
        int letterNumberNow = 1, 
        int unreadLettersCount = 0,
        int unreadDayCount = 0,
        int lastDayBill=0)
    {
        return new TEGame(
            new NSPoints(nsPointsScore),
            new GameTime(minutes, day),
            new GameMoney(money),
            new Player(),
            new FoodControl(foodCount,differentFoodCount,lastEatFood),
            new AlcoholControl(drinkCount,drinkDayCount,notDrinkDayCount),
            new StatusControl(inWorldDreams,enlightenment,flint,schizo,asexual,boss,athlete),
            new AmusementControl(timeFunInDay,notFunDayCount,differentPlayCount,differentSeeCount),
            new SocialControl(talkTimeInDay,notTalkDayCount),
            new PornControl(lookPornInDayCount,notLookPornDayCount),
            new HobbyControl(hobbyInDayCount,hobbyDayCount),
            new WorkControl(workData,workInDayCount,notWorkDayCount, inVacationDayCount),
            new SleepControl(sleepInDayCount,notSleepDayCount,remainingSleep),
            new TrainingControl
            (easyWorkoutCount,hardWorkoutCount,
            stretchingCount,trainInDayCount,
            notTrainDayCount,notFrailDayCount,lastTrainingName),
            new BillControl(billCount,lastDayBill),
            new StoryLettersControl(letterNumberNow,unreadLettersCount,unreadDayCount));
    }
}

