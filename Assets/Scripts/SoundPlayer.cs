using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [field: SerializeField]
    public AudioSource AudioSource { get; private set; }

    public bool IsSoundPlayed { get; set; } = false;


    public void PlaySound() 
    {

        AudioSource.Play();
    
    }

    public void PlaySoundOneShot()
    {
        if (!IsSoundPlayed)
        {
            AudioSource.Play();
            IsSoundPlayed = true;
        }
    }


    
}
