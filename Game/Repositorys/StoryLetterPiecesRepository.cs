using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryLetterPiecesRepository
{
    private readonly List<string> storyLetterPiecesLine;
    private readonly StoryLetterPieceCreator creator;
    public StoryLetterPiecesRepository()
    {
        storyLetterPiecesLine = ParamsReader.GetLetterPiecesParams().ToList();
        creator = new StoryLetterPieceCreator();
    }

    public StoryLetterPiece Get(string subLocKey,int pieseNumber)
    {
        var startString = subLocKey + "!" + pieseNumber.ToString();
        var line = storyLetterPiecesLine.FirstOrDefault(c => c.StartsWith(startString));
        return line != null ? creator.Create(line) : throw new NullReferenceException($"{startString} not found");
    }
}
