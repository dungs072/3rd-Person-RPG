using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private float duration = 1f;
    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }
    public override void Tick(float timeDeltaTime)
    {
        Move(timeDeltaTime);
        duration -= timeDeltaTime;
        if(duration<=0f)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }
    public override void Exit()
    {
        
    }

   
}
