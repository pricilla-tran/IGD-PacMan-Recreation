using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource walkingSound;
    //public AudioSource IntroBGMusic;
    public AudioSource BGMusic;
    //private float soundTimer = 0f;
    //const float moveWait = 2.0f;
    //private int moveCycle = 0;

    // Start is called before the first frame update
    void Start()
    {
        //IntroBGMusic.Play();
        Invoke("PlayBG", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        PlayWalkingSound();

    }

    void StopSound()
    {
        walkingSound.Stop();
    }

    void PlayBG()
    {
        BGMusic.Play();
    }

    void PlayWalkingSound()
    {
        /*
        soundTimer += Time.deltaTime;
        if ((int)soundTimer == (int)moveWait && moveCycle == 0)
        {
            soundTimer = 0;
            walkingSound.Play();
            Invoke("StopSound", 1.5f);
            moveCycle++;
        }
        if ((int)soundTimer == (int)moveWait && moveCycle == 1)
        {
            soundTimer = 0;
            walkingSound.Play();
            Invoke("StopSound", 1.5f);
            moveCycle++;
        }
        if ((int)soundTimer == (int)moveWait && moveCycle == 2)
        {
            soundTimer = 0;
            walkingSound.Play();
            Invoke("StopSound", 1.5f);
            moveCycle++;
        }
        if ((int)soundTimer == (int)moveWait && moveCycle == 3)
        {
            soundTimer = 0;
            walkingSound.Play();
            Invoke("StopSound", 1.5f);
            moveCycle = 0;
        }
        */

    }

}
