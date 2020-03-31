using UnityEngine;
using System.Collections;

public class FirstBossAI : EnemyAI, ITargetable
{
    // Use this for initialization

    public override void KnockBack(Vector2 origin, float amount)
    {
        //should not be implemented
    }

    //protected override void SetUpAttackRangeCollider()
    //{
    //    //there is a bug with ellipsecollider editor
    //    //steps to reproduce correct sized ellipse
    //    //- add ellipsecollider2D
    //    //- set values
    //    //- then delete the ellipsecollider2D script leaving the polygon collider
    //    attackRangeCollider = GetComponent<PolygonCollider2D>(); 

    //    if (attackRangeCollider == null)
    //    {
    //        this.gameObject.SetActive(false);
    //        throw new System.Exception("missing ellipse collider on boss, add in inspector");
    //    }
    //}
}
