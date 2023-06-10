using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceen : MonoBehaviour
{
    private PlayableDirector playableDirector;
    public PlayableDirector PlayableDirector => playableDirector;
    public string Name => gameObject.name;
    [SerializeField] public string type;
    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
        CutSceensRepositiry.AddCutSceen(this);
    }
}
