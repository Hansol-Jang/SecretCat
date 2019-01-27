using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioSource bgm_source;
    public AudioClip bgm0;
    public AudioClip portal0;
    public AudioClip stair0;
    public AudioClip cat_die0;
    public AudioClip cat_atk0;
    public AudioClip[] bone;
    public AudioClip[] dog_atk;
    public AudioClip[] dog_far_atk;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); //파괴되지않아!
    }

    public void SingleSound(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void RandomizeSound(AudioSource audioSource, params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        audioSource.clip = clips[randomIndex];
        audioSource.Play();
    }

    public void SoundStop(AudioSource audioSource)
    {
        audioSource.Stop();
    }
}
