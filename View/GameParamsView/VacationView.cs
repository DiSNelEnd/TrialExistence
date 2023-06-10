using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class VacationView : MonoBehaviour
{
    private readonly string localozationVacation = "Computer.Vacation";
    private Text vacationDays;
    private void Awake()
    {
        vacationDays = GetComponent<Text>();
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
        GameRoot.Game.WorkControler.WorkData.OnVacationChenged += Localize;
    }

    private void OnDisable()
    {
        if (!GameRoot.IsGameNotStart)
            GameRoot.Game.WorkControler.WorkData.OnVacationChenged -= Localize;
    }

    private void Localize()
    {
        if (!GameRoot.IsGameNotStart)
            vacationDays.text = LocalizationManager.Localize(localozationVacation, GameRoot.Game.WorkControler.WorkData.VacationDayCount);
    }
}
