using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : PlayerBaseState
{
    public PlayerStateMove(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        Debug.Log("Entering Move state");
    }
    public override void UpdateState()
    {
        float inputRawH = Input.GetAxisRaw("Horizontal");
        float inputRawV = Input.GetAxisRaw("Vertical");
        Vector2 moveInputClamped = Vector2.ClampMagnitude(new Vector2(inputRawH, inputRawV), 1);
        float inputH = moveInputClamped.x;
        float inputV = moveInputClamped.y;

        // Handle flip sprite
        if (inputH > Mathf.Epsilon)
        {
            Ctx.FacingLeft = false;
        } else if (inputH < -Mathf.Epsilon)
        {
            Ctx.FacingLeft = true;
        }
        Ctx.SpriteRenderer.flipX = Ctx.FacingLeft;

        // Handle movement
        Ctx.SpeedH = Ctx.MaxSpeedH * inputH;
        Ctx.SpeedV = Ctx.MaxSpeedV * inputV;
        Ctx.Rb.velocity = new Vector3(Ctx.SpeedH, 0, Ctx.SpeedV);

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
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < Mathf.Epsilon
            && Mathf.Abs(Input.GetAxisRaw("Vertical")) < Mathf.Epsilon)
        {
            SwitchState(Factory.Idle());
        }
    }
}
