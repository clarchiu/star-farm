using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
     AudioSource MpPlayer;
     [SerializeField] AudioClip NightClip = null;
     [SerializeField] AudioClip NightClip = null;
     // Use this for initialization
     void Start () {
         MpPlayer.clip = FirstClip;
         MpPlayer.loop = false;
         MpPlayer.Play();
         StartCoroutine(WaitForTrackTOend());
     }

     void Awake() {
         MpPlayer = gameObject.AddComponent<AudioSource>() as AudioSource;
     }
 
     IEnumerator WaitForTrackTOend()
     {
         while (MpPlayer.isPlaying)
         {
             yield return new WaitForSeconds(0.01f);
             
         }
         MpPlayer.clip = FirstClip;
         MpPlayer.loop = true ;
         MpPlayer.Play();
        
     }

}
