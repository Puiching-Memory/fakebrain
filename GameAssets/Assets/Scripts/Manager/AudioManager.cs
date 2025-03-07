using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : Singlten<AudioManager>
{
    public AudioType[] audioTypes;
    public AudioMixer audioMixer;
    public AudioSource currentBGMSource;

    protected override void Awake()
    {
        base.Awake();
        foreach (AudioType audioType in audioTypes)
        {
            audioType.Source = gameObject.AddComponent<AudioSource>();
            audioType.Source.name = audioType.Name;
            audioType.Source.clip = audioType.Clip;
            audioType.Source.volume = audioType.volume;
            audioType.Source.loop = audioType.isLoop;
            audioType.Source.pitch = audioType.pitch;
            if (audioType.isOnAwake)
            {
                audioType.Source.playOnAwake = true;
            }
            else
            {
                audioType.Source.playOnAwake = false;
            }
            if (audioType.Group != null)
            {
                audioType.Source.outputAudioMixerGroup = audioType.Group;
            }
        }
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Play(string name)
    {
        foreach (AudioType audioType in audioTypes)
        {
            if (audioType.Name == name)
            {
                switch (audioType.clipType)
                {
                    case ClipType.SoundEffect: break;
                    case ClipType.BGM:
                        {
                            if (currentBGMSource != null)
                            {
                                if (currentBGMSource == audioType.Source) return;
                                currentBGMSource.Stop();
                            }
                            currentBGMSource = audioType.Source;
                            break;
                        }
                }
                audioType.Source.Play();
                return;
            }
        }

        Debug.Log("Not Found Music");
    }
    public void Pause(string name)
    {
        foreach (AudioType audioType in audioTypes)
        {
            if (audioType.Name == name)
            {
                audioType.Source.Pause();
                return;
            }
        }

        Debug.Log("Not Found Music");
    }
    public void Stop(string name)
    {
        foreach (AudioType audioType in audioTypes)
        {
            if (audioType.Name == name)
            {
                audioType.Source.Stop();
                return;
            }
        }
        Debug.Log("Not Found Music");
    }

    public void SetMusicValue(float value)
    {
        audioMixer.SetFloat("BGMValue", value);
    }
    public void SetSEValue(float value)
    {
        audioMixer.SetFloat("SEValue", value);
    }

    public void SliderSetMusicValue(Slider slider)
    {
        SetMusicValue(slider.value);
    }
    public void SliderSetSeValue(Slider slider)
    {
        SetSEValue(slider.value);
    }
}

