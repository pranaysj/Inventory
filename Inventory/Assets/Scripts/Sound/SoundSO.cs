using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Sound/SoundSO")]
public class SoundSO : ScriptableObject
{
    public Sounds[] audioList;
}

[Serializable]
public struct Sounds
{
    public SoundType soundType;
    public AudioClip audio;
}
