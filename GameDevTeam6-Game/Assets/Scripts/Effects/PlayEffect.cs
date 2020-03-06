using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffect : MonoBehaviour
{

    private static PlayEffect _instance;
    public static PlayEffect Instance { get { return _instance; } }

    //Singleton
    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    public ParticleSystem breakEffect;

    public void PlayBreakEffect(Vector2 pos)
    {
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = pos;
        ParticleSystem obj = Instantiate(breakEffect, pos, Quaternion.identity);
        obj.Emit(emitParams, 10);
    }
}
