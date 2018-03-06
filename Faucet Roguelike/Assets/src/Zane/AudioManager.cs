using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sounds[] sounds;

    public bool placeHolderTheme = false;


    // Use this for initialization
    // Loop through array of sounds and create a sound object for each one
    void Awake()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;
        }
     
    }

    public void Update()
    {
        if (placeHolderTheme == true)
        {
            playMusic(1);
            placeHolderTheme = false;
        }
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
                sounds[i].source.pitch = Random.Range(.6f, 1.0f);
                sounds[i].source.volume = Random.Range(.2f, .3f);
                sounds[i].source.Play();
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
            sounds[6].source.pitch = 1.0f;
            sounds[6].source.volume = .2f;
            sounds[6].source.Play();
            
            // start background music for level one

        }
        else if (level == 2)
        {
            // start background music for level two
        }

        // ...
        // ...
        // cases for each level of difficulty until max reached
    }






    /****        Stress Test for Audio Manager            *****
     *                                                        *
     * This function plays ambient sounds indefinitely with   *
     * random play intervals, random volumes and pitches,     * 
     * and random stereo panning. This function can be called *
     * multiple times to stress test the game, as the sounds  *
     * stack rather than get replaced or overwritten.         *
     *                                                        *
     * *******************************************************/
    public void zgStressTest()
    {
        // starts a coroutine so that wait can be used between audio calls
        StartCoroutine(AudioStressTest());  
    }

    // test audio
    IEnumerator AudioStressTest()
    {
        // initiate constant looping ambience before random sounds are called

        sounds[5].source.panStereo = .5f;
        sounds[5].source.volume = .005f;
        sounds[5].source.Play();

        // this loop calls a random sound, waits a random time, and repeats
        // with various volumes/pitches/panning
        while (true)
        {
            float randPlayIntervals = Random.Range(4.0f, 9.0f);

            yield return new WaitForSeconds(randPlayIntervals);

            for (int i = 0; i < 4; i++)
            {  
                sounds[i].source.pitch = Random.Range(.6f, 1.0f);
                sounds[i].source.volume = Random.Range(.2f, .3f);
                sounds[i].source.panStereo = Random.Range(-1.0f, 1.0f);
                
                
            }
            sounds[Random.Range(0, 4)].source.Play();
           
        }

    }
}




