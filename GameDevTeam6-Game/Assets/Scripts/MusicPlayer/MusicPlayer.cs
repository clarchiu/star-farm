using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
     public AudioSource MpPlayer;


     void Awake() {
         MpPlayer = gameObject.AddComponent<AudioSource>() as AudioSource;
     }

     public IEnumerator WaitForTrackToEnd()
     {
         while (MpPlayer.isPlaying)
         {
             yield return new WaitForSeconds(0.01f);

         }
         //MpPlayer.clip = dayClip;
         MpPlayer.loop = true;
         MpPlayer.Play();
     }

    public void SwitchMusic(AudioClip clip)
    {
        StartCoroutine(FadeSwitch(clip, MpPlayer, 5f));
    }

    private static IEnumerator FadeSwitch(AudioClip clip, AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

       // audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = startVolume;
    }

}
