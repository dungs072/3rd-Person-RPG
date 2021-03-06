using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField]public InputReader InputReader { get; private set; }
    [field: SerializeField]public CharacterController Controller { get; private set; }
    [field: SerializeField]public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField]public Animator Animator { get; private set; }
    [field: SerializeField]public Targeter Targeter { get; private set; }
    [field: SerializeField]public Attack[] Attacks { get; private set; }
    [field: SerializeField]public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField]public Health Health { get; private set; }
    [field: SerializeField]public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField]public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField]public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField]public float RotationDamping { get; private set; }
    [field: SerializeField]public float DolgeDuration { get; private set; }
    [field: SerializeField]public float DolgeDistance { get; private set; }
    [field: SerializeField]public float JumpForce { get; private set; }
    public float PreviousDolgeTime { get; private set; } = Mathf.NegativeInfinity;
    public Transform MainCameraTransform { get; private set; }
    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }
    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }
    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }
    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }
    public void SetDolgeTime(float dolgeTime)
    {
        PreviousDolgeTime = dolgeTime;
    }
}
