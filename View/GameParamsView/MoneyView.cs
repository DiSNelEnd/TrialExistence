using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyView : MonoBehaviour
{
    private const string LOCMONEY = "Computer.Money";
    private Text money;

    private void Awake()
    {
        money = GetComponent<Text>();
    }

    private void Start()
    {
        LocalizationManager.LocalizationChanged += Localize;
    }

    private void OnEnable()
    {
        if (GameRoot.IsGameNotStart) return;
        Localize();
        GameRoot.Game.Money.OnMoneyChanged += Localize;
    }

    private void OnDisable()
    {
        if (!GameRoot.IsGameNotStart)
            GameRoot.Game.Money.OnMoneyChanged -= Localize;
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= Localize;
    }

    private void Localize()
    {
        if(!GameRoot.IsGameNotStart)
            money.text = LocalizationManager.Localize(LOCMONEY, GameRoot.Game.Money.MoneyText);
    }
}
