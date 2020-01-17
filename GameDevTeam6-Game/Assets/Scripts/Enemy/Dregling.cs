using System;
using UnityEngine;

public class Dregling : Enemy
{

    private int health = 50;
    protected override int Health { get => health; set => health = value; }
    protected override float Speed => 1f;
    protected override int Damage => 10;
    protected override string PreferredTarget => "Player";

    //target should implement targetable interface
    protected override void Attack(GameObject target)
    {
        //throw new NotImplementedException();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        FollowTarget();
        base.Update();
    }

}
