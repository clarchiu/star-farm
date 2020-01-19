using System;
using UnityEngine;

public class Dregling : Enemy
{

    private int baseHealth = 50;
    protected override int BaseHealth => baseHealth; 
    public override float Speed => 1f;
    protected override int Damage => 10;
    protected override string PreferredTarget => "Player";
    public override float AttackRange => 1.2f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

}
