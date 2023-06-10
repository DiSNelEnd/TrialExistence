using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomResultOfSleep 
{
    private readonly string nightmare = "Nightmare";
    private readonly string prophetic = "Prophetic";
    private readonly int nigtmareNsPoints = -20;
    private readonly int propheticNspoints = 10;
    public Tuple<string,int> GetSesult()
    {
        var number = Random.Range(0, 101);
        if (number >= 0 && number <= 50)
            return new Tuple<string,int>(prophetic,propheticNspoints);
        else return new Tuple<string, int>(nightmare, nigtmareNsPoints);
    }
}
