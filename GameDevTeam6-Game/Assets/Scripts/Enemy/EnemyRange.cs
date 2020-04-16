using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyRange : MonoBehaviour
{
    public EnemyAI enemy;
    public Collider2D attackRangeCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (!attackRangeCollider)
        {
            this.gameObject.SetActive(false);
            throw new System.Exception("missing collider on attack range, add in inspector");
        }

        enemy.OnNewTarget += CheckTargetInRange;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (GameObject.ReferenceEquals(enemy.Target, collision.gameObject))
        {
            enemy.IsTargetInAttackRange = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (GameObject.ReferenceEquals(enemy.Target, collision.gameObject))
        {
            enemy.IsTargetInAttackRange = false;
        }
    }

    private void CheckTargetInRange()
    {
        if (this.attackRangeCollider.IsTouching(enemy.Target.GetComponent<BoxCollider2D>()))
        {
            enemy.IsTargetInAttackRange = true;
            //Debug.Log("in range true");
        }
        else
        {
            enemy.IsTargetInAttackRange = false;
            //Debug.Log("in range false");
        }
    }
}
