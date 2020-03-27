using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMusic : MonoBehaviour
{
    public AudioClip clip;

    private MusicPlayer player;
    void Awake()
    {
        player = FindObjectOfType<MusicPlayer>();
    }
    
    // Update is called once per frame
    void Start()
    {
        player.MpPlayer.clip = clip;
        player.MpPlayer.loop = true;
        player.MpPlayer.Play();
        StartCoroutine(player.WaitForTrackToEnd());
    }
}
