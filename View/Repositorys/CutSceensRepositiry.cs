using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CutSceensRepositiry
{
    private static List<CutSceen> cutSceens=new List<CutSceen>();

    public static void AddCutSceen(CutSceen cutSceen)
    {
        cutSceens.Add(cutSceen);
    }

    public static List<CutSceen> GetAll()
    {
        return cutSceens;
    }

    public static CutSceen GetCutSceen(string name)
    {
        var cutsceen= cutSceens.FirstOrDefault(c => c.Name.Equals(name));
        return cutsceen is null? throw new NullReferenceException($"{name} not found") : cutsceen;
    }
}
