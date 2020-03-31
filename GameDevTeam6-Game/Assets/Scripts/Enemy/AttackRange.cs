using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackRange : MonoBehaviour
{
    private ICombative parent;
    private Collider2D attackRangeCollider;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<ICombative>();
        attackRangeCollider = GetComponent<Collider2D>();

        if (attackRangeCollider == null)
        {
            this.gameObject.SetActive(false);
            throw new System.Exception("missing collider on attack range, add in inspector");
        }

        parent.OnNewTarget += checkTargetInRange;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.ReferenceEquals(parent.GetTarget(), collision.gameObject))
        {
            parent.SetInAttackRange(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (GameObject.ReferenceEquals(parent.GetTarget(), collision.gameObject))
        {
            parent.SetInAttackRange(false);
        }
    }

    private void checkTargetInRange()
    {
        if (this.attackRangeCollider.IsTouching(parent.GetTarget().GetComponent<BoxCollider2D>()))
        {
            parent.SetInAttackRange(true);
            Debug.Log("in range true");
        }
        else
        {
            parent.SetInAttackRange(false);
            Debug.Log("in range false");
        }
    }
}
