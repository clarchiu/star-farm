using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public float[] playerPosition;

    public SaveData() {
        PlayerController player = GameObject.FindObjectOfType<PlayerController>();
        playerPosition = new float[] {player.transform.position.x, player.transform.position.y, player.transform.position.z};
    }
}
