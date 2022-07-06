using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
   
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash,CrossFadeDuration);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }
   
    public override void Tick(float timeDeltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }
        Vector3 movement = CalculateMovement();
        Move(movement*stateMachine.FreeLookMovementSpeed, timeDeltaTime);
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, AnimatorDampTime, timeDeltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, timeDeltaTime);
        FaceMovementDirection(movement,timeDeltaTime);
    }
    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
    private void FaceMovementDirection(Vector3 movement,float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
                    stateMachine.transform.rotation,
                    Quaternion.LookRotation(movement),
                    deltaTime*stateMachine.RotationDamping);
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }
    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        return forward*stateMachine.InputReader.MovementValue.y+
               right*stateMachine.InputReader.MovementValue.x;
    }
}
