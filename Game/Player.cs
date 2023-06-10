using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player
{
    private readonly Queue<Condition> conditions;
    public Queue<Condition> Conditions => conditions;
    public Player()
    {
        conditions = new Queue<Condition>();
    }

    public void AddCondition(Condition condition)
    {
        if (Contains(condition.Name))
            StrengthenCondition(condition);
        else
            AddNew(condition);
    }

    public void DeleteCondition(string name)
    {
        var con = conditions.FirstOrDefault(c => c.Name.Equals(name));
        con?.Delete();
    }

    public void ReduceTimeConditions(double timeInMinutes)
    {
        var count = conditions.Count;
        for(int i = 0; i < count; i++)
        {
            var con = conditions.Dequeue();
            con.ReduceTime(timeInMinutes);
            if (con.IsOver)
                DeleteRegister(con.Name);
            else
                conditions.Enqueue(con);
        }
    }

    private void DeleteRegister(string conName)
    {
        Information.DeleteRegister(conName);
    }

    public void AddNew(Condition condition)
    {
        conditions.Enqueue(condition);
        Information.CreateRegister(condition);
    }

    public bool Contains(string name)
    {
        return conditions.Any(c=>c.Name.Equals(name));
    }

    public Tuple<int,int> GetCriticalAndHeroicCounts()
    {
        var critCount = 0;
        var heroicCount = 0;
        foreach(var c in conditions)
        {
            if (c.GetType() == typeof(CriticalCondition))
                critCount++;
            if (c.GetType() == typeof(HeroicCondition))
                heroicCount++;
        }
        return new Tuple<int, int>(critCount, heroicCount);
    }

    private void StrengthenCondition(Condition condition)
    {
        var con = conditions.First(c => c.Name == condition.Name);
        con.Strengthen();
    }
}