using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDataView : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Text resultNsPoints;
    [SerializeField] private Text serviceName;
    [SerializeField] private Text serviceNsPoints;
    [SerializeField] private Text money;
    [SerializeField] private Text resultMoney;
    [SerializeField] private Text signNspoints;
    [SerializeField] private Text signMoney;
    [SerializeField] private Text sumNsPoints;
    [SerializeField] private Text sumMoney;
    [SerializeField] private Text scipText;
    [SerializeField] private Text scipNS;
    [SerializeField] private ResultImposeView imposeView;
    [SerializeField] private ResultApplyView applyView;
    private readonly string locServiceName = "Service."; 

    private void OnEnable()
    {
        if (GameRoot.IsGameNotStart) return;
        ShowNsPoints();
        ShowMoney();
        ShowService();
        ShowScipNS();
        imposeView.Show();
        applyView.Show();
    }

    private void Start()
    {
        LocalizationManager.LocalizationChanged += LocalizeServiceName;
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= LocalizeServiceName;
    }

    private void ShowNsPoints()
    {
        score.text = GameRoot.Game.ResultData.Score.ToString();
        var result = GameRoot.Game.ResultData.ResultScore;
        signNspoints.text = result >= 0 ? "+" : "-";
        resultNsPoints.text = result > 0 ? result.ToString() : (result * -1).ToString();
        sumNsPoints.text = (GameRoot.Game.ResultData.Score + result).ToString();
    }

    private void ShowMoney()
    {
        money.text = GameRoot.Game.ResultData.Money.ToString();
        var result = GameRoot.Game.ResultData.ResultMoney;
        signMoney.text = result >= 0 ? "+" : "-";
        resultMoney.text = result > 0 ? result.ToString() : (result * -1).ToString();
        sumMoney.text = (GameRoot.Game.ResultData.Money + result).ToString();
    }

    private void ShowService()
    {
        var service = GameRoot.Game.ResultData.Service;
        if (service is null) return;
        var nsPoints = service.NsPoints+service.BonusNsPoints;
        serviceNsPoints.text = nsPoints > 0 ? "+" + nsPoints.ToString() : nsPoints.ToString();
        LocalizeServiceName();
    }

    private void ShowScipNS()
    {
        var ns = 0;
        if (GameRoot.Game.ResultData.IsScipCutSceen)
            ns = GameRoot.ScipCutSceenNS;
        scipNS.text = ns.ToString();
    }

    private void LocalizeServiceName()
    {
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.Game.ResultData.Service is null) return;
        serviceName.text = LocalizationManager.Localize(locServiceName+GameRoot.Game.ResultData.Service.Name);
    }
}
