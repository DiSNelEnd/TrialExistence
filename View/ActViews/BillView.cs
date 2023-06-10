using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillView : MonoBehaviour, ILetterView
{
    [SerializeField] private Text status;
    private readonly string locKeyStatus = "MailLetter.Status.";
    private int id;
    public void Delete()
    {
        Destroy(gameObject);
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void Click()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.BillControler.SetBill(id);
        Mail.ToggleBillPage(true);
        Mail.TogglePaidUp(GameRoot.Game.BillControler.Bill.IsPaidUp);
    }

    private void Start()
    {
        LocalizationManager.LocalizationChanged += Localize;
    }

    private void OnEnable()
    {
        Localize();
        Letter.OnStatusChanged += Localize;
    }
    private void OnDisable()
    {
        Letter.OnStatusChanged -= Localize;
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= Localize;
    }

    private void Localize()
    {
        if (GameRoot.IsGameNotStart) return;
        var bill = GameRoot.Game.GetBill(id);
        status.text = LocalizationManager.Localize(locKeyStatus + bill.Status);
    }
}

public interface ILetterView
{
    public void SetId(int id);
    public void Delete();
    public void Click();
}
