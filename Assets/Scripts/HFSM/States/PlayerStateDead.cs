using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDead : PlayerBaseState
{
    public PlayerStateDead(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        Debug.Log("Entering Dead state");
        Ctx.Animator.SetTrigger("Death");

        // Set velocity to 0
        Ctx.SpeedH = 0;
        Ctx.SpeedV = 0;
        Ctx.Rb.velocity = Vector3.zero;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
    }
    public override void InitializeSubState()
    {
    }
    public override void CheckSwitchStates()
    {
    }
}
