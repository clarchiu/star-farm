using System;
using UnityEngine;

public class AttackState : IState
{
    void IState.Enter(EnemyAI parent)
    {
        throw new NotImplementedException();
    }

    void IState.Exit()
    {
        throw new NotImplementedException();
    }

    void IState.Update()
    {
<<<<<<< Updated upstream
        throw new NotImplementedException();
=======
        isAttacking = true;
        attackCoolDown = parent.MyAttributes.attackCoolDown; 

        parent.GFX.MyAnimator.SetTrigger("attack");

        targetable.RemoveHealth(parent.gameObject, parent.MyAttributes.attackDamage); 
        targetable.KnockBack(parent.transform.position, 50f); //TODO: make amount of knockback scale with damage?

        yield return new WaitForSeconds(parent.GFX.MyAnimator.GetCurrentAnimatorStateInfo(2).length); //check how long the animation is

        isAttacking = false;

        yield break;
>>>>>>> Stashed changes
    }
}
