using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SwitcheblesRepository
{
    private static List<ISwitcheble> switchebles=new List<ISwitcheble>();
    public static void AddSwitcheble(ISwitcheble switcheble)
    {
        switchebles.Add(switcheble);
    }

    public static ISwitcheble GetSwitcheble(string name)
    {
        var switcheble= switchebles.FirstOrDefault(s => s.Name.Equals(name));
        return switcheble is null ? throw new NullReferenceException($"{name} not found") : switcheble;
    }

    public static IEnumerable<ISwitcheble> GetAll()
    {
        return switchebles;
    }
}
