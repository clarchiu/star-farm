using System;
using UnityEngine;

public class Dregling : Enemy
{
    //target should implement targetable interface
    protected override void Attack(GameObject target) 
    {
        //throw new NotImplementedException();
    }

    protected override void InitializeProperties()
    {
        speed = 3f;
        health = 50;
        damage = 10;
        preferredTarget = "Player";
    }


    void Start()
    {
        InitializeProperties();
    }

    void Update()
    {
        FollowTarget();
    }

}
