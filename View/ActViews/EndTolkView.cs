using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTolkView : MonoBehaviour
{
    [SerializeField] private Text tolk;
    private int step;
    private void Start()
    {
        LocalizationManager.LocalizationChanged += LocalizationText;
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= LocalizationText;
    }

    private void LocalizationText()
    {
        var lockey = GameEnd.Instance.ActualTolkLocKey;
        tolk.text = LocalizationManager.Localize(lockey + step.ToString());
    }

    public void ContinueTolk()
    {
        var maxTexts = GameEnd.Instance.ActualTolkMaxTexts;
        if(step > maxTexts)
            FinishTolk();
        else
        {
            LocalizationText();
            step++;
        }
    }

    public void PlayTolkSound()
    {
        GameEnd.Instance.PlayTolkSound();
    }

    private void FinishTolk()
    {
        GameEnd.Instance.ToggleTolk(false);
        step= 0;
        GameEnd.Instance.GoToEnd();
    }
}
