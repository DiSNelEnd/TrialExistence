using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CriticalConditionView : MonoBehaviour,IConditionView
{
    private CriticalCondition condition;
    [SerializeField] private Image icon;
    [SerializeField] private Text nameCon;
    [SerializeField] private Text descriptionCon;
    [SerializeField] private Text duration;
    [SerializeField] private Text nsPoints;
    [SerializeField] private Text tick;
    [SerializeField] private Text type;
    [SerializeField] private Text critical;
    private const string CRITICALSTATUSLOCKEY = "ConStatus.Critical";
    private const string CRITLOCKEY = "ConCritical.";
    public void SetCondition(Condition condition)
    {
        this.condition = (CriticalCondition)condition;
        SetIcon();
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

    private void OnEnable()
    {
        if (condition is null) return;
        ShowTime(condition.TimeDuration, duration);
        ShowTime(condition.TimeTick, tick);
        ShowNsPoints();
    }

    private void SetIcon()
    {
        icon.sprite = ImageRepository.GetConditionSprite(condition.Name);
    }

    private void Localize()
    {
        if (condition is null) return;
        nameCon.text = LocalizationManager.Localize(Condition.LOCNAME + condition.Name);
        descriptionCon.text = LocalizationManager.Localize(Condition.LOCDESCRIPT + condition.Name);
        type.text = LocalizationManager.Localize(CRITICALSTATUSLOCKEY);
        critical.text = LocalizationManager.Localize(CRITLOCKEY + condition.Name);
    }

    private void ShowTime(double timeInminutes, Text text)
    {
        var hm = GameTime.ParseTime(timeInminutes);
        text.text = Condition.CorrectFormat(hm);
    }

    private void ShowNsPoints()
    {
        nsPoints.text = condition.NSPoints.ToString();
    }
}
