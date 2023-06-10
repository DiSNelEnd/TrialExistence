using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroicConditionView : MonoBehaviour, IConditionView
{
    private HeroicCondition condition;
    [SerializeField] private Image icon;
    [SerializeField] private Text nameCon;
    [SerializeField] private Text descriptionCon;
    [SerializeField] private Text duration;
    [SerializeField] private Text nsPoints;
    [SerializeField] private Text tick;
    [SerializeField] private Text type;
    private const string HEROICSTATUSLOCKEY = "Con.Status.Heroic";
    private const string HEROICDURATION = "ConHeroic.Duration";
    public void SetCondition(Condition condition)
    {
        this.condition = (HeroicCondition)condition;
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
        type.text = LocalizationManager.Localize(HEROICSTATUSLOCKEY);
        duration.text = LocalizationManager.Localize(HEROICDURATION);
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
