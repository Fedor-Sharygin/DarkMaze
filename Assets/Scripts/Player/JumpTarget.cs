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
    private bool m_Jumping = false;

    [SerializeField]
    private LeanTarget m_SpaceHolder;

    public void Jump()
    {
        PrivateRigidBody.velocity += m_JumpForce * m_SpaceHolder.UpVector;
    }
}
