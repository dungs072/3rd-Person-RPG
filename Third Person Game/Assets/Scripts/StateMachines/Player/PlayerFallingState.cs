using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private Vector3 momentum;
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.Controller.velocity;
        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
    }
    public override void Tick(float timeDeltaTime)
    {
        Move(momentum, timeDeltaTime);
        if(stateMachine.Controller.isGrounded)
        {
            ReturnToLocomotion();
            return;
        }
        FaceTarget();
    }

    public override void Exit()
    {

    }


}