using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
     AudioSource MpPlayer;
     [SerializeField] AudioClip dayClip = null;
     [SerializeField] AudioClip nightClip = null;
     // Use this for initialization
     void Start () {
         MpPlayer.clip = dayClip;
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
         MpPlayer.clip = dayClip;
         MpPlayer.loop = true;
         MpPlayer.Play();
        
     }

}
