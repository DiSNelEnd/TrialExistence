using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkStatusView : MonoBehaviour
{
    private Text status;
    private readonly string LocalizationStatus = "Work.Status";
    private void Awake()
    {
        status = GetComponent<Text>();
    }

    private void OnEnable()
    {
        if (GameRoot.IsGameNotStart) return;
        Localize();
        GameRoot.Game.WorkControler.WorkData.OnStatusChanged += Localize;
    }

    private void OnDisable()
    {
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.WorkControler.WorkData.OnStatusChanged -= Localize;
    }

    private void Start()
    {
        LocalizationManager.LocalizationChanged += Localize;
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= Localize;
    }

    private void Localize()
    {
        if (GameRoot.IsGameNotStart) return;
        status.text = LocalizationManager.Localize(GetLocalizTag(GameRoot.Game.WorkControler.WorkData.WorkStatus));
    }

    private string GetLocalizTag(WorkStatus workStatus)
    {
        return LocalizationStatus + workStatus.ToString();
    }
}
