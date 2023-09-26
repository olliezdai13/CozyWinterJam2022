using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack : PlayerBaseState
{
    private const int ATTACK_DAMAGE = 1;
    private const float SPAWN_DELAY = 0.2f;
    public PlayerStateAttack(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Debug.Log("Entering Attack state");

        Ctx.Animator.SetTrigger("Attack");

        // Set velocity to 0 (cancel all movement)
        Ctx.SpeedH = 0;
        Ctx.SpeedV = 0;
        Ctx.Rb.velocity = Vector3.zero;

        // Attack
        Ctx.AttackDuration = 0.27f;
        Ctx.AttackStartTime = Time.time;
        Ctx.DelayedThrowSnowball(SPAWN_DELAY, ATTACK_DAMAGE);
    }
    public override void UpdateState()
    {
        Ctx.AttackTime = Time.time - Ctx.AttackStartTime;
        if (Ctx.AttackBuffer > 0)
        {
            Ctx.AttackBuffer = Mathf.Clamp(Ctx.AttackBuffer - Time.deltaTime, 0, 1);
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X))
        {
            Ctx.AttackBuffer = 0.2f;
        }

        CheckSwitchStates();
    }
    public override void ExitState()
    {
        Ctx.AttackStartTime = 0;
        Ctx.AttackBuffer = 0;
    }
    public override void InitializeSubState()
    {
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.AttackTime >= Ctx.AttackDuration && Ctx.AttackBuffer > 0)
        {
            
            SwitchState(Factory.Attack());
        }
        else if (Ctx.AttackTime >= Ctx.AttackDuration)
        {
            SwitchState(Factory.Grounded());
        }
    }
}
