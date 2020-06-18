using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GolemAOEAttack : MonoBehaviour
{
    FirstBossAI golem;

    private void Start()
    {
        golem = GetComponentInParent<FirstBossAI>();
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Structure"))
        {
            ITargetable targetable = collision.gameObject.GetComponent<ITargetable>();

            if (targetable != null)
            {
                //Debug.Log(collision.gameObject.name);
                targetable.RemoveHealth(golem.gameObject, golem.MyAttributes.attackDamage);
            }
        }
    }
}
