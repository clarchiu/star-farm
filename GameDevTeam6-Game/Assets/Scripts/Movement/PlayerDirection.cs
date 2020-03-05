using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    private Camera gameCamera = null;

    private Vector3 mousePos;
    private Vector3 playerPos;
    private float lookAngle = 0;

    void Awake(){
        FindCamera();
    }

    void FindCamera(){
        gameCamera = Camera.main;
        if (!gameCamera){
            gameObject.SetActive(false);
            Debug.LogError("No Game Camera Found!");
        }
    }

    public Quaternion GetLookDirection() {
        mousePos = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        playerPos = transform.position;
        lookAngle = Mathf.Atan2((mousePos - playerPos).y, (mousePos - playerPos).x) * Mathf.Rad2Deg;
        Vector3 eularAngles = new Vector3(0, 0, lookAngle);
        Quaternion currentRoation = Quaternion.Euler(eularAngles);
        return currentRoation;
    }
}
