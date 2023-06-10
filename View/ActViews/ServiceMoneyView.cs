using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ServiceMoneyView : MonoBehaviour
{
    public string serviceName;
    private Text money;
    public const string  LOCMONEY = "Act.Money";
    private Service service;
    private void Awake()
    {
        money = GetComponent<Text>();
    }

    private void OnEnable()
    {
        Localize();
    }

    private void Start()
    {
        LocalizationManager.LocalizationChanged += Localize;
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= Localize;
    }

    private void Localize()
    {
        if (GameRoot.IsGameNotStart) return;
        service ??= GameRoot.Game.GetService(serviceName);
        money.text = LocalizationManager.Localize(LOCMONEY,service.Money); 
    }
}
