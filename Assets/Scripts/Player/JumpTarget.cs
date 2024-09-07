using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTarget : MonoBehaviour
{
    public static JumpTarget CurrentObj;

    private Transform PrivateTransform;
    private Rigidbody2D PrivateRigidBody;
    private void Awake()
    {
        PrivateTransform = transform;   //to avoid unnecessary calls to external code
        PrivateRigidBody = GetComponent<Rigidbody2D>(); //to avoid unnecessary calls

        CurrentObj = this;
    }

    [SerializeField]
    private Timer m_CoyoteTimer;

    [SerializeField]
    private float m_JumpForce = 20f;

    [SerializeField]
    private GroundedCheck m_GroundedCheck;

    private bool CanJump
    {
        get
        {
            return m_GroundedCheck.Grounded || m_CoyoteTimer.IsPlaying();
        }
    }

    public void Jump()
    {
        if (!CanJump)
        {
            return;
        }

        m_GroundedCheck.DisableJump();
        PrivateRigidBody.velocity += m_JumpForce * LeanTarget.CommonUpVector;
    }


    //[Space(10)]
    //[SerializeField]
    //private Transform m_SpawnPoint;
    //[SerializeField]
    //private float m_ResetSpeed = 5f;
    //private bool m_ResettingPosition = false;
    //private void FixedUpdate()
    //{
    //    if (!m_ResettingPosition)
    //    {
    //        return;
    //    }

    //    if (Vector3.Distance(PrivateTransform.localPosition, m_SpawnPoint.localPosition) <= .5f)
    //    {
    //        PrivateTransform.localPosition = m_SpawnPoint.localPosition;
    //        m_ResettingPosition = false;
    //        return;
    //    }

    //    PrivateTransform.localPosition = Vector3.Lerp(PrivateTransform.localPosition, m_SpawnPoint.localPosition, m_ResetSpeed * Time.fixedDeltaTime);
    //}

    //public void ResetPositionState()
    //{
    //    m_ResettingPosition = true;
    //}
    public void ResetVelocity()
    {
        PrivateRigidBody.velocity = Vector3.zero;
    }
    public void DisableGravity()
    {
        PrivateRigidBody.gravityScale = 0f;
    }
    public void EnableGravity()
    {
        PrivateRigidBody.gravityScale = 1f;
    }
}
