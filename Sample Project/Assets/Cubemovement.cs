using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubemovement : MonoBehaviour {
    public float movespeed;

	// Use this for initialization
	void Start () {
        movespeed = 2f;
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Input.GetAxis("xAxis") * Time.deltaTime * movespeed, 0f, Input.GetAxis("Vertical")*Time.deltaTime * movespeed);
	}
}
