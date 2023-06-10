using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImposeView : MonoBehaviour
{
    private string conditionName;
    [SerializeField] Text conditionText;
    [SerializeField] Image conditionIcon;
    public void SetConditionName(string name)
    {
        conditionName = name;
        SetIcon();
        LocalizationName();
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

    private void LocalizationName()
    {
        if(conditionName !=null)
            conditionText.text = LocalizationManager.Localize(Condition.LOCNAME + conditionName);
    }
}
