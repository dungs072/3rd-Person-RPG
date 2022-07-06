using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{

    protected EnemyStateMachine stateMachine;
    protected const float CrossFadeDuration = 0.1f;
    protected const float AnimatorDampTime = 0.1f;
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected void FaceTarget()
    {
        if (stateMachine.Player == null) { return; }
        Vector3 direction = stateMachine.Player.transform.position -
                            stateMachine.transform.position;
        direction.y = 0f;//dont want face up and down
        stateMachine.transform.rotation = Quaternion.LookRotation(direction);

    }
    protected bool IsInChaseRange()
    {
        if (stateMachine.Player.IsDead) { return false; }
        if (stateMachine.Player == null) { return false; }
        return (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude
             <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }
    protected bool IsInAttackRange()
    {
        if (stateMachine.Player.IsDead) { return false; }
        float distancesqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return distancesqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }

}
