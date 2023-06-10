using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    private Dropdown dropdown;
    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
    }

    private void Start()
    {
        InitialResolution();
    }

    private void OnEnable()
    {
        SetValue();
    }

    private void InitialResolution()
    {
        dropdown
            .AddOptions(Root
            .Instance
            .Resolutions
            .Select(r => $"{r.Item1} x {r.Item2}")
            .ToList());
        SetValue();
    }

    private void SetValue()
    {
        if (Settings.Instance.SettingsData is null) return;
        dropdown.SetValueWithoutNotify(Settings.Instance.SettingsData.indexResolution);
    }
    public void OnResolutionChanzed()
    {
        Settings.Instance.SetResolutionIndex(dropdown.value);
        Settings.Instance.ToggleWarningText(true);
    }
}
