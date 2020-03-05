using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Serializable Fields
    [SerializeField] float cameraOffsetZ = -50f;

    private float maxX = 43.5f, minX = 14.5f, maxY = 50f, minY = 8f;

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
        float camX = 0, camY = 0;
        //Set camera position to min or max positions if out of bounds
        
        if (player.transform.position.x > minX && player.transform.position.x  < maxX) { camX = player.transform.position.x; }
        else if (player.transform.position.x <= minX) { camX = minX; }
        else { camX = maxX; }

        if (player.transform.position.y > minY && player.transform.position.y < maxY) { camY = player.transform.position.y; }
        else if (player.transform.position.y <= minY) { camY = minY; }
        else { camY = maxY; }
        
        transform.position = new Vector3(camX, camY, cameraOffsetZ);

    }
}
