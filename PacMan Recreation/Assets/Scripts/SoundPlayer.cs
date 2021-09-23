using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource walkingSound;
    public AudioSource IntroBGMusic;
    public AudioSource BGMusic;

    // Start is called before the first frame update
    void Start()
    {
        IntroBGMusic.Play();
        Invoke("PlayBG", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            walkingSound.Play();
            Invoke("StopSound", 1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            walkingSound.Play();
            Invoke("StopSound", 1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            walkingSound.Play();
            Invoke("StopSound", 1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            walkingSound.Play();
            Invoke("StopSound", 1.5f);
        }

    }

    void StopSound()
    {
        walkingSound.Stop();
    }

    void PlayBG()
    {
        BGMusic.Play();
    }

}
