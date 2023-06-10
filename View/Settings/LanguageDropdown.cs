using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{
    private Dropdown dropdown;
    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
    }

    private void OnEnable()
    {
        SetValue();
    }

    private void SetValue()
    {
        if (Settings.Instance.SettingsData is null) return;
        GetComponent<Dropdown>().SetValueWithoutNotify(Settings.Instance.SettingsData.indexLanguage);
    }

    public void OnLanguageChanzed()
    {
        Settings.Instance.SetLanguageIndex(dropdown.value);
        Settings.Instance.ToggleWarningText(true);
    }
}
