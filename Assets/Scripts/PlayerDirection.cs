using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    private bool facingRight = true;
    private bool facingLeft = false;

    public bool FacingRight { get { return facingRight; } }
    public bool FacingLeft { get { return facingLeft; } }

    public void FaceRight()
    {
        facingRight = true;
        facingLeft = false;
    }
    public void FaceLeft()
    {
        facingRight = false;
        facingLeft = true;
    }
}