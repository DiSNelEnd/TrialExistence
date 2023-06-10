using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ServiceNsView : MonoBehaviour
{
    private Text nsPoints;
    public string serviceName;
    public const string LOCNS = "Act.NsPoints";
    private Service service;
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
        Localize();
    }

    private void Localize()
    {
        if (GameRoot.IsGameNotStart) return;
        service??= GameRoot.Game.GetService(serviceName);
        nsPoints.text = LocalizationManager.Localize(LOCNS, service.NsPoints);
    }
}
