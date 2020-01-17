using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    public Collider2D[] checkMelee()
    {
        int i = 0;
        Collider2D[] hitColliders;

        hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(20f, 20f), 0f);

        while (i < hitColliders.Length)
        {
            //Output all of the collider names
            // Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array

            if (hitColliders[i].name == "Player") ;
            {
                
            }
            i++;
        }

        return hitColliders;
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
