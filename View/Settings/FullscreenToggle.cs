using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    Toggle toggle;
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        SetValue();
    }

    private void SetValue()
    {
        if (Settings.Instance.SettingsData is null) return;
        toggle.SetIsOnWithoutNotify(Settings.Instance.SettingsData.isFullscreen);
    }

    public void OnFullscreenChanzed()
    {
        Settings.Instance.SetIsFullscreen(toggle.isOn);
        Settings.Instance.ToggleWarningText(true);
    }
}
