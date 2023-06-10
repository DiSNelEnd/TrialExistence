using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLetterPieceCreator
{
    private const int SUBLOCKEY = 0;
    private const int PIECENUMBER = 1;
    private const int NSPOINT = 2;
    private const int MONEY = 3;
    private const int NEXTFLAG = 4;
    private const int READFLAG = 5;
    private readonly char splitChar = '!';
    public StoryLetterPieceCreator() { }

    public StoryLetterPiece Create(string line)
    {
        var serviceParams = line.Split(splitChar);
        var subLocKey = serviceParams[SUBLOCKEY];
        var pieceNumber = int.TryParse(serviceParams[PIECENUMBER], out var parcePieceNumber)
            ? parcePieceNumber : throw new Exception("PieceNumber not number");
        var nsPoint = int.TryParse(serviceParams[NSPOINT], out var parceNsPoint)
            ? parceNsPoint : throw new Exception("NSPoint not number");
        var money = int.TryParse(serviceParams[MONEY], out var parceMoney)
            ? parceMoney : throw new Exception("Money not number");
        var nextFlag = (int.TryParse(serviceParams[NEXTFLAG], out var parceNextFlag)
            ? parceNextFlag : throw new Exception("NextFlag not number")) == 1;
        var readFlag = (int.TryParse(serviceParams[READFLAG], out var parceReadFlag)
            ? parceReadFlag : throw new Exception("ReadFlag not number")) == 1;
        return new StoryLetterPiece(subLocKey,pieceNumber,nsPoint,money,nextFlag,readFlag);
    }
}
