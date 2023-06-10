using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusConditionView : MonoBehaviour, IConditionView
{
    private StatusCondition condition;
    [SerializeField] private Image icon;
    [SerializeField] private Text nameCon;
    [SerializeField] private Text descriptionCon;
    [SerializeField] private Text duration;
    [SerializeField] private Text nsPoints;
    [SerializeField] private Text tick;
    [SerializeField] private Text type;
    private const string STATESTATUSLOCKEY = "Con.Status.State";
    private const string STATUSDURATION = "ConStatus.Duration";
    public void SetCondition(Condition condition)
    {
        this.condition = (StatusCondition)condition;
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
        type.text = LocalizationManager.Localize(STATESTATUSLOCKEY);
        duration.text = LocalizationManager.Localize(STATUSDURATION);
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
