using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private MultiTool tool;
    private GameObject player;
    private PlayerDirection_ direction;
    private Health health;
    private HealthBar hpBar;
    private Rigidbody2D body;
    float attackDelay = 0;


    private void Awake()
    {
        tool = FindObjectOfType<MultiTool>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = FindObjectOfType<PlayerDirection_>();
    }
    private void Update()
    {
        if (attackDelay <= 0)
        {
            StartCoroutine(runAttack());
        }
    }
    IEnumerator runAttack()
    {
        Debug.Log("running Coroutine!");
        if (!(tool.GetMode() == ToolModes.combatMode))
        {
            yield return null;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            checkMelee();
            attackDelay = 1;
            yield return new WaitForSeconds(1f);
            attackDelay = 0;
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
      
            if(hitColliders[i].name == "fly(Clone)" || hitColliders[i].name == "fly")
            {
                health = hitColliders[i].GetComponent<Health>();
                health.RemoveHealth(20);
                hpBar = hitColliders[i].GetComponent<HealthBar>();
                hpBar.UpdateHealthBar();
                knockBackObject(hitColliders[i], 20f);
            }
            i++;
        }

        return hitColliders;

    }
    public void knockBackObject(Collider2D enemyCollider, float amount)
    {
        float knockbackX = 0;
        float knockbackY = 0;

        body = enemyCollider.GetComponent<Rigidbody2D>();

        if (direction.GetDirection() == playerDir.left)
        {
            knockbackX -= amount;
        }
        else if (direction.GetDirection() == playerDir.right)
        {
            knockbackX += amount;
        }
        else if (direction.GetDirection() == playerDir.up)
        {
            knockbackY += amount;
        }
        else if (direction.GetDirection() == playerDir.down)
        {
            knockbackY -= amount;
        }
        body.AddForce(new Vector2(knockbackX, knockbackY), ForceMode2D.Force);
    }

}