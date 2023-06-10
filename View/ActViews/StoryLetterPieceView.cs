using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLetterPieceView : MonoBehaviour
{
    [SerializeField] private Text letterText;
    [SerializeField] private Text nsPoints;
    [SerializeField] private Text money;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button readButton;
    private StoryLetterPiece letterPiece;
    private void Start()
    {
        LocalizationManager.LocalizationChanged += Localize;
        LocalizationManager.LocalizationChanged += ShowNumbers;
        Localize();
        ButtonControl();
        letterPiece.OnFlagChanged += ButtonControl;
    }

    private void OnDestroy()
    {
        LocalizationManager.LocalizationChanged -= Localize;
        LocalizationManager.LocalizationChanged -= ShowNumbers;
        letterPiece.OnFlagChanged -= ButtonControl;
    }

    private void OnEnable()
    {
        if (letterPiece is null) return;
        Localize();
        ButtonControl();
        letterPiece.OnFlagChanged += ButtonControl;   
    }

    private void OnDisable()
    {
        letterPiece.OnFlagChanged -= ButtonControl;
    }

    public void PressNext()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        var piece = GameRoot.Game.LettersControler.NextPiece(letterPiece);
        StoryLetterPage.InstantiateStoryLetterPiece(piece);
    }

    public void PressRead()
    {
        Computer.Instance.PlayButtonSound();
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.LettersControler.ReadActualLetter();
        Mail.ToggleStoryLetterPage(false);
    }

    public void SetLetterPiece(StoryLetterPiece storyLetterPiece)
    {
        letterPiece = storyLetterPiece;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Localize()
    {
        if (letterPiece is null) return;
        letterText.text = LocalizationManager.Localize(letterPiece.LocKeyLetterText);
    }

    private void ShowNumbers()
    {
        if (letterPiece is null) return;
        if (letterPiece.NsPoint != 0)
            nsPoints.text = LocalizationManager.Localize(ServiceNsView.LOCNS, letterPiece.NsPoint);
        if (letterPiece.Money != 0)
            money.text = LocalizationManager.Localize(ServiceMoneyView.LOCMONEY, letterPiece.Money);
    }

    private void ButtonControl()
    {
        if (letterPiece is null) return;
        nextButton.gameObject.SetActive(letterPiece.ButtonNextFlag);
        readButton.gameObject.SetActive(letterPiece.ButtonReadFlag);
        if (!letterPiece.ButtonNextFlag)
            ShowNumbers();
    }
}
