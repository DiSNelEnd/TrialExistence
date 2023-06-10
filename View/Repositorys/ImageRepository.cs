using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class ImageRepository 
{
    private static List<Sprite> conditionImages;
    public static void LoadImages()
    {
        conditionImages = Resources.LoadAll<Sprite>("Image/Conditions").ToList();
    }

    public static Sprite GetConditionSprite(string conditionName)
    {
        var sprite = conditionImages.FirstOrDefault(i=>i.name==conditionName);
        return sprite is null ? throw new NullReferenceException($"{conditionName} not found") : sprite;
    }
}
