using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameDataSaver 
{
    private readonly string gameDataFile = "GameData.json";
    private readonly string foodDataFile = "FoodData.json";
    private readonly string alcoholDataFile = "AlcoholData.json";
    private readonly string amusementDataFile = "AmusementData.json";
    private readonly string hobbyDataFile = "HobbyData.json";
    private readonly string lettersDataFile = "LettersData.json";
    private readonly string pornDataFile = "PornData.json";
    private readonly string sleepDataFile = "SleepData.json";
    private readonly string socialDataFile = "SocialData.json";
    private readonly string statusDataFile = "StatusData.json";
    private readonly string trainingDataFile = "TrainingData.json";
    private readonly string workDataFile = "WorkData.json";
    private readonly string conditionDataName= "ConditionData";
    private readonly string letterDataName = "LetterData";
    private readonly string letterPieceDataName = "LetterPieceData";
    private Dictionary<Type, string> conditionTypes;
    public bool IsSaved => File.Exists(GetPath(gameDataFile));
    public static GameDataSaver Instance => lazy.Value;
    private static readonly Lazy<GameDataSaver> lazy =
        new Lazy<GameDataSaver>(() => new GameDataSaver());
    private GameDataSaver() 
    {

    }

    public void SaveGameData(int nsPointsScore,double minutes,double day,int money,int conditionCount,bool isFirstStart)
    {
        var gameSaveData = new GameSaveData()
        {
            nsPointsScore = nsPointsScore,
            minutes = minutes,
            day = day,
            money = money,
            conditionCount = conditionCount,
            isFirstStart = isFirstStart
        };
        JsonSaver.SaveFileJson(gameSaveData, GetPath(gameDataFile));
    }

    public GameSaveData LoadGameData()
    {
        return LoadData<GameSaveData>(gameDataFile);
    }

    public void SaveFoodData(int foodCount,int differentFoodCount,string lastEatFood)
    {
        var foodSaveData = new FoodSaveData()
        {
            foodCount = foodCount,
            differentFoodCount = differentFoodCount,
            lastEatFood = lastEatFood
        };
        JsonSaver.SaveFileJson(foodSaveData, GetPath(foodDataFile));
    }

    public FoodSaveData LoadFoodData()
    {
        return LoadData<FoodSaveData>(foodDataFile);
    }

    public void SaveAlcoholData(int drinkInDayCount,int drinkDayCount,int notDrinkDayCount)
    {
        var alcoholSaveData = new AlcoholSaveData()
        {
            drinkInDayCount = drinkInDayCount,
            drinkDayCount = drinkDayCount,
            notDrinkDayCount = notDrinkDayCount
        };
        JsonSaver.SaveFileJson(alcoholSaveData, GetPath(alcoholDataFile));
    }

    public AlcoholSaveData LoadAlcoholData()
    {
        return LoadData<AlcoholSaveData>(alcoholDataFile);
    }

    public void SaveStatusData(bool inWorldDreams,bool enlightenment,bool flint,bool schizo,bool asexual,bool boss,bool athlete)
    {
        var statusSaveData = new StatusSaveData()
        {
            inWorldDreams = inWorldDreams,
            enlightenment = enlightenment,
            flint = flint,
            schizo = schizo,
            asexual = asexual,
            boss = boss,
            athlete = athlete
        };
        JsonSaver.SaveFileJson(statusSaveData, GetPath(statusDataFile));
    }

    public StatusSaveData LoadStatusData()
    {
        return LoadData<StatusSaveData>(statusDataFile);
    }

    public void SaveAmusementData(double timeFunInDay,int notFunDayCount,int differentPlayCount,int differentSeeCount)
    {
        var amusementSaveData = new AmusementSaveData()
        {
            timeFunInDay = timeFunInDay,
            notFunDayCount = notFunDayCount,
            differentPlayCount = differentPlayCount,
            differentSeeCount = differentSeeCount
        };
        JsonSaver.SaveFileJson(amusementSaveData, GetPath(amusementDataFile));
    }

    public AmusementSaveData LoadAmusementData()
    {
        return LoadData<AmusementSaveData>(amusementDataFile);
    }

    public void SaveHobbyData(int hobbyInDayCount,int hobbyDayCount)
    {
        var hobbySaveData = new HobbySaveData()
        {
            hobbyInDayCount = hobbyInDayCount,
            hobbyDayCount = hobbyDayCount
        };
        JsonSaver.SaveFileJson(hobbySaveData, GetPath(hobbyDataFile));
    }

    public HobbySaveData LoadHobbyData()
    {
        return LoadData<HobbySaveData>(hobbyDataFile);
    }

    public void SaveLettersData(
        int letterNumberNow,int unreadLettersCount,
        int unreadDayCount,int billCount,
        int lettersCount,int lastDayBill)
    {
        var lettersSaveData = new LettersSaveData()
        {
            letterNumberNow = letterNumberNow,
            unreadLettersCount = unreadLettersCount,
            unreadDayCount = unreadDayCount,
            billCount = billCount,
            lettersCount = lettersCount,
            unreadEmailsCount = Mail.UnreadEmailsCount,
            lastDayBill = lastDayBill
        };
        JsonSaver.SaveFileJson(lettersSaveData, GetPath(lettersDataFile));
    }

    public LettersSaveData LoadLettersData()
    {
        return LoadData<LettersSaveData>(lettersDataFile);
    }

    public void SavePornData(int lookPornInDayCount,int notLookPornDayCount)
    {
        var pornSaveData = new PornSaveData()
        {
            lookPornInDayCount = lookPornInDayCount,
            notLookPornDayCount = notLookPornDayCount
        };
        JsonSaver.SaveFileJson(pornSaveData, GetPath(pornDataFile));
    }

    public PornSaveData LoadPornData()
    {
        return LoadData<PornSaveData>(pornDataFile);
    }

    public void SaveSleepData(double sleepInDayCount,int notSleepDayCount,double remainingSleep)
    {
        var sleepSaveData = new SleepSaveData()
        {
            sleepInDayCount = sleepInDayCount,
            notSleepDayCount = notSleepDayCount,
            remainingSleep = remainingSleep
        };
        JsonSaver.SaveFileJson(sleepSaveData, GetPath(sleepDataFile));
    }

    public SleepSaveData LoadSleepData()
    {
        return LoadData<SleepSaveData>(sleepDataFile);
    }

    public void SaveSocialData(double talkTimeInDay,int notTalkDayCount)
    {
        var socialSaveData = new SocialSaveData()
        {
            talkTimeInDay = talkTimeInDay,
            notTalkDayCount = notTalkDayCount
        };
        JsonSaver.SaveFileJson(socialSaveData, GetPath(socialDataFile));
    }

    public SocialSaveData LoadSocialData()
    {
        return LoadData<SocialSaveData>(socialDataFile);
    }

    public void SaveTrainingData(
        int easyWorkoutCount,int hardWorkoutCount,
        int stretchingCount,int trainInDayCount,
        int notTrainDayCount,int notFrailDayCount,string lastTrainingName)
    {
        var trainingSaveData = new TrainingSaveData()
        {
            easyWorkoutCount = easyWorkoutCount,
            hardWorkoutCount = hardWorkoutCount,
            stretchingCount = stretchingCount,
            trainInDayCount = trainInDayCount,
            notTrainDayCount = notTrainDayCount,
            notFrailDayCount = notFrailDayCount,
            lastTrainingName = lastTrainingName
        };
        JsonSaver.SaveFileJson(trainingSaveData, GetPath(trainingDataFile));
    }

    public TrainingSaveData LoadTrainingData()
    {
        return LoadData<TrainingSaveData>(trainingDataFile);
    }

    public void SaveWorkData(int vacationDays,int daysOff,int workStatus,int workInDayCount,int notWorkDayCount,int inVacationDayCount)
    {
        var workSaveData = new WorkSaveData()
        {
            vacationDays = vacationDays,
            daysOff = daysOff,
            workStatus = workStatus,
            workInDayCount = workInDayCount,
            notWorkDayCount = notWorkDayCount,
            inVacationDayCount = inVacationDayCount
        };
        JsonSaver.SaveFileJson(workSaveData, GetPath(workDataFile));
    }

    public WorkSaveData LoadWorkData()
    {
        return LoadData<WorkSaveData>(workDataFile);
    }

    public void SaveConditionDatas(Queue<Condition> conditions)
    {
        var num = 0;
        foreach(var con in conditions)
        {
            var conditionSaveData = new ConditionSaveData
            {
                name = con.Name,
                nsPoints = con.NSPoints,
                timeDurationInMinutes = con.TimeDuration,
                timeTickInMinutes = con.TimeTick,
                heroicDayCount = con.HeroicDayCount
            };
            var dataFile = GetDataFile(num,conditionDataName);
            JsonSaver.SaveFileJson(conditionSaveData, GetPath(dataFile));
            num++;
        }
    }

    public List<ConditionSaveData> LoadCoditionDatas(int conditionCount)
    {
        var conditionDataFiles=GetDataFiles(conditionCount,conditionDataName);
        return conditionDataFiles.Select(f => LoadData<ConditionSaveData>(f)).ToList();
    }

    public void SaveLetterRegistrationDatas(Dictionary<int,Letter> letters)
    {
        var num = 0;
        foreach (var regLetter in letters)
        {
            if (regLetter.Value is StoryLetter)
                SaveStoryLetterData(regLetter.Key, regLetter.Value as StoryLetter,num);
            else
                SaveBillData(regLetter.Key, regLetter.Value as Bill,num);
            num++;
        }
    }

    public List<StoryLetterSaveData> LoadLetterDatas(int lettersCount)
    {
        var letterDataFiles = GetDataFiles(lettersCount, letterDataName);
        return letterDataFiles.Select(f => LoadData<StoryLetterSaveData>(f)).ToList();
    }

    public List<LetterPieceSaveData> LoadLetterPieceDatas(int idLetter,int letterPiecesCount)
    {
        var letterPieceFiles = GetDataFiles(letterPiecesCount, letterPieceDataName + idLetter.ToString());
        return letterPieceFiles.Select(f => LoadData<LetterPieceSaveData>(f)).ToList();
    }


    public void DeleteAllSaves()
    {
        if (!IsSaved) return;
        var gameData = LoadData<GameSaveData>(GetPath(gameDataFile));
        var conditionDataFiles = GetDataFiles(gameData.conditionCount,conditionDataName);
        foreach (var dataFile in conditionDataFiles)
            File.Delete(GetPath(dataFile));
        DeleteLetterDatas();
        File.Delete(GetPath(gameDataFile));
        File.Delete(GetPath(foodDataFile));
        File.Delete(GetPath(alcoholDataFile));
        File.Delete(GetPath(amusementDataFile));
        File.Delete(GetPath(hobbyDataFile));
        File.Delete(GetPath(lettersDataFile));
        File.Delete(GetPath(pornDataFile));
        File.Delete(GetPath(sleepDataFile));
        File.Delete(GetPath(socialDataFile));
        File.Delete(GetPath(statusDataFile));
        File.Delete(GetPath(trainingDataFile));
        File.Delete(GetPath(workDataFile));
    }

    private void DeleteLetterDatas()
    {
        var lettersData = LoadData<LettersSaveData>(GetPath(lettersDataFile));
        var letterDataFiles = GetDataFiles(lettersData.lettersCount, letterDataName);
        foreach(var letterDataFile in letterDataFiles)
        {
            var letterData = LoadData<StoryLetterSaveData>(GetPath(letterDataFile));
            if (letterData.storyLetterPiecesCount == 0)
                File.Delete(GetPath(letterDataFile));
            else
            {
                var letterPieceDataFiles = GetDataFiles(letterData.storyLetterPiecesCount, letterPieceDataName + letterData.id.ToString());
                foreach (var pieceDataFile in letterPieceDataFiles)
                    File.Delete(GetPath(pieceDataFile));
                File.Delete(GetPath(letterDataFile));
            }
        }
    }

    private void SaveStoryLetterData(int id,StoryLetter storyLetter,int num)
    {
        var storyLetterPieces = storyLetter.GetLettersPieces();
        var storyLetterData = new StoryLetterSaveData()
        {
            id = id,
            senderName=storyLetter.SenderName,
            status = (int)storyLetter.Status,
            day = storyLetter.Day,
            time = storyLetter.Time,
            letterNumber = storyLetter.LetterNumber,
            storyLetterPiecesCount=storyLetterPieces.Count
        };
        var dataFile = GetDataFile(num, letterDataName);
        JsonSaver.SaveFileJson(storyLetterData, GetPath(dataFile));
        SaveStoryLetterPieces(id, storyLetterPieces);
    }

    private void SaveBillData(int id,Bill bill,int num)
    {
        var storyLetterData = new StoryLetterSaveData()
        {
            id = id,
            status = (int)bill.Status,
            day = bill.Day,
            time = bill.Time,
        };
        var dataFile = GetDataFile(num, letterDataName);
        JsonSaver.SaveFileJson(storyLetterData, GetPath(dataFile));
    }

    private void SaveStoryLetterPieces(int idLetter,List<StoryLetterPiece> pieces)
    {
        var num = 0;
        foreach(var piece in pieces)
        {
            var storyLetterPieceData = new LetterPieceSaveData()
            {
                idLetter=idLetter,
                subLocKey=piece.SubLocKey,
                pieceNumber=piece.PieceNumber,
                nsPoint=piece.NsPoint,
                money=piece.Money,
                buttonNextFlag=piece.ButtonNextFlag,
                buttonReadFlag=piece.ButtonReadFlag             
            };
            var pieceDataName= letterPieceDataName+idLetter.ToString();
            var dataFile = GetDataFile(num,pieceDataName);
            JsonSaver.SaveFileJson(storyLetterPieceData, GetPath(dataFile));
            num++;
        }
    }

    private List<string> GetDataFiles(int count,string dataName)
    {
        var conditionDataFiles = new List<string>();
        for (var i=0; i < count; i++)
            conditionDataFiles.Add(GetDataFile(i,dataName));
        return conditionDataFiles;
    }

    private T LoadData<T>(string dataFile)
    {
        return JsonSaver.LoadingJson<T>(GetPath(dataFile));
    }

    private string GetDataFile(int num, string name)
    {
        return name + num.ToString() + ".json";
    }

    private string GetPath(string dataFile)
    {
        return Path.Combine(Application.persistentDataPath, dataFile);
    }
}
