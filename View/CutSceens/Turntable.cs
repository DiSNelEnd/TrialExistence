using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public static class Turntable 
{
    public static void TuningCuteSceens()
    {
        var tuner = new TunerCutSceens();
        tuner.TuningCuteSceens();
    }

    public static void PlayCutSceen(string name)
    {
        var cutSceen = CutSceensRepositiry.GetCutSceen(name);
        cutSceen.PlayableDirector.Play();
    }

    public static void StopCutSceen(string name)
    {
        var cutSceen = CutSceensRepositiry.GetCutSceen(name);
        cutSceen.PlayableDirector.Stop();
    }

    public static void SetBoolAnimator(string animatorName, string boolName, bool flag)
    {
        var anomated = AnimatorsRepository.GetAnimated(animatorName);
        anomated.Animator.SetBool(boolName,flag);
    }
}
