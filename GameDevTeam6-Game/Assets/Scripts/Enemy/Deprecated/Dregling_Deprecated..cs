using System;
using UnityEngine;

public class DeprecatedDregling : DeprecatedEnemy
{

    private int baseHealth = 50;
    protected override int BaseHealth => baseHealth; 
    public override float Speed => 1f;
    protected override int Damage => 10;
    protected override string PreferredTarget => "Player";
    public override float AttackRange => 0.8f;

    protected override void Start()
    {
        throw new System.Exception("deprecated, don't use this -Clarence");
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

}
