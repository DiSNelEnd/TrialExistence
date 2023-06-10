using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkResultView : MonoBehaviour
{
    [SerializeField]private Text resultName;
    [SerializeField]private Text nsPoints;
    [SerializeField]private Text money;
    private readonly string locKey = "ResultWork.";
    private readonly string nsLocKey = "Act.NsPoints";
    private readonly string moneyLocKey = "Act.Money";
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
        GameRoot.Game.WorkControler.OnResultChanged += Localize;
    }

    private void OnDisable()
    {
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.WorkControler.OnResultChanged -= Localize;
        //GameRoot.Game.WorkControler.WorkData.ResetRandomResult();
        ClearTexts();
    }

    private void Localize()
    {
        if (GameRoot.IsGameNotStart) return;
        var data = GameRoot.Game.WorkControler.WorkData.ResultOfWork;
        if (data == null) return;
        resultName.text = LocalizationManager.Localize(locKey + data.Item1);
        nsPoints.text = LocalizationManager.Localize(nsLocKey, data.Item2);
        money.text = LocalizationManager.Localize(moneyLocKey, data.Item2);
    }

    private void ClearTexts()
    {
        resultName.text = "";
        nsPoints.text = "";
        money.text = "";
    }
}
