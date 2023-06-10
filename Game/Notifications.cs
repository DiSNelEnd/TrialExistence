using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Notifications
{
    private static readonly string computerSwitchebleTextName = "ComputerNotification";
    private static readonly string bedSwitchebleTextName = "BedNotification";
    private static readonly string trainSwitchebleTextName = "TrainNotification";
    private static readonly string cutSceenSwitchebleTextName = "CutSceenNotification";
    private static Coroutine showText;
    public static string LocalizationKey { get; private set; }
    public static string LocalizationParams { get; private set; }
    public static bool IsMessageReady => LocalizationKey != null;
    public static void TrainShowMessage(string localizationKey)
    {
        LocalizationKey = localizationKey;
        ShowNotification(trainSwitchebleTextName);
    }

    public static void ComputerShowMessage(string localizationKey)
    {
        LocalizationKey = localizationKey;
        ShowNotification(computerSwitchebleTextName);
    }

    public static void BedShowMessage(string localizationKey)
    {
        LocalizationKey = localizationKey;
        ShowNotification(bedSwitchebleTextName);
    }

    public static void MenuShowMessage(string localizationKey)
    {
        LocalizationKey = localizationKey;
        MenuGame.Instance.ToggleMenuNotification(true);
    }

    public static void CutSceenShowMessage(string localizationKey)
    {
        LocalizationKey = localizationKey;
        LocalizationParams = GameRoot.ScipCutSceenNS.ToString();
        ShowNotification(cutSceenSwitchebleTextName);
    }

    private static void ShowNotification(string switchebleTextName)
    {
        if (showText != null)
            Root.Instance.StopCoroutine(showText);
        showText = Root.Instance.StartCoroutine(ShowText(switchebleTextName));
    }

    private static IEnumerator ShowText(string textName)
    {
        Switcher.Toggle(textName, false);
        Switcher.Toggle(textName, true);
        yield return new WaitForSeconds(3f);
        Switcher.Toggle(textName, false);
    }
}
