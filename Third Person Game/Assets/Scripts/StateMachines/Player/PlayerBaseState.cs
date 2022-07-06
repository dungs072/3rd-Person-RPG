using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    protected const float AnimatorDampTime = 0.1f;
    protected const float CrossFadeDuration = 0.1f;
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero,deltaTime);
    }
    protected void Move(Vector3 motion,float deltaTime)
    {
        stateMachine.Controller.Move((motion+stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }
        Vector3 direction = stateMachine.Targeter.CurrentTarget.transform.position - 
                            stateMachine.transform.position;
        direction.y = 0f;//dont want face up and down
        stateMachine.transform.rotation = Quaternion.LookRotation(direction);   

    }

    protected void ReturnToLocomotion()
    {
        if(stateMachine.Targeter.CurrentTarget!=null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
