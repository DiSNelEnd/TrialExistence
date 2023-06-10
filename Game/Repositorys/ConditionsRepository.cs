using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionsRepository
{
    private readonly List<string> conditionsLine;
    private readonly ConditionCreator creator;
    public ConditionsRepository()
    {
        conditionsLine = ParamsReader.GetConditionsParams().ToList();
        creator = new ConditionCreator();
    }

    public Condition Get(string name)
    {
        var line = conditionsLine.FirstOrDefault(c =>c.StartsWith(name));
        return line != null ? creator.Create(line) : throw new NullReferenceException($"{name} not found");
    }

    public Condition Create(ConditionSaveData conditionSaveData)
    {
        var conNew = Get(conditionSaveData.name);
        conNew.SetParams(
            conditionSaveData.nsPoints, 
            conditionSaveData.timeDurationInMinutes, 
            conditionSaveData.timeTickInMinutes, 
            conditionSaveData.heroicDayCount);
        return conNew;
    }
}
