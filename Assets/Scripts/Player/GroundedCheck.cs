using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    private bool m_Grounded = false;
    public bool Grounded
    {
        get
        {
            return m_Grounded;
        }
    }

    [SerializeField]
    private Timer m_CoyoteTimer;
    private void OnTriggerEnter2D(Collider2D p_Other)
    {
        m_ActionJump = false;
        m_CoyoteTimer.ResetTimer();
        m_CoyoteTimer.Stop();
        m_Grounded = true;
    }
    private void OnTriggerExit2D(Collider2D p_Other)
    {
        if (!m_ActionJump)
        {
            m_CoyoteTimer.ResetTimer();
            m_CoyoteTimer.Play();
        }
        m_Grounded = false;
    }

    private bool m_ActionJump = false;
    public void DisableJump()
    {
        m_ActionJump = true;
    }
}
