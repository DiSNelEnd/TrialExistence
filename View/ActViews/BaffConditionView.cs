using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaffConditionView : MonoBehaviour,IConditionView
{
    private BaffCondition condition;
    [SerializeField] private Image icon;
    [SerializeField] private Text nameCon;
    [SerializeField] private Text descriptionCon;
    [SerializeField] private Text duration;
    [SerializeField] private Text nsPoints;
    [SerializeField] private Text tick;
    [SerializeField] private Text type;
    private const string BAFFSTATUSLOCKEY = "Con.Status.Baff";
    public void SetCondition(Condition condition)
    {
        this.condition = (BaffCondition)condition;
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
        var image = Resources.Load<Sprite>($"Image/Conditions/{condition.Name}");
        icon.sprite = image;
    }

    private void Localize()
    {
        if (condition is null) return;
        nameCon.text = LocalizationManager.Localize(Condition.LOCNAME + condition.Name);
        descriptionCon.text = LocalizationManager.Localize(Condition.LOCDESCRIPT + condition.Name);
        type.text = LocalizationManager.Localize(BAFFSTATUSLOCKEY);
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

public interface IConditionView
{
    public void SetCondition(Condition condition);
}
