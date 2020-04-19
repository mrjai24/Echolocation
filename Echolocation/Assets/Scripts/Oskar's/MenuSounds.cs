using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    public AudioClip hooverSound;
    public AudioClip pressedSound;

    public void PlayHooverSound() {
        SoundManager.PlaySound(hooverSound);
    }
    public void PlayPressedSound()
    {
        SoundManager.PlaySound(pressedSound);
    }


}
