using System;
using UnityEngine;

public delegate void OnNewTargetDelegate();

public interface ICombative
{
    void SetInAttackRange(bool inRange);

    GameObject GetTarget();

    void PrimaryAttack(ITargetable target);

    void SecondaryAttack(ITargetable target);

    event OnNewTargetDelegate OnNewTarget;
}
