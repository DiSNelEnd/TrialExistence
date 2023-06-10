using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mail : MonoBehaviour
{
    private static Action OnUnreadEmailsChanged;
    private static List<ILetterView> registers;
    private static Dictionary<Type, Func<GameObject>> forms;
    private static readonly string storyLetterPageName = "StoryLetterPage";
    private static readonly string billPageName = "BillPage";
    private static readonly string paidUpTextName = "PaidUpText";
    private static readonly string payButtonName = "PayButton";
    private static readonly string LocKeyStatusDebt = "Mail.StatusDebt.";
    private static readonly string readStoryLetterMusic = "SCPx3x";
    private static int id;
    private static int unreadEmailsCount;
    public static int UnreadEmailsCount => unreadEmailsCount;
    [SerializeField] private Transform parent;
    [SerializeField] private Text unreadEmails;
    [SerializeField] private Text statusDebt;
    public static bool IsNewLetters => unreadEmailsCount > 0;
    private void Awake()
    {
        registers = new List<ILetterView>();
        OnUnreadEmailsChanged += UnreadEmailsShow;
        LocalizationManager.LocalizationChanged += Localize;
        InitialForms();
    }

    private void OnDestroy()
    {
        OnUnreadEmailsChanged -= UnreadEmailsShow;
        LocalizationManager.LocalizationChanged -= Localize;
    }

    private void OnEnable()
    {
        OnUnreadEmailsChanged?.Invoke();
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.BillControler.OnStatusDebtChanged += Localize;
        Localize();
    }

    private void OnDisable()
    {
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.BillControler.OnStatusDebtChanged -= Localize;
    }

    public static void SetUnreadEmailsCount(int unreadEmailsCount)
    {
        Mail.unreadEmailsCount = unreadEmailsCount;
    }

    public static int CreateRegister(Letter letter)
    {
        id++;
        var form = forms.TryGetValue(letter.GetType(), out var value)
            ? value() : throw new KeyNotFoundException("Form not found");
        var letterView = form.GetComponent<ILetterView>();
        letterView.SetId(id);
        registers.Add(letterView);
        unreadEmailsCount++;
        return id;
    }

    public static void DeleteAllRegister()
    {
        foreach (var mail in registers)
            mail.Delete();
        registers.Clear();
        SetUnreadEmailsCount(0);
    }

    public static void ToggleStoryLetterPage(bool flag)
    {
        Switcher.Toggle(storyLetterPageName, flag);
    }

    public static void ToggleBillPage(bool flag)
    {
        Switcher.Toggle(billPageName, flag);
    }

    public static void TogglePaidUp(bool flag)
    {
        Switcher.Toggle(paidUpTextName, flag);
        Switcher.Toggle(payButtonName, !flag);
    }

    public static void OpenNewLetter()
    {
        unreadEmailsCount--;
        OnUnreadEmailsChanged?.Invoke();
    }

    public static void PlayReadMusic()
    {
        AudioPlayer.PlayMusic(readStoryLetterMusic);
    }

    private void UnreadEmailsShow()
    {
        if (GameRoot.IsGameNotStart) return;
        unreadEmails.text = unreadEmailsCount.ToString();
        if (!IsNewLetters) MainComButtonsControl.Instance.TogglePlayNewLetterAnim(false);
    }

    private void Localize()
    {
        if (GameRoot.IsGameNotStart) return;
        var status = GameRoot.Game.BillControler.DebtStatus;
        statusDebt.text = LocalizationManager.Localize(LocKeyStatusDebt + status);   
    }

    private void InitialForms()
    {
        forms = new Dictionary<Type, Func<GameObject>>()
        {
            {typeof(Bill),()=> Instantiate(Resources.Load<GameObject>("Prefabs/MailBill"),parent)},
            {typeof(StoryLetter),()=> Instantiate(Resources.Load<GameObject>("Prefabs/MailLetter"),parent) }
        };
    }
}
