using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AnimatorsRepository
{
    private static readonly List<AnimatedEntity> animateds = new List<AnimatedEntity>();
    public static void AddAnimated(AnimatedEntity animated)
    {
        if(!animateds.Contains(animated))
            animateds.Add(animated);
    }

    public static AnimatedEntity GetAnimated(string name)
    {
        var animated = animateds.FirstOrDefault(c => c.Name.Equals(name));
        return animated is null ? throw new NullReferenceException($"{name} not found") : animated;
    }
}
