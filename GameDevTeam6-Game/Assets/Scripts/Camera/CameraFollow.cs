using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Serializable Fields
    [SerializeField] float cameraOffsetZ = -50f;

    private float maxX = 58.5f, minX = -0.5f, maxY = 58.5f, minY = -0.5f;

    //Reference Variables
    private PlayerController player = null;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        if (!player) {
            Debug.LogError("No Player Object Found for Camera to Follow");
            gameObject.SetActive(false);
        }
        transform.position = player.transform.position;
    }

    void LateUpdate()
    {
        UpdateCameraPosition();
    }

    void UpdateCameraPosition() {
        float camX = 0, camY = 0;
        //Set camera position to min or max positions if out of bounds

        camX = player.transform.position.x;
        camY = player.transform.position.y;

        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;

        transform.position = new Vector2(camX, camY);
        Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f));
        Vector3 top = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1.0f, 0.0f));
        Vector3 bottom = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));

        if (left.x <= minX) { camX = minX + (width / 2); }
        else if (right.x >= maxX) { camX = maxX - (width/2); }
        
        if (bottom.y <= minY) { camY = minY + (height / 2); }
        else if (top.y >= maxY) { camY = maxY - (height / 2); }
        
        transform.position = new Vector3(camX, camY, cameraOffsetZ);

    }
}
