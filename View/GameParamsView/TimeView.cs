using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeView : MonoBehaviour
{
    private Text time;

    private void Awake()
    {
        time = GetComponent<Text>();
    }

    private void OnEnable()
    {
        if (GameRoot.IsGameNotStart) return;
        Show();
        GameRoot.Game.Time.OnTimeChanged += Show;
    }

    private void OnDisable()
    {
        if (!GameRoot.IsGameNotStart)
            GameRoot.Game.Time.OnTimeChanged -= Show;
    }

    private void Show()
    {
        if (!GameRoot.IsGameNotStart)
            time.text = GameRoot.Game.Time.TimeText;
    }
}
