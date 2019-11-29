using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTrigger : MonoBehaviour
{
    private void Awake() {
        SaveData saveFile = SaveLoadManager.LoadGame();
        if (saveFile != null) {
            PlayerController player = GameObject.FindObjectOfType<PlayerController>();
            player.transform.position = new Vector3(saveFile.playerPosition[0], saveFile.playerPosition[1], saveFile.playerPosition[2]);
            Debug.Log("Game Loaded");
        }
    }

    private void OnApplicationQuit() {
        SaveLoadManager.SaveGame();
        Debug.Log("Game Saved");
    }
}
