using UnityEngine;
using System.Collections;

public class FirstBossAI : EnemyAI
{
    public override IEnumerator GetKnockedBack(Vector2 origin, float amount)
    {
        //should not be implemented
        yield return null;
    }

    public override void PrimaryAttack(ITargetable target)
    {
        return;
    }
    //    //there is a bug with ellipsecollider editor
    //    //steps to reproduce correct sized ellipse
    //    //- add ellipsecollider2D
    //    //- set values
    //    //- then delete the ellipsecollider2D script leaving the polygon collider
}
