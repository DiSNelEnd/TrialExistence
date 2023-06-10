using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioPlayer 
{
    public static void PlayMusic(string musicName)
    {
        var clip= AudioRepository.GetMusic(musicName);
        Root.Instance.Music.clip=clip;
        Root.Instance.Music.Play();
    }

    public static void PlayNextMusic(string musicName)
    {
        var delay = Root.Instance.Music.clip.length - Root.Instance.Music.time;
        var clip=AudioRepository.GetMusic(musicName);
        Root.Instance.Music.clip = clip;
        Root.Instance.Music.PlayDelayed(delay);
    }

    public static void PlayEffectOneShot(string effectName)
    {
        var clip = AudioRepository.GetEffect(effectName);
        Root.Instance.Effects.PlayOneShot(clip);
    }

    public static void PlayEffectLoop(string effectName)
    {
        var clip = AudioRepository.GetEffect(effectName);
        Root.Instance.Effects.clip = clip;
        Root.Instance.Effects.Play();
    }

    public static void PlayUISound(string soundName)
    {
        var clip = AudioRepository.GetUISound(soundName);
        Root.Instance.UI.PlayOneShot(clip);
    }

    public static void StopEffectSound()
    {
        Root.Instance.Effects.Stop();
    }
}
