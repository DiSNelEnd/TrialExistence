using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServiceCreator
{
    private const int NAMEINDEX = 0;
    private const int LV1 = 1;
    private const int LV2 = 2;
    private const int LV3 = 3;
    private const int MONEYINDEX = 4;
    private const int TIMEINDEX = 5;
    private readonly char splitChar = '!';
    public ServiceCreator(){}

    public Service Create(string line)
    {
        var serviceParams = line.Split(splitChar);
        var nsPointLv1 = int.TryParse(serviceParams[LV1], out var parceNsLv1) 
            ? parceNsLv1 : throw new Exception("NsLv1 not number");
        var nsPointLv2 = int.TryParse(serviceParams[LV2], out var parceNsLv2)
            ? parceNsLv2 : throw new Exception("NsLv2 not number");
        var nsPointLv3 = int.TryParse(serviceParams[LV3], out var parceNsLv3)
            ? parceNsLv3 : throw new Exception("NsLv3 not number");
        var money = int.TryParse(serviceParams[MONEYINDEX], out var parceMoney)
            ? parceMoney : throw new Exception("Money not number");
        var timeInMinute = double.TryParse(serviceParams[TIMEINDEX], out var parceTime)
            ? parceTime : throw new Exception("Time not number");
        return new Service(serviceParams[NAMEINDEX],nsPointLv1,nsPointLv2,nsPointLv3,money,timeInMinute);
    } 
}
