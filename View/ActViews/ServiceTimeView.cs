using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ServiceTimeView : MonoBehaviour
{
    private Text timeText;
    private Service service;
    public string serviceName;
    private void Awake()
    {
        timeText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        Show();
    }

    private string ParseTime(double timeInMinutes)
    {
        var hm = GameTime.ParseTime(timeInMinutes);
        return GameTime.CorrectFormat(hm.Item1)+":"+GameTime.CorrectFormat(hm.Item2);
    }

    private void Show()
    {
        if (GameRoot.IsGameNotStart) return;
        service??= GameRoot.Game.GetService(serviceName);
        timeText.text = ParseTime(service.TimeInMinutes);
    }
}
