using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomResultOfWork
{
    private readonly string critOversightKey = "CritOversight";
    private readonly string oversightKey = "Oversight";
    private readonly string critBreakKey = "CritBreak";
    private readonly string breakKey = "Break";
    private readonly string typicalDayKey = "TypicalDay";
    private bool IsIntoxication => GameRoot.Game.Player.Contains("Intoxication");
    private Dictionary<string, int> numbers;
    public RandomResultOfWork()
    {
        InitialNumbers();
    }

    public Tuple<string, int> GetResult()
    {
        if (GameRoot.IsGameNotStart) throw new  Exception("Game not start");
        if (IsIntoxication) return GetData(critBreakKey);
        var number = Random.Range(0, 101);
        if (number <= 20)
            return GetData(critOversightKey);
        else if (number > 20 && number <= 40)
            return GetData(oversightKey);
        else if (number > 40 && number <= 60)
            return GetData(typicalDayKey);
        else if (number > 60 && number <= 80)
            return GetData(breakKey);
        else
            return GetData(critBreakKey);
    }

    private int GetNumber(string key)
    {
        return numbers.TryGetValue(key, out var number) 
            ? number : throw new KeyNotFoundException("Reuslt work not found");
    }

    private Tuple<string,int> GetData(string key)
    {
        var number = GetNumber(key);
        return new Tuple<string, int>(key, number);
    }

    private void InitialNumbers()
    {
        numbers = new Dictionary<string, int>
        {
            {critOversightKey,-20 },
            {oversightKey,-10 },
            {critBreakKey,20 },
            {breakKey,10 },
            {typicalDayKey,0 }
        };
    }
}
