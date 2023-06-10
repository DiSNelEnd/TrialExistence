using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterView : MonoBehaviour, ILetterView
{
    [SerializeField] private Text status;
    [SerializeField] private Text senderName;
    [SerializeField] private Text nsPoint;
    [SerializeField] private Text money;
    private readonly string locKeyStatus = "MailLetter.Status.";
    private readonly string locKeyName = "StoryLetter.Name.";
    private int id;
    public void Click()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.LettersControler.SetLetter(id);
        Mail.ToggleStoryLetterPage(true);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void SetId(int id)
    {
        this.id = id;
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
        var story = GameRoot.Game.GetStoryLetter(id);
        status.text = LocalizationManager.Localize(locKeyStatus + story.Status);
        senderName.text = LocalizationManager.Localize(locKeyName + story.SenderName);
        if (story.IsUnread) return;
        nsPoint.text = LocalizationManager.Localize(ServiceNsView.LOCNS, story.FullNsPoint);
        money.text = LocalizationManager.Localize(ServiceMoneyView.LOCMONEY, story.FullMoney);
    }
}
