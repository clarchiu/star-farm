using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking_Script : MonoBehaviour
{
    AudioSource my_audiosource;
    public AudioClip walking;
    // Start is called before the first frame update
    void Start()
    {
        my_audiosource = GetComponent<AudioSource>();  
    }

    void Walking_sound()
    {
        my_audiosource.PlayOneShot(walking, 0.5f);
    }
}
