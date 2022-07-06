using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LocomotionBlendTree = Animator.StringToHash("LocomotionBlendTree");
    private readonly int Speed = Animator.StringToHash("Speed");

    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTree, CrossFadeDuration);

    }
    public override void Tick(float timeDeltaTime)
    {
        Move(timeDeltaTime);
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        if(IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }
        MoveToPlayer(timeDeltaTime);
        FaceTarget();
        stateMachine.Animator.SetFloat(Speed, 1, AnimatorDampTime, timeDeltaTime);
    }
    public override void Exit()
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.ResetPath();
        } 
        stateMachine.Agent.velocity = Vector3.zero;
    }
    private void MoveToPlayer(float deltaTime)
    {
        if(stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;
            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }
}
