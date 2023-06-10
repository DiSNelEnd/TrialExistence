using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryLetter : Letter
{
    private readonly List<StoryLetterPiece> letterPieces;
    //private readonly int maxPieces = 10;
    public string SenderName { get; }
    public int FullNsPoint { get; private set; }
    public int FullMoney { get; private set; }
    public int LetterNumber { get; }
    public bool IsUnread => Status != LetterStatus.Read;
    public StoryLetter(
        string senderName,
        string day, 
        string time,
        int letterNumber,
        List<StoryLetterPiece> letterPieces, 
        LetterStatus status = LetterStatus.New) :base(day,time,status)
    {
        SenderName = senderName;
        this.letterPieces = letterPieces;
        foreach(var piece in letterPieces)
        {
            FullNsPoint += piece.NsPoint;
            FullMoney += piece.Money;
        }
        LetterNumber = letterNumber;
    }

    public void Read()
    {
        if (!IsUnread) return;
        StatusRead();
        if (GameRoot.IsGameNotStart) return;
        GameRoot.Game.AddNsPoints(FullNsPoint);
        GameRoot.Game.AddMoney(FullMoney);
    }

    public List<StoryLetterPiece> GetLettersPieces()
    {
        return letterPieces;
    }


    public void AddLetterPiece(StoryLetterPiece letterPiece)
    {
        letterPieces.Add(letterPiece);
        FullNsPoint += letterPiece.NsPoint;
        FullMoney += letterPiece.Money;
    }

    public void StatusUnread()
    {
        ChangeStatus(LetterStatus.Unread);
    }

    private void StatusRead()
    {
        ChangeStatus(LetterStatus.Read);
    }
}
