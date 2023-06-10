using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private Text valumeNum;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        SetValumeText();
    }

    private void OnEnable()
    {
        SetValue();
        SetValumeText();
    }

    private void SetValue()
    {
        if (Settings.Instance.SettingsData is null) return;
        slider.SetValueWithoutNotify(Settings.Instance.SettingsData.volume);
    }

    private void SetValumeText()
    {
        valumeNum.text = Math.Round((slider.value * 100)).ToString() + "%";
    }

    public void OnValumeChanzed()
    {
        SetValumeText();
        Settings.Instance.SetValume(slider.value);
        Settings.Instance.ToggleWarningText(true);
    }
}
