using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    private Enemy parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Dregling>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parent.Target = collision.gameObject;
            //print("target found");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parent.Target = null;
            //print("target found");
        }
    }

}
