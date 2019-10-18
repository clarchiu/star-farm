using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Serializable Fields
    [SerializeField] float cameraOffsetZ = -50f;

    //Reference Variables
    private PlayerController player = null;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        if (!player) {
            Debug.LogError("No Player Object Found for Camera to Follow");
            gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        UpdateCameraPosition();
    }

    void UpdateCameraPosition() {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cameraOffsetZ);
    }
}
