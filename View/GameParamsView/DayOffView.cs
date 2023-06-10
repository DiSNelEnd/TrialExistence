using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DayOffView : MonoBehaviour
{
    private readonly string localozationDayOff = "Computer.Dayoff";
    private Text daysOff;
    private void Awake()
    {
        daysOff = GetComponent<Text>();
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
        if (GameRoot.IsGameNotStart) return;
        Localize();
        GameRoot.Game.WorkControler.WorkData.OnDayOffChanged += Localize;
    }

    private void OnDisable()
    {
        if (!GameRoot.IsGameNotStart)
            GameRoot.Game.WorkControler.WorkData.OnDayOffChanged -= Localize;
    }

    private void Localize()
    {
        if (!GameRoot.IsGameNotStart)
            daysOff.text = LocalizationManager.Localize(localozationDayOff, GameRoot.Game.WorkControler.WorkData.DayOffCount);
    }
}
