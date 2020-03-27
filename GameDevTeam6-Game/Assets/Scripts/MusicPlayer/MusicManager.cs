using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    /*AUDIOCLIP VARIABLES*/
    public AudioClip dayClip;
    public AudioClip dayClip2;
    public AudioClip dayClip3;
    public AudioClip nightClip;
    public AudioClip bossClip;

    /*MUSICKIT ENUM*/
    private MusicKits queue;

    private MusicPlayer player;

    private TimeSystem time;
    private bool flag = true;

    void Awake()
    {
        player = FindObjectOfType<MusicPlayer>();
        time = FindObjectOfType<TimeSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        queue = MusicKits.day1;
        player.MpPlayer.clip = dayClip;
        player.MpPlayer.loop = true;
        player.MpPlayer.Play();
        StartCoroutine(player.WaitForTrackToEnd());
    }

    // Update is called once per frame
    void Update()
    {
        //If it is night time
        if (!time.isDay())
        {

            if (flag)
            {

                queue++; 
                player.SwitchMusic(nightClip);
                flag = false;

                if (queue == MusicKits.boss)
                {
                    player.SwitchMusic(bossClip);
                }
                
            }

        //If it is day time
        } else
        {
            
            if (!flag)
            {
                //The following if statements cycles through three music kits after each new day.
                if (queue == MusicKits.day1)
                {   
                    player.SwitchMusic(dayClip);
                    flag = true;

                }
                if (queue == MusicKits.day2)
                {
                    player.SwitchMusic(dayClip2);
                    flag = true;
                }
                if (queue == MusicKits.day3)
                {
                    player.SwitchMusic(dayClip3);
                    flag = true;
                }
                if (queue == MusicKits.end)
                {
                    queue = MusicKits.day1;
                }
            }
        }
    }
}

public enum MusicKits
{
    day1,
    day2,
    day3,
    end,
    boss,
}