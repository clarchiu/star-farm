using UnityEngine;
using System.Collections;

public class BossAI : EnemyAI, ITargetable
{
    // Use this for initialization

    public override void KnockBack(Vector2 origin, float amount)
    {
        //should not be implemented
    }
}
