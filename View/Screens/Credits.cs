using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] private Text creditsText1;
    [SerializeField] private Text creditsText2;
    private readonly string localKey = "Credits.FlyText";
    private readonly static string switchebleName = "Credits";
    private readonly int maxText = 11;
    private int step;
    private void Start()
    {
        LocalizationManager.LocalizationChanged += LocalizationText;
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= LocalizationText;
    }

    public void LocalizationText()
    {
        if (GameRoot.IsGameNotStart) return;
        if (step > maxText)
            OffCredits();
        creditsText1.text = LocalizationManager.Localize(localKey + step.ToString());
        step++;
        creditsText2.text = LocalizationManager.Localize(localKey + step.ToString());
        step++;
    }

    private void OffCredits()
    {
        //ToggleCredits(false);
        step = 0;
    }

    public static void ToggleCredits(bool flag)
    {
        Switcher.Toggle(switchebleName, flag);
    }
}
