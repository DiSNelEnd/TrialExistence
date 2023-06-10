using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEntity : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioSource Audio => audioSource;
    public string Name => gameObject.name;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
