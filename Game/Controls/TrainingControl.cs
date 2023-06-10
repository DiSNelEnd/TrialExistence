using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingControl
{
    private int easyWorkoutCount;
    private int hardWorkoutCount;
    private int stretchingCount;
    private int trainInDayCount;
    private int notTrainDayCount;
    private int notFrailDayCount;
    private string lastTrainingName;
    private readonly int maxWorkoutRowCount = 2;
    private readonly int maxStretchingRowCount = 3;
    private readonly int maxNotTrainDayCount = 3;
    private readonly int maxNotFrailDayCount = 30;
    private readonly int bonusEasyWorkout = 20;
    private readonly int bonusHardWorkout = 40;
    private readonly int bonusStreching = 15;
    private readonly string easyWorkoutName = "EasyWorkout";
    private readonly string hardWorkoutName = "HardWorkout";
    private readonly string stretchingName = "Stretching";
    private readonly string noProgressName = "NoProgress";
    private readonly string traumaName = "Trauma";
    private readonly string progressName = "Progress";
    private readonly string pulledName = "Pulled";
    private readonly string inGoodShapeName = "InGoodShape";
    private readonly string overtrainingName = "Overtraining";
    private readonly string frailName = "Frail";
    private bool IsTrauma => GameRoot.Game.Player.Contains(traumaName);
    private bool IsPulled => GameRoot.Game.Player.Contains(pulledName);
    private bool IsFrail=> GameRoot.Game.Player.Contains(frailName);
    private bool IsOvertraining => GameRoot.Game.Player.Contains(overtrainingName);
    public TrainingControl(
        int easyWorkoutCount,
        int hardWorkoutCount,
        int stretchingCount,
        int trainInDayCount,
        int notTrainDayCount,
        int notFrailDayCount,
        string lastTrainingName)
    {
        this.easyWorkoutCount = easyWorkoutCount;
        this.hardWorkoutCount = hardWorkoutCount;
        this.stretchingCount = stretchingCount;
        this.trainInDayCount = trainInDayCount;
        this.notTrainDayCount = notTrainDayCount;
        this.notFrailDayCount = notFrailDayCount;
        this.lastTrainingName = lastTrainingName;
    }

    public void Train(Service service)
    {
        if (service.Name == easyWorkoutName)
            EasyWorkout(service);
        else if (service.Name == hardWorkoutName)
            HardWorkout(service);
        else
            Stretching(service);
        trainInDayCount++;
        if (trainInDayCount > maxNotTrainDayCount || IsTrauma && IsPulled || IsOvertraining)
            ImposeCondition(overtrainingName);
        DeleteCondition(frailName);
        notTrainDayCount = 0;
    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged += CheckTrainCount;
    }

    public void SaveData()
    {
        GameDataSaver.Instance.SaveTrainingData
            (easyWorkoutCount, hardWorkoutCount, stretchingCount,
            trainInDayCount, notTrainDayCount, notFrailDayCount, lastTrainingName);
    }

    private void CheckTrainCount()
    {
        if (trainInDayCount == 0)
            notTrainDayCount++;
        if (notTrainDayCount > maxNotTrainDayCount)
            ImposeFrail();
        if (!IsFrail)
            notFrailDayCount++;
        if (notFrailDayCount > maxNotFrailDayCount && !GameRoot.Game.StateControl.Athlete)
            GameRoot.Game.StateControl.GetAthlete();
        trainInDayCount = 0;
    }

    private void EasyWorkout(Service service)
    {
        easyWorkoutCount++;
        lastTrainingName = easyWorkoutName;
        hardWorkoutCount = 0;
        stretchingCount = 0;
        if (easyWorkoutCount > maxWorkoutRowCount)
            ImposeCondition(noProgressName);
        if (GameRoot.Game.StateControl.Athlete)
            service.AddRandomBonus(bonusEasyWorkout);
    }

    private void HardWorkout(Service service)
    {
        hardWorkoutCount++;
        lastTrainingName = hardWorkoutName;
        easyWorkoutCount = 0;
        stretchingCount = 0;
        if (hardWorkoutCount > maxWorkoutRowCount)
            ImposeCondition(traumaName);
        else
            ImposeProgress();
        if(GameRoot.Game.StateControl.Athlete)
            service.AddRandomBonus(bonusHardWorkout);
    }

    private void Stretching(Service service)
    {
        stretchingCount++;
        easyWorkoutCount = 0;
        hardWorkoutCount = 0;
        if(lastTrainingName==null) lastTrainingName = stretchingName;
        if (lastTrainingName == hardWorkoutName || lastTrainingName == easyWorkoutName)
            ImposeCondition(inGoodShapeName);
        lastTrainingName = stretchingName;
        if (stretchingCount > maxStretchingRowCount)
            ImposeCondition(pulledName);
        if (GameRoot.Game.StateControl.Athlete)
            service.AddRandomBonus(bonusStreching);
    }

    private void ImposeFrail()
    {
        ImposeCondition(frailName);
        notFrailDayCount = 0;
    }

    private void ImposeProgress()
    {
        var number = Random.Range(0,11);
        if (number >= 0 && number < 3 || number > 8)
        {
            ImposeCondition(progressName);
            DeleteCondition(noProgressName);
        }
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
