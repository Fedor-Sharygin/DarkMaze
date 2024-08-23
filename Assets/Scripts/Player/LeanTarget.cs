using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTarget : MonoBehaviour
{
    private float[] PossibleRotations = new float[] { 0f, 90f, 180f, 270f };
    private int CurrentRotationIdx = 0;
    private float BaseRotation
    {
        get
        {
            return PossibleRotations[CurrentRotationIdx];
        }
    }

    private Transform PrivateTransform;
    private void Awake()
    {
        PrivateTransform = transform; //to avoid unnecessary calls to external code
    }

    private float m_TargetAdditiveRotation  = 0f;
    private float m_CurrentAdditiveRotation = 0f;
    /// <summary>
    /// Sets the target rotation for the object to rotate to
    /// </summary>
    /// 
    /// <param name="p_TargetAngle">
    /// Target angle in Degrees
    /// </param>
    public void SetTargetRotation(float p_TargetAngle)
    {
        m_TargetAdditiveRotation = p_TargetAngle;
    }
    public void ResetRotation()
    {
        m_TargetAdditiveRotation = 0f;
    }

    private void FixedUpdate()
    {
        ReachRotation();
    }

    [SerializeField]
    private float m_RotationSpeed = 5f;
    private void ReachRotation()
    {
        if (Mathf.Abs(m_CurrentAdditiveRotation - m_TargetAdditiveRotation) <= .02f)
        {
            if (m_CurrentAdditiveRotation != m_TargetAdditiveRotation)
            {
                m_CurrentAdditiveRotation = m_TargetAdditiveRotation;
                PrivateTransform.rotation = Quaternion.Euler(0f, 0f, BaseRotation + m_TargetAdditiveRotation);
            }
            return;
        }

        m_CurrentAdditiveRotation = Mathf.Lerp(m_CurrentAdditiveRotation, m_TargetAdditiveRotation, m_RotationSpeed * Time.fixedDeltaTime);
        PrivateTransform.rotation = Quaternion.Euler(0f, 0f, BaseRotation + m_CurrentAdditiveRotation);
        //Debug.Log($"Current rotation: {m_BeginRotation + m_CurrentAdditiveRotation}");
    }
}
