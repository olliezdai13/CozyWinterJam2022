using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGrounded : PlayerBaseState
{
    public PlayerStateGrounded(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        Debug.Log("Entering Grounded state");
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
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            SetSubState(Factory.Idle());
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            SetSubState(Factory.Move());
        }
    }
    public override void CheckSwitchStates()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X))
        {
            SwitchState(Factory.Attack());
        }

        if (Ctx.Dead)
        {
            SwitchState(Factory.Dead());
        }

        //if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.X)) {
        //    SwitchState(Factory.GroundAttack());
        //} 
        //else if (Input.GetAxisRaw("Jump") != 0)
        //{
        //    SwitchState(Factory.Jump());
        //} else if (!Ctx.GroundSensor.IsGrounded)
        //{
        //    SwitchState(Factory.Air());
        //}
    }
    }
