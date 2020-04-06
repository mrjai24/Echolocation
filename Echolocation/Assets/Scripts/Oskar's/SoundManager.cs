using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static List <AudioClip> audioClips;
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
