using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDolgingState : PlayerBaseState
{
    private readonly int DolgeBlendTreeHash = Animator.StringToHash("DolgeBlendTree");
    private readonly int DolgeForwardHash = Animator.StringToHash("DolgeForward");
    private readonly int DolgeRightHash = Animator.StringToHash("DolgeRight");
    private Vector3 dolgeInputMovement;
    private float remainingDolgeTime;
    public PlayerDolgingState(PlayerStateMachine stateMachine,Vector3 dolgeInputMovement) : base(stateMachine)
    {
        this.dolgeInputMovement = dolgeInputMovement; 
    }

    public override void Enter()
    {
        remainingDolgeTime = stateMachine.DolgeDuration;

        stateMachine.Animator.SetFloat(DolgeForwardHash, dolgeInputMovement.y);
        stateMachine.Animator.SetFloat(DolgeRightHash, dolgeInputMovement.x);
        stateMachine.Animator.CrossFadeInFixedTime(DolgeBlendTreeHash, CrossFadeDuration);

        stateMachine.Health.SetVulnerable(true);
    }
    public override void Tick(float timeDeltaTime)
    {
       
        Vector3 movement = new Vector3();
        movement += stateMachine.transform.right * dolgeInputMovement.x *
                     stateMachine.DolgeDistance / stateMachine.DolgeDuration;
        movement += stateMachine.transform.forward * dolgeInputMovement.y *
                    stateMachine.DolgeDistance / stateMachine.DolgeDuration;
        Move(movement, timeDeltaTime);
        FaceTarget();
        remainingDolgeTime -= timeDeltaTime;
        if (remainingDolgeTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetVulnerable(false);
    }

   
}
