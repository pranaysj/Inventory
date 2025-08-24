using System;
using UnityEngine;

public enum SoundType
{
    BackgroundMusic,
    KeyboardClick,
    ButtonClick,
    TabChanged,
    ItemSelected, 
    ItemPurchased,
    ItemSold
}

public class SoundService
{
    [SerializeField] private SoundSO soundScriptableObject;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource backgroundMusic;

    public SoundService(SoundSO soundScriptableObject, AudioSource audioEffectSource, AudioSource bgMusicSource)
    {
        this.soundScriptableObject = soundScriptableObject;
        audioEffects = audioEffectSource;
        backgroundMusic = bgMusicSource;
        PlaybackgroundMusic(SoundType.BackgroundMusic, true);
    }

    public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            audioEffects.loop = loopSound;
            audioEffects.clip = clip;
            audioEffects.PlayOneShot(clip);
        }
        else
            Debug.LogError("No Audio Clip selected.");
    }

    private void PlaybackgroundMusic(SoundType soundType, bool loopSound = false)
    {
        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            backgroundMusic.loop = loopSound;
            backgroundMusic.clip = clip;
            backgroundMusic.Play();
        }
        else
            Debug.LogError("No Audio Clip selected.");
    }

    private AudioClip GetSoundClip(SoundType soundType)
    {
        Sounds sound = Array.Find(soundScriptableObject.audioList, item => item.soundType == soundType);
        if (sound.audio != null)
            return sound.audio;
        return null;
    }
}
