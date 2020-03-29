using UnityEngine;
using System.Collections;

public class BossAI : EnemyAI, ITargetable
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void KnockBack(Vector2 origin, float amount)
    {
        //should not be implemented
    }
}
