using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBackground : MonoBehaviour
{
    public GameObject background;
    private int tileCountX;
    private int tileCountY;

    void Start() {
        TileLayout t = FindObjectOfType<TileLayout>();
        tileCountX = t.getTileCountX();
        tileCountY = t.getTileCountY();
        background.transform.localScale = new Vector3(tileCountX, tileCountY, 1);
        background.transform.position = new Vector2(tileCountX / 2, tileCountY / 2);
    }
}
