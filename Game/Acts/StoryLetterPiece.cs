using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLetterPiece
{
    private readonly string locKey = "StoryLetter.";
    public string SubLocKey { get; }
    public bool ButtonNextFlag { get; private set; }
    public bool ButtonReadFlag { get; private set; }
    public int NsPoint { get; }
    public int Money { get; }
    public int PieceNumber { get; }
    public Action OnFlagChanged;
    public string LocKeyLetterText => locKey + SubLocKey+PieceNumber.ToString();
    public StoryLetterPiece(string subLocKey, int pieceNumber,int nsPoint,int money,bool buttonNextFlag,bool buttonReadFlag)
    {
        SubLocKey = subLocKey;
        ButtonNextFlag = buttonNextFlag;
        ButtonReadFlag = buttonReadFlag;
        NsPoint = nsPoint;
        Money = money;
        PieceNumber = pieceNumber;
    }

    public void OffButtonNext()
    {
        ButtonNextFlag = false;
        OnFlagChanged?.Invoke();
    }
}
