using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private MultiTool tool;
    private GameObject player;
    private PlayerDirection_ direction;
    private Health health;

    private void Awake()
    {
        tool = FindObjectOfType<MultiTool>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = FindObjectOfType<PlayerDirection_>();
    }
    private void Update()
    {
        runAttack();

    }
    void runAttack()
    {
        if (!(tool.GetMode() == ToolModes.combatMode))
        {
            return;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            checkMelee();
        }

    }
    public Collider2D[] checkMelee()
    {
        int i = 0;
        float hitPosX = player.transform.position.x;
        float hitPosY = player.transform.position.y;
        Collider2D[] hitColliders;
        
          if (direction.GetDirection() == playerDir.left)
          {
            hitPosX -= 10;
          }
          else if (direction.GetDirection() == playerDir.right)
          {
            hitPosX += 10;
          }
          else if (direction.GetDirection() == playerDir.up)
          {
            hitPosY += 10;
          }
          else if (direction.GetDirection() == playerDir.down)
          {
            hitPosY -= 10;
          }
          hitColliders = Physics2D.OverlapBoxAll(new Vector2(hitPosX, hitPosY), new Vector2(20f, 20f), 0f);


        while (i < hitColliders.Length)
        {
            //Output all of the collider names
          // Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array
      
            if(hitColliders[i].name == "fly(Clone)")
            {
                health = hitColliders[i].GetComponent<Health>();
                health.removeHealth(20);
               
            }
            i++;
        }

        return hitColliders;

    }
}