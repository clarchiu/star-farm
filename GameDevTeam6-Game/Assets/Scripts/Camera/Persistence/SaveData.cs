using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float[] playerPosition;

    public SaveData() {
        PlayerController player = GameObject.FindObjectOfType<PlayerController>();
        playerPosition = new float[] {player.transform.position.x, player.transform.position.y, player.transform.position.z};
    }
}
