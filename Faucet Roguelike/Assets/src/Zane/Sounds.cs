using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]               // Allows the AudioManager to view the array of sound objects from the inspector
public class Sounds
{
    public float volume;
    public float pitch;
    public AudioClip clip;
    public AudioSource source;
    public string name;

}
