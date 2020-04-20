using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script managing playing sound effect from every script
public class SoundManager : MonoBehaviour
{
    public static AudioSource audioSrc;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }
}
