using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public interface ISwitcheble
{
    public string Name { get; }
    public void Toggle(bool flag);
}
public static class Switcher 
{
    public static void Toggle(string name, bool flag)
    {
        var switcheble = SwitcheblesRepository.GetSwitcheble(name);
        switcheble.Toggle(flag);
    }

    public static void ToggleAll(bool flag)
    {
        var switchebles = SwitcheblesRepository.GetAll();
        foreach (var s in switchebles)
            s.Toggle(flag);
    }
}
