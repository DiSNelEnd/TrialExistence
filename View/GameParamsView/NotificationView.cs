using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NotificationView : MonoBehaviour
{
    private Text notification;
    private void Awake()
    {
        notification = GetComponent<Text>();
    }
    private void OnEnable()
    {
        Localize();
        LocalizationManager.LocalizationChanged += Localize;
    }

    private void OnDisable()
    {
        LocalizationManager.LocalizationChanged -= Localize;
    }

    private void Localize()
    {
        if (Notifications.IsMessageReady)
            notification.text = LocalizationManager.Localize(Notifications.LocalizationKey,Notifications.LocalizationParams);
    }
}
