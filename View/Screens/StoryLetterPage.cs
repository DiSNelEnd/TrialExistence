using Assets.SimpleLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLetterPage : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Text day;
    [SerializeField] private Text time;
    [SerializeField] private Text senderName;
    private readonly string locKeyName = "StoryLetter.Name.";
    private static List<StoryLetterPieceView> registers;
    private static Func<StoryLetterPieceView> GetForm;
    private void Awake()
    {
        LocalizationManager.LocalizationChanged += Localize;
        GetForm += InstantiateForm;
        registers = new List<StoryLetterPieceView>();
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= Localize;
        GetForm -= InstantiateForm;
    }

    private void OnEnable()
    {

        Localize();
        InitStoryLetterPiece();
    }

    private void OnDisable()
    {
        DeleteAllRegisters();
    }

    public static void InstantiateStoryLetterPiece(StoryLetterPiece storyLetterPiece)
    {
        var form = GetForm?.Invoke();
        if (form is null) throw new NullReferenceException("GetForm not worked");
        form.SetLetterPiece(storyLetterPiece);
        registers.Add(form);
    }

    private void InitStoryLetterPiece()
    {
        if(GameRoot.IsGameNotStart) return;
        if (GameRoot.Game.LettersControler.IsLetterNotSet) return;
        var pieces = GameRoot.Game.LettersControler.ActualLetter.GetLettersPieces();
        foreach (var piece in pieces)
            InstantiateStoryLetterPiece(piece);
    }

    private void DeleteAllRegisters()
    {
        if (GameRoot.IsGameNotStart) return;
        foreach (var reg in registers)
            reg.Delete();
        registers.Clear();
    }

    private void Localize()
    {
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.Game.LettersControler.IsLetterNotSet) return;
        var letter = GameRoot.Game.LettersControler.ActualLetter;
        day.text = letter.Day;
        time.text = letter.Time;
        senderName.text = LocalizationManager.Localize(locKeyName + letter.SenderName);
    }

    private StoryLetterPieceView InstantiateForm()
    {
        return Instantiate(Resources.Load<GameObject>("Prefabs/StoryLetterPiece"), parent).GetComponent<StoryLetterPieceView>();
    }
}
