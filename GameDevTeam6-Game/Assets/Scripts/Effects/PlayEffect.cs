using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffect : MonoBehaviour
{

    private static PlayEffect _instance;
    public static PlayEffect Instance { get {
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<PlayEffect>();
        }
        if (_instance == null)
        {
            Debug.Log("PlayEffect script not found!, Add effects manager prefab to your scene!");
        }
        return _instance;
        }
    } }

    //Singleton
    private void Awake() {
    }

    public ParticleSystem breakEffect;

    public void PlayBreakEffect(Vector2 pos)
    {
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = pos;
        ParticleSystem obj = Instantiate(breakEffect, pos, Quaternion.Euler(new Vector3(0,0,45)));
        Destroy(obj, 2);
        obj.Emit(emitParams, 10);
    }
}
