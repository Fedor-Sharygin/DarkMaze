using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTarget : MonoBehaviour
{
    private Rigidbody2D PrivateRigidBody;
    private void Awake()
    {
        PrivateRigidBody = GetComponent<Rigidbody2D>(); ; //to avoid unnecessary calls to external code
    }

    [SerializeField]
    private Timer m_CoyoteTimer;

    [SerializeField]
    private float m_JumpForce = 20f;

    private bool CanJump
    {
        get
        {
            return GroundedCheck.CurrentObj.Grounded || m_CoyoteTimer.IsPlaying();
        }
    }

    public void Jump()
    {
        if (!CanJump)
        {
            return;
        }

        GroundedCheck.CurrentObj.DisableJump();
        PrivateRigidBody.velocity += m_JumpForce * LeanTarget.CurrentObj.UpVector;
    }
}
