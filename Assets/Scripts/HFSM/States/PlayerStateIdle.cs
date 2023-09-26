using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerBaseState
{
    public PlayerStateIdle(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        Debug.Log("Entering Idle state");

        // Set velocity to 0 (necessary for jump -> idle)
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
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            SwitchState(Factory.Move());
        }
    }
}
