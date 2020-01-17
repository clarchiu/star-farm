using System;
using UnityEngine;

public class Dregling : Enemy
{

    private int baseHealth = 50;
    protected override int BaseHealth { get => baseHealth; }
    protected override float Speed => 1f;
    protected override int Damage => 10;
    protected override string PreferredTarget => "Player";

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
