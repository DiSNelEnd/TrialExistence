using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AudioRepository
{
    private static List<AudioClip> musics;
    private static List<AudioClip> effects;
    private static List<AudioClip> uiSounds;
    public static void LoadAudios()
    {
        musics = Resources.LoadAll<AudioClip>("Audio/Music").ToList();
        effects = Resources.LoadAll<AudioClip>("Audio/Sounds/Effects").ToList();
        uiSounds = Resources.LoadAll<AudioClip>("Audio/Sounds/UI").ToList();
    }

    public static AudioClip GetMusic(string name)
    {
        var audio = musics.FirstOrDefault(c => c.name.Equals(name));
        return audio is null ? throw new NullReferenceException($"{name} not found") : audio;
    }

    public static AudioClip GetEffect(string name)
    {
        var audio = effects.FirstOrDefault(c => c.name.Equals(name));
        return audio is null ? throw new NullReferenceException($"{name} not found") : audio;
    }

    public static AudioClip GetUISound(string name)
    {
        var audio = uiSounds.FirstOrDefault(c => c.name.Equals(name));
        return audio is null ? throw new NullReferenceException($"{name} not found") : audio;
    }
}
