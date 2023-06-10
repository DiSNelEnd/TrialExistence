using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Root : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource effects;
    [SerializeField] private AudioSource ui;
    private string[] Languages { get; set; }
    public Tuple<int, int>[] Resolutions { get; private set; }
    public Camera MainCamera => mainCamera;
    public AudioSource Music => music;
    public AudioSource Effects => effects;
    public AudioSource UI => ui;
    public static Root Instance { get; private set; }
    public Action StopCutSceen;
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ApplySettings(SettingsData settingsData)
    {
        SetResolution(settingsData.indexResolution, settingsData.isFullscreen);
        SetLanguage(settingsData.indexLanguage);
        SetVolume(settingsData.volume);
    }

    public void ResetCamera()
    {
        mainCamera.orthographicSize = 5.0f;
        mainCamera.transform.position = Vector3.zero;
    }

    private void SetResolution(int indexResolution, bool isFullscreen)
    {
        if (Resolutions.Length == 0) return;
        var resolution = Resolutions[indexResolution];
        Screen.SetResolution(resolution.Item1, resolution.Item2,isFullscreen,60);
    }

    private void SetLanguage(int indexLanguage)
    {
        LocalizationManager.Language = Languages[indexLanguage];
    }

    private void SetVolume(float volume)
    {
        Music.volume = volume;
        Effects.volume = volume;
        UI.volume = volume;
    }

    private void Awake()
    {
        LocalizationManager.Read();
        InitialInsctance();
        InitialResolutions();
        InitialLanguages();
        Settings.Instance.Start();
        EventMethods.InitialMethods();
        AudioRepository.LoadAudios();
        ImageRepository.LoadImages();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        AspectRatio.Controlling();
        if (Input.GetKeyDown(KeyCode.Escape))
            StopCutSceen?.Invoke();
    }

    private void StartGame()
    {
        Turntable.TuningCuteSceens();
        Switcher.ToggleAll(false);
        MenuGame.Instance.OpenMenuStartGame();
        MainComButtonsControl.Instance.ShowButtons();
    }

    private void InitialResolutions()
    {
        Resolutions = Screen
            .resolutions
            .Select(r => new Tuple<int, int>(r.width, r.height))
            .Distinct()
            .ToArray();
    }

    private void InitialLanguages()
    {
        Languages = new string[]
        {
            "English",
            "Russian"
        };
    }

    private void InitialInsctance()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);
    }
}
