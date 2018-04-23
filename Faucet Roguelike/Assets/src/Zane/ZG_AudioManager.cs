using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ZG_AudioManager : MonoBehaviour
{

    public ZG_DynamicSounds[] cDynamicSounds;
    public ZG_FixedPitchSounds[] cFixedPitchSounds;
    public ZG_Music[] cMusic;

    public static ZG_AudioManager instance;


    // Use this for initialization
    // Loop through array of sounds and create a sound object for each one
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < cDynamicSounds.Length; i++)
        {
            cDynamicSounds[i].source = gameObject.AddComponent<AudioSource>();
            cDynamicSounds[i].source.clip = cDynamicSounds[i].clip;
            cDynamicSounds[i].source.volume = cDynamicSounds[i].mVolume;
            cDynamicSounds[i].source.pitch = cDynamicSounds[i].mPitch;
            cDynamicSounds[i].source.loop = cDynamicSounds[i].mLoop;
        }

        for (int i = 0; i < cFixedPitchSounds.Length; i++)
        {
            cFixedPitchSounds[i].source = gameObject.AddComponent<AudioSource>();
            cFixedPitchSounds[i].source.clip = cFixedPitchSounds[i].clip;
            cFixedPitchSounds[i].source.volume = cFixedPitchSounds[i].mVolume;
            cFixedPitchSounds[i].source.loop = cFixedPitchSounds[i].mLoop;
        }


        for (int i = 0; i < cMusic.Length; i++)
        {
            cMusic[i].source = gameObject.AddComponent<AudioSource>();
            cMusic[i].source.clip = cMusic[i].clip;
            cMusic[i].source.volume = cMusic[i].mVolume;
            cMusic[i].source.loop = cMusic[i].mLoop;

        }
    }

    private void Start()
    {
        playMusic("backgroundMusic1");
    }



    /******************************************************
    * playSound function                                  *
    *  argument(s): string name                           *
    *                                                     *
    * pass the name of the sound that is to be played     *
    * if sound exists, it will play with random pitch     *
    ******************************************************/

    public void playDynamicSound(string name)
    {
        for (int i = 0; i < cDynamicSounds.Length; i++)
        {
            if (cDynamicSounds[i].mClipName == name)
            {
                cDynamicSounds[i].source.pitch = Random.Range(.6f, 1.0f);
                cDynamicSounds[i].source.volume = Random.Range(.9f, 1.0f);
                cDynamicSounds[i].source.Play();
                return;
            }
         
        }
    }


    public void playFixedSound(string name)
    {
        for (int i = 0; i < cFixedPitchSounds.Length; i++)
        {
            if (cFixedPitchSounds[i].mClipName == name)
            { 
                cFixedPitchSounds[i].source.volume = Random.Range(.9f, 1.0f);
                cFixedPitchSounds[i].source.Play();
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

    public void playMusic(string name)
    {
        for (int i = 0; i < cMusic.Length; i++)
        {
            if (cMusic[i].mClipName == name)
            {
                cMusic[i].source.Play();
                return;
            }

           
        }

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

        // this loop calls a random sound, waits a random time, and repeats
        // with various volumes/pitches/panning
        while (true)
        {
            float randPlayIntervals = Random.Range(4.0f, 9.0f);

            yield return new WaitForSeconds(randPlayIntervals);

            for (int i = 0; i < cDynamicSounds.Length; i++)
            {
                cDynamicSounds[i].source.pitch = Random.Range(.6f, 1.0f);
                cDynamicSounds[i].source.volume = Random.Range(.2f, .3f);
                cDynamicSounds[i].source.panStereo = Random.Range(-1.0f, 1.0f);


            }
            cDynamicSounds[Random.Range(0, 10)].source.Play();

        }

    }
}




