using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DayView : MonoBehaviour
{
    private string localozationDay = "Computer.Day";
    private Text day;

    private void Awake()
    {
        day = GetComponent<Text>();
    }

    private void Start()
    {
        LocalizationManager.LocalizationChanged += Localize;
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= Localize;
    }

    private void OnEnable()
    {
        if (GameRoot.IsGameNotStart) return;
        Localize();
        GameRoot.Game.Time.OnDayChanged += Localize;
    }

    private void OnDisable()
    {
        if (!GameRoot.IsGameNotStart) 
            GameRoot.Game.Time.OnDayChanged -= Localize;
    }

    private void Localize()
    {
        if (!GameRoot.IsGameNotStart)
            day.text = LocalizationManager.Localize(localozationDay, GameRoot.Game.Time.Day);
    }
}
