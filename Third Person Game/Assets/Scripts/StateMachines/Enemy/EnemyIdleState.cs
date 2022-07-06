using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LocomotionBlendTree = Animator.StringToHash("LocomotionBlendTree");
    private readonly int Speed = Animator.StringToHash("Speed");

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }
    

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTree, CrossFadeDuration);
        
    }
    public override void Tick(float timeDeltaTime)
    {
        Move(timeDeltaTime);
        if(IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        FaceTarget();
        stateMachine.Animator.SetFloat(Speed,0,AnimatorDampTime,timeDeltaTime);
    }
    public override void Exit()
    {
    }
}
