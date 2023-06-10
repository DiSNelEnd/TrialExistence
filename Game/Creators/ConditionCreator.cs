using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionCreator 
{
    private const int NAMEINDEX = 0;
    private const int NSINDEX = 1;
    private const int TIMEDURINDEX = 2;
    private const int TIMETICKINDEX = 3;
    private const int TYPEINDEX = 4;
    private readonly char splitChar = '!';
    private Dictionary<string, Func<string, int, double, double,Condition>> creators;
    public ConditionCreator() 
    {
        InitialCreators();
    }

    public Condition Create(string line)
    {
        var conditionParams = line.Split(splitChar);
        var nsPoints = int.TryParse(conditionParams[NSINDEX], out var parceNsPoints)
            ? parceNsPoints : throw new Exception("Nspoints not number");
        var timeDurationInMinute = double.TryParse(conditionParams[TIMEDURINDEX], out var parceTimeDuration)
            ? parceTimeDuration : throw new Exception("Time duration not number");
        var timeTickInMinute = double.TryParse(conditionParams[TIMETICKINDEX], out var parceTimeTick)
            ? parceTimeTick : throw new Exception("Time tick not number");
        return creators.TryGetValue(conditionParams[TYPEINDEX], out var creator)
            ? creator(conditionParams[NAMEINDEX], nsPoints, timeDurationInMinute, timeTickInMinute)
            : throw new KeyNotFoundException($"{conditionParams[NAMEINDEX]} not type condition");
    }

    private void InitialCreators()
    {
        creators = new Dictionary<string, Func<string, int, double, double, Condition>>()
        {
            {"D",(name,ns,timeDur,timeTick)=> new DebaffCondition(name,ns,timeDur,timeTick) },
            {"I", (name,ns,timeDur,timeTick)=> new InfluenceCondition(name,ns,timeDur,timeTick)},
            {"H", (name,ns,timeDur,timeTick)=> new HeroicCondition(name,ns,timeDur,timeTick)},
            {"S", (name,ns,timeDur,timeTick)=> new StatusCondition(name,ns,timeDur,timeTick)},
            {"C", (name,ns,timeDur,timeTick)=> new CriticalCondition(name,ns,timeDur,timeTick)},
            {"B", (name,ns,timeDur,timeTick)=> new BaffCondition(name,ns,timeDur,timeTick)},
            {"DE", (name,ns,timeDur,timeTick)=> new DependenceCondition(name,ns,timeDur,timeTick)}
        };
    }
}
