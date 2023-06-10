using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StoryLettersControl 
{
    private const string BAD = "b";
    private const string GOOD = "g";
    private const string WELCOME = "w";
    private readonly string senderMother = "Mother";
    private readonly string senderFather = "Father";
    private readonly string curiosityName = "Curiosity";
    private readonly int maxLetterNumber = 10;
    private readonly int maxUnreadDayCount = 2;
    private readonly int nsPointThreshold = 1000;
    private readonly StoryLetterPiecesRepository piecesRepository;
    private int unreadLettersCount;
    private int letterNumberNow;
    private int unreadDayCount;
    public int UnreadLettersCount => unreadLettersCount;
    public int LetterNumberNow => letterNumberNow;
    public int UnreadDayCount => unreadDayCount;
    public StoryLetterPiecesRepository PiecesRepository => piecesRepository;
    public StoryLetter ActualLetter { get; private set; }
    public bool IsLetterNotSet => ActualLetter is null;
    public StoryLettersControl(int letterNumberNow, int unreadLettersCount, int unreadDayCount)
    {
        piecesRepository = new StoryLetterPiecesRepository();
        this.letterNumberNow = letterNumberNow;
        this.unreadLettersCount = unreadLettersCount;
        this.unreadDayCount = unreadDayCount;
    }

    public void SetLetter(int id)
    {
        ActualLetter = GameRoot.Game.GetStoryLetter(id);
        if (ActualLetter.Status == LetterStatus.New)
            FirstOpenBill();
    }

    public StoryLetterPiece NextPiece(StoryLetterPiece lastPiece)
    {
        var newPiece = GetRandomPiece(ActualLetter.LetterNumber, lastPiece.PieceNumber);
        lastPiece.OffButtonNext();
        ActualLetter.AddLetterPiece(newPiece);
        return newPiece;
    }

    public void ReadActualLetter()
    {
        if (!ActualLetter.IsUnread) return;
        ActualLetter.Read();
        unreadLettersCount--;
        if (unreadLettersCount == 0)
            DeleteCuriosity();
        GameRoot.Game.SaveGame();
        Room.Instance.PlayRoomMusic();
    }

    public void SubscribeOnDayChanged()
    {
        GameRoot.Game.Time.OnDayChanged += CheckDayCount;
        GameRoot.Game.Ns.OnScoreChanged += CheckSendLetter;
    }

    private void FirstOpenBill()
    {
        if (ActualLetter is StoryLetter)
            FirstOpenStoryLetter();
        ActualLetter.StatusUnread();
        Mail.OpenNewLetter();
    }

    private void FirstOpenStoryLetter()
    {
        Mail.PlayReadMusic();
    }

    private StoryLetterPiece GetRandomPiece(int letterNumber,int previousPieceNumber)
    {
        var number = Random.Range(0, 11);
        var prefix = number < 5 ? GOOD : BAD;
        return GetLetterPiece(letterNumber.ToString() + prefix, previousPieceNumber+1); 
    }

    private StoryLetterPiece GetLetterPiece(string subLocKey,int pieceNumber)
    {
        return piecesRepository.Get(subLocKey, pieceNumber);
    }

    private void SendLetter(string senderName,StoryLetterPiece storyLetterPiece,int letterNumber)
    {
        var letter = new StoryLetter(senderName, GameRoot.Game.Time.Day, 
            GameRoot.Game.Time.TimeText,letterNumber,new List<StoryLetterPiece>() { storyLetterPiece });
        unreadLettersCount++;
        GameRoot.Game.SendLetter(letter);
        letterNumberNow++;
    }

    private void SendStoryLetter()
    {
        if (letterNumberNow > maxLetterNumber) return;
        var senderName = letterNumberNow % 2 == 0 ? senderFather : senderMother;
        var letterPiece = GetLetterPiece(letterNumberNow.ToString()+WELCOME,0);
        SendLetter(senderName, letterPiece, letterNumberNow);
    }

    private void CheckSendLetter()
    {
        var nsPoints = GameRoot.Game.Ns.Score;
        if (nsPoints >= letterNumberNow * nsPointThreshold)
            SendStoryLetter();
    }

    private void CheckDayCount()
    {
        if (unreadLettersCount > 0)
            unreadDayCount++;
        if (unreadDayCount > maxUnreadDayCount)
            ImposeCondition(curiosityName);
        
    }

    private void ImposeCondition(string conName)
    {
        GameRoot.Game.ImposeCondition(conName);
    }

    private void DeleteCondition(string name)
    {
        GameRoot.Game.Player.DeleteCondition(name);
    }

    private void DeleteCuriosity()
    {
        unreadDayCount = 0;
        DeleteCondition(curiosityName);
    }
}
