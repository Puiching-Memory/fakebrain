using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

[Serializable]
public class AudioType
{
    [HideInInspector]
    public AudioSource Source;
    public AudioClip Clip;
    public AudioMixerGroup Group;


    public string Name;
    public bool isLoop;
    public bool isOnAwake;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 5f)]
    public float pitch;
    public ClipType clipType;
}
