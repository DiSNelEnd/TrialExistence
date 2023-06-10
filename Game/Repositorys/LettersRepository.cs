using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersRepository 
{
    private readonly Dictionary<int, Letter> letters;
    public int LettersConut => letters.Count;
    public Dictionary<int, Letter> Letters => letters;
    public LettersRepository()
    {
        letters = new Dictionary<int, Letter>();
    }

    public void AddLetter(int id,Letter letter)
    {
        if (letters.ContainsKey(id))
            letters[id] = letter;
        else
            letters.Add(id, letter);
    }

    public Bill GetBill(int id)
    {
        var letter = GetLetter(id);
        return letter is Bill ? (Bill)letter : throw new Exception("letter is not Bill");
    }

    public StoryLetter GetStoryLetter(int id)
    {
        var letter = GetLetter(id);
        return letter is StoryLetter ? (StoryLetter)letter : throw new Exception("letter is not StoryLetter");
    }

    private Letter GetLetter(int id)
    {
        return letters.TryGetValue(id, out var value) ? value : throw new KeyNotFoundException("Letter not found");
    }
}
