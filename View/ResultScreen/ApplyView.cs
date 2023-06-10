using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyView : MonoBehaviour
{
    private string conditionName;
    private int nsPoints;
    [SerializeField] Text conditionNameText;
    [SerializeField] Image conditionIcon;
    [SerializeField] Text nsPointsText;
    public void SetData(string name, int nsPoints)
    {
        conditionName = name;
        this.nsPoints = nsPoints;
        SetIcon();
        LocalizationName();
        ShowNsPoints();
    }

    private void Start()
    {
        LocalizationManager.LocalizationChanged += LocalizationName;
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= LocalizationName;
    }

    private void SetIcon()
    {
        conditionIcon.sprite = ImageRepository.GetConditionSprite(conditionName);
    }

    private void ShowNsPoints()
    {
        nsPointsText.text= nsPoints > 0 ? "+" + nsPoints.ToString() : nsPoints.ToString();
    }

    private void LocalizationName()
    {
        if (conditionName != null)
            conditionNameText.text = LocalizationManager.Localize(Condition.LOCNAME + conditionName);
    }
}
