using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sounds[] sounds;


    // Use this for initialization
    // Loop through array of sounds and create a sound object for each one
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
        }

        playMusic(1);
    }



    /******************************************************
    * playSound function                                  *
    *  argument(s): string name                           *
    *                                                     *
    * pass the name of the sound that is to be played     *
    * if sound exists, it will play with random pitch     *
    ******************************************************/

    public void playSound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                Debug.Log("Sound Request Received; Sounds Coming Soon");
                return;
            }

        }
    }


    /******************************************************
   * playMusic function                                  *
   *  argument(s): int level                             *
   *                                                     *
   * function takes the current level number and plays   *
   * the appropriate background music until the user     *
   * reaches a difficulty in which the music does not    *
   * need to be further adjusted                         *
   * ****************************************************/

    public void playMusic(int level)
    {
        if (level == 1)
        {
            // start background music for level one
            Debug.Log("Level Music Requested; Music coming soon");
        }
        else if (level == 2)
        {
            // start background music for level two
        }

        // ...
        // ...
        // cases for each level of difficulty until max reached
    }
}
