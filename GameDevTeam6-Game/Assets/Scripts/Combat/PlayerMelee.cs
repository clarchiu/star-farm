using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private MultiTool tool;
    private GameObject player;
    private PlayerDirection_ direction;

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
            Debug.Log("Swinging Weapon");
        }

    }
    public Collider2D[] checkMelee()
    {
        int i = 0;
        Collider2D[] hitColliders;

        hitColliders = Physics2D.OverlapBoxAll(player.transform.position, new Vector2(20f, 20f), 0f, default, default, default);

        /*  if (direction.getDirection() == playerDir.left)
          {
              hitColliders = Physics2D.OverlapBoxAll(player.transform.position, new Vector2(20f, 20f), 0f, default, default, default);
          }
          else if (direction.getDirection() == playerDir.right)
          {
              hitColliders = Physics2D.OverlapBoxAll(player.transform.position, new Vector2(20f, 20f), 0f, default, default, default);
          }
          else if (direction.getDirection() == playerDir.up)
          {
              hitColliders = Physics2D.OverlapBoxAll(player.transform.position, new Vector2(20f, 20f), 0f, default, default, default);
          }
          else if (direction.getDirection() == playerDir.down)
          {
              hitColliders = Physics2D.OverlapBoxAll(player.transform.position, new Vector2(20f, 20f), 0f, default, default, default);
          }*/

        while (i < hitColliders.Length)
        {
            //Output all of the collider names
            Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array
            i++;
        }

        return hitColliders;

    }
}