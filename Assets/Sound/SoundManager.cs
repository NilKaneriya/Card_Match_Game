using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    public AudioSource sfxSource;
    public AudioClip mis_Match_Sound;
    public AudioClip match_Sound;
    public AudioClip flip_Sound;
    public AudioClip distroy_Sound;
    private void Awake()
    {
        instance = this;
    }


    public void PlaySFX(SoundClip clipName)
    {
        AudioClip clip = null;
        switch (clipName)
        {
            case SoundClip.DestroySound:
                clip = distroy_Sound;
                break;
            case SoundClip.MatchSound:
                clip = match_Sound;
                break;
            case SoundClip.MatchMiss:
                clip = mis_Match_Sound;
                break;
            case SoundClip.FlipSound:
                clip = flip_Sound;
                break;
        }

        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }


    public void Mute()
    {
        sfxSource.volume = 0;
    }
    public void UnMute()
    {
        sfxSource.volume = 1;
    }
    public void SetVolume(float volume)
    {
        sfxSource.volume = 1;
    }
}
public enum SoundClip
{
    MatchMiss,
    MatchSound,
    FlipSound,
    DestroySound
}
