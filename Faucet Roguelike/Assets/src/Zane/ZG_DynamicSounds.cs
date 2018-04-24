using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]               // Allows the AudioManager to view the array of sound objects from the inspector
public class ZG_DynamicSounds
{
    public float mVolume;
    public float mPitch;
    public AudioClip clip;
    public AudioSource source;
    public string mClipName;
    public bool mLoop;
}