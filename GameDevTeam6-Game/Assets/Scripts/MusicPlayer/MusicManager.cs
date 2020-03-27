using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip dayClip;
    public AudioClip nightClip;

    private MusicPlayer player;

    private TimeSystem time;
    private bool flag = true;

    void Awake()
    {
        try
        {
            player = FindObjectOfType<MusicPlayer>();
            time = FindObjectOfType<TimeSystem>();
        } catch
        {
            Debug.Log("Could not find player or time prefab");

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player.MpPlayer.clip = dayClip;
        player.MpPlayer.loop = true;
        player.MpPlayer.Play();
        StartCoroutine(player.WaitForTrackToEnd());
    }

    // Update is called once per frame
    void Update()
    {
        if (!time.isDay())
        {
            if (flag)
            {
                player.SwitchMusic(nightClip);
                flag = false;
            }
        } else
        {
            if (!flag)
            {
                player.SwitchMusic(dayClip);
                flag = true;
            }
        }
    }
}
