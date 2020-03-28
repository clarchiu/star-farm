using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private MultiTool tool;
    private GameObject player;
    private PlayerDirection_ direction;
    float attackDelay = 0;


    private void Awake()
    {
        tool = FindObjectOfType<MultiTool>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = FindObjectOfType<PlayerDirection_>();
    }

    //make the check before running the coroutine, more efficient that way -Clarence
    private void Update()
    {
        if (attackDelay <= 0 && tool.GetMode() == ToolModes.combatMode && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(RunAttack());
        }
    }
    IEnumerator RunAttack()
    {
        checkMelee();
        GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);

        yield return new WaitForSeconds(0.5f);
        //  attackDelay = 1;
        //
        // attackDelay = 0;
    }

    public Collider2D[] CheckMelee()
    {
        float hitPosX = player.transform.position.x;
        float hitPosY = player.transform.position.y;
        Collider2D[] hitColliders;

        if (direction.GetDirection() == playerDir.left)
        {
            hitPosX -= 0.75f;
        }
        else if (direction.GetDirection() == playerDir.right)
        {
            hitPosX += 0.75f;
        }
        else if (direction.GetDirection() == playerDir.up)
        {
            hitPosY += 0.75f;
        }
        else if (direction.GetDirection() == playerDir.down)
        {
            hitPosY -= 0.75f;
        }
        hitColliders = Physics2D.OverlapBoxAll(new Vector2(hitPosX, hitPosY), new Vector2(1.5f, 1.5f), 0f);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            //Output all of the collider names
            //Increase the number of Colliders in the array
            if (hitColliders[i].CompareTag("Enemy"))
            {

                /*can't assume rigidbody for enenmies
                 *use ITargetable interface instead
                 *- Clarence
                 */
                ITargetable targetable = hitColliders[i].GetComponent<ITargetable>();
                targetable.RemoveHealth(player, 15);
                targetable.KnockBack(player.transform.position, 1f);
            }
        }

        return hitColliders;
    }
}
