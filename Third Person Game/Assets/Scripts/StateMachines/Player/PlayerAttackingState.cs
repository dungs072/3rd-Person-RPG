using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private Attack attack;
    private bool alreadyAppliedForce;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
        stateMachine.WeaponDamage.SetDamage(attack.Damage,attack.KnockBack);
    }
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, 0.1f);
    }
    public override void Tick(float timeDeltaTime)
    {
        Move(timeDeltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime(stateMachine.Animator);
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if(normalizedTime>=attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
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

        previousFrameTime = normalizedTime;
    }
    public override void Exit()
    {

    }
    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyAppliedForce = true;
    }
    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }
        if (normalizedTime < attack.ComboAttackTime) { return; }
        stateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                stateMachine,
                attack.ComboStateIndex
            )
        );
    }
   


}
