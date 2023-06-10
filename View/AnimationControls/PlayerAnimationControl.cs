using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl 
{
    private readonly string animatorName = "PersoneTest";
    private Dictionary<string, string> boolNames;
    public static PlayerAnimationControl Instance => lazy.Value;
    private static readonly Lazy<PlayerAnimationControl> lazy =
        new Lazy<PlayerAnimationControl>(() => new PlayerAnimationControl());
    private string lastBoolName="";
    private PlayerAnimationControl()
    {
        InitialBoolNames();
    }

    public void PlayPlayerAnimation(string cutSceenName)
    {
        var boolName=GetBoolName(cutSceenName);
        lastBoolName=boolName;
        if (boolName == "") return;
        SetBoolAnim(boolName,true);
    }

    public void StopLastAnim()
    {
        if(lastBoolName=="") return;
        SetBoolAnim(lastBoolName,false);
        lastBoolName = "";
    }

    private void InitialBoolNames()
    {
        boolNames = new Dictionary<string, string>()
        {
            {"CreateFastFood","EatFood" },
            {"CreateStandardKitchen","EatFood" },
            {"CreateProperNutrition","EatFood" },
            {"CreateDessert","EatDesert" },
            {"CreateAlcohol", "DrinkAlcohol" },
            {"FunPlayGame", "PlayGame"},
            {"FunSeeSomething", "SeeSomething" },
            {"GetToWork","GetToWork" },
            {"PropheticDream", "GoToSleep" }
        };
    }

    private void SetBoolAnim(string boolName,bool flag)
    {
        Turntable.SetBoolAnimator(animatorName, boolName, flag);
    }

    private string GetBoolName(string cutSceenName)
    {
        return boolNames.TryGetValue(cutSceenName, out var result)
            ? result : "";
    }
}
