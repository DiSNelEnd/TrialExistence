using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusControl
{
    private static readonly string enlightenmentName = "Enlightenment";
    private static readonly string bossName = "Boss";
    private static readonly string schizoName = "Schizo";
    private static readonly string asexualName = "Asexual";
    private static readonly string athleteName = "Athlete";
    private static readonly string inWorldDreamsName = "InWorldDreams";
    private static readonly string flintName = "Flint";
    private readonly int countStatusForWin = 4;
    private Dictionary<string, Func<string>> statuses;
    public bool IsWin=>CheckStatuses();
    public bool Enlightenment { get; private set; }
    public bool Flint { get; private set; }
    public bool Schizo { get; private set; }
    public bool Asexual { get; private set; }
    public bool Boss { get; private set; }
    public bool InWorldDreams { get; private set; }
    public bool Athlete { get; private set; }
    public StatusControl(
        bool inWorldDreams,
        bool enlightenment, 
        bool flint, 
        bool schizo,
        bool asexual,
        bool boss,
        bool athlete)
    {
        InWorldDreams = inWorldDreams;
        Enlightenment = enlightenment;
        Flint = flint;
        Schizo = schizo;
        Asexual = asexual;
        Boss = boss;
        Athlete = athlete;
        InitialStatuses();
        InitialSprites();
    }

    public void EndedHeroic(string name)
    {
        if (GameRoot.IsGameNotStart) return;
        var statusName= statuses.TryGetValue(name,out var value)? value(): throw new KeyNotFoundException("status not found");
        GameRoot.Game.ImposeCondition(statusName);
    }
    private bool CheckStatuses()
    {
        var num = 0;
        if (Enlightenment == true)
            num++;
        if (Flint==true)
            num++;
        if (Schizo == true)
            num++;
        if (Asexual == true)
            num++;
        if (Boss == true)
            num++;
        if (InWorldDreams == true)
            num++;
        if (Athlete == true)
            num++;
        return num >= countStatusForWin;
    }

    private void InitialStatuses()
    {
        statuses = new Dictionary<string,Func<string>>()
        {
            {"Starvation",GetEnlightenment},
            {"Boredom", GetFlint},
            {"Loneliness", GetSchizo },
            {"Abstinence", GetAsexual },
            {"Sonya", GetInWorldDreams }
        };
    }

    public void GetAthlete()
    {
        Athlete = true;
        ToggleSprite(athleteName, Athlete);
        ImposeCondition(athleteName);
        DeleteCondition("NoProgress");
    }

    public void GetBoss()
    {
        Boss = true;
        GameRoot.Game.ImposeCondition(bossName);
        ToggleSprite(bossName, Boss);
    }

    public void SaveData()
    {
        GameDataSaver.Instance.SaveStatusData(InWorldDreams,Enlightenment,Flint,Schizo,Asexual,Boss,Athlete);
    }

    private string GetInWorldDreams()
    {
        InWorldDreams = true;
        ToggleSprite(inWorldDreamsName, InWorldDreams);
        return inWorldDreamsName;
    }

    private string GetFlint()
    {
        Flint = true;
        ToggleSprite(flintName, Flint);
        return flintName;
    }

    private string GetEnlightenment()
    {
        Enlightenment = true;
        DeleteCondition("Planted");
        ToggleSprite(enlightenmentName, Enlightenment);
        return enlightenmentName;
    }

    private string GetSchizo()
    {
        Schizo = true;
        ToggleSprite(schizoName, Schizo);
        return schizoName;
    }

    private string GetAsexual()
    {
        Asexual = true;
        DeleteCondition("Standing");
        ToggleSprite(asexualName, Asexual);
        return asexualName;
    }

    private void DeleteCondition(string name)
    {
        GameRoot.Game.Player.DeleteCondition(name);
    }

    private void ImposeCondition(string conName)
    {
        GameRoot.Game.ImposeCondition(conName);
    }

    private void ToggleSprite(string name, bool flag)
    {
        Switcher.Toggle(name, flag);
    }

    private void InitialSprites()
    {
        ToggleSprite(enlightenmentName, Enlightenment);
        ToggleSprite(bossName, Boss);
        ToggleSprite(schizoName, Schizo);
        ToggleSprite(asexualName, Asexual);
        ToggleSprite(flintName, Flint);
        ToggleSprite(inWorldDreamsName, InWorldDreams);
        ToggleSprite(athleteName, Athlete);
    }
}
