using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NsPointsView : MonoBehaviour
{
    private const string  LOCNS= "Computer.NSpoints";
    private Text nsPoints;
    private void Awake()
    {
        nsPoints = GetComponent<Text>();   
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
        GameRoot.Game.Ns.OnScoreChanged += Localize;
    }

    private void OnDisable()
    {
        if(!GameRoot.IsGameNotStart)
            GameRoot.Game.Ns.OnScoreChanged -= Localize;
    }

    private void Localize()
    {
        if (!GameRoot.IsGameNotStart)
            nsPoints.text = LocalizationManager.Localize(LOCNS, GameRoot.Game.Ns.ScoreText);
    }
}
