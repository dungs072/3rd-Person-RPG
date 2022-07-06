using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetDamage(stateMachine.AttackDamage,stateMachine.KnockBack);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, CrossFadeDuration);
    }
    public override void Tick(float timeDeltaTime)
    {
        FaceTarget();
        if(GetNormalizedTime(stateMachine.Animator)>=1f)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit()
    {
       
    }
   
}
