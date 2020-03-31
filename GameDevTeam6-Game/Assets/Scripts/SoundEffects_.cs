using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects_ : MonoBehaviour
{
    private static SoundEffects_ _instance;
    public static SoundEffects_ Instance
    {
        get
        {
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SoundEffects_>();
                }
                if (_instance == null)
                {
                    Debug.Log("Sound effects script not found!");
                }
                return _instance;
            }
        }
    }
    public AudioSource aiTalk;
    public AudioSource alarm;
    public AudioSource attacked;
    public AudioSource breaking;
    public AudioSource breathing;
    public AudioSource button;
    public AudioSource collect;
    public AudioSource craft;
    public AudioSource criticalHit;
    public AudioSource door;
    public AudioSource gameOver;
    public AudioSource grunt1;
    public AudioSource grunt2;
    public AudioSource gruntInPain;
    public AudioSource gruntInPain2;
    public AudioSource hit;
    public AudioSource harvest;
    public AudioSource laserGun;
    public AudioSource toolBite;
    public AudioSource objectPlace;
    public AudioSource reload;
    public AudioSource shortHit;
    public AudioSource startDialogue;
    public AudioSource swoosh;
    public AudioSource swordHitMetal;
    public AudioSource swordSwoosh;
    public AudioSource walk;
    public AudioSource walkLoud;

    Dictionary<SoundEffect, AudioSource> soundEffects;
        
    private void Awake()
    {
        soundEffects = new Dictionary<SoundEffect, AudioSource>();
        soundEffects.Add(SoundEffect.aiTalk, aiTalk);
        soundEffects.Add(SoundEffect.alarm, alarm);
        soundEffects.Add(SoundEffect.attacked, attacked);
        soundEffects.Add(SoundEffect.breaking, breaking);
        soundEffects.Add(SoundEffect.breathing, breathing);
        soundEffects.Add(SoundEffect.button, button);
        soundEffects.Add(SoundEffect.collect, collect);
        soundEffects.Add(SoundEffect.craft, craft);
        soundEffects.Add(SoundEffect.criticalHit, criticalHit);
        soundEffects.Add(SoundEffect.door, door);
        soundEffects.Add(SoundEffect.gameOver, gameOver);
        soundEffects.Add(SoundEffect.grunt1, grunt1);
        soundEffects.Add(SoundEffect.grunt2, grunt2);
        soundEffects.Add(SoundEffect.gruntInPain, gruntInPain);
        soundEffects.Add(SoundEffect.gruntInPain2, gruntInPain2);
        soundEffects.Add(SoundEffect.hit, hit);
        soundEffects.Add(SoundEffect.harvest, harvest);
        soundEffects.Add(SoundEffect.laserGun, laserGun);
        soundEffects.Add(SoundEffect.toolBite, toolBite);
        soundEffects.Add(SoundEffect.objectPlace, objectPlace);
        soundEffects.Add(SoundEffect.reload, reload);
        soundEffects.Add(SoundEffect.shortHit, shortHit);
        soundEffects.Add(SoundEffect.startDialogue, startDialogue);
        soundEffects.Add(SoundEffect.swoosh, swoosh);
        soundEffects.Add(SoundEffect.swordHitMetal, swordHitMetal);
        soundEffects.Add(SoundEffect.swordSwoosh, swordSwoosh);
        soundEffects.Add(SoundEffect.walk, walk);
        soundEffects.Add(SoundEffect.walkLoud, walkLoud);
    }




    public void PlaySoundEffect(SoundEffect effect)
    {
        if (soundEffects.TryGetValue(effect, out AudioSource audio)) {
            audio.Play();
        }
    }
}

public enum SoundEffect {
    aiTalk,
    alarm,
    attacked,
    breaking,
    breathing,
    button,
    collect,
    craft,
    criticalHit,
    door,
    gameOver,
    grunt1,
    grunt2,
    gruntInPain,
    gruntInPain2,
    harvest,
    hit,
    laserGun,
    toolBite,
    objectPlace,
    reload,
    shortHit,
    startDialogue,
    swoosh,
    swordHitMetal,
    swordSwoosh,
    walk,
    walkLoud
}
