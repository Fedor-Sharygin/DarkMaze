using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTarget : MonoBehaviour
{
    public static LeanTarget CurrentObj;

    private float[] PossibleRotations = new float[] { 0f, 90f, 180f, 270f };
    private int CurrentRotationIdx = 0;
    private float BaseRotation
    {
        get
        {
            return PossibleRotations[CurrentRotationIdx];
        }
    }

    public void ToNextBaseRotation(int p_Val)
    {
        CurrentRotationIdx = (PossibleRotations.Length + CurrentRotationIdx + p_Val) % PossibleRotations.Length;

        foreach (var OTG in BottomObject.ObjectsToGround)
        {
            OTG.transform.localEulerAngles = new Vector3(0f, 0f, -BaseRotation);
        }
    }
    public void ResetBaseRotation()
    {
        CurrentRotationIdx = 0;

        foreach (var OTG in BottomObject.ObjectsToGround)
        {
            OTG.transform.localEulerAngles = Vector3.zero;
        }
    }

    private Transform PrivateTransform;
    private void Awake()
    {
        CurrentObj = this;
        PrivateTransform = transform; //to avoid unnecessary calls to external code
    }

    private float m_TargetAdditiveRotation  = 0f;
    private float m_CurrentAdditiveRotation = 0f;
    private float m_CurrentBaseRotation = 0f;
    public Vector2 UpVector
    {
        get
        {
            return new Vector3(-Mathf.Sin(m_CurrentAdditiveRotation * Mathf.Deg2Rad), Mathf.Cos(m_CurrentAdditiveRotation * Mathf.Deg2Rad), 0f);
        }
    }
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
        //MAYBE THERE'S A MORE OPTIMAL WAY TO DO THIS!!!
        if (Mathf.Abs(m_CurrentBaseRotation - BaseRotation) <= .02f)
        {
            if (m_CurrentBaseRotation != BaseRotation)
            {
                m_CurrentBaseRotation = BaseRotation;
                PrivateTransform.rotation = Quaternion.Euler(0f, 0f, BaseRotation + m_TargetAdditiveRotation);
            }
        }
        else
        {
            m_CurrentBaseRotation = Mathf.LerpAngle(m_CurrentBaseRotation, BaseRotation, m_RotationSpeed * Time.fixedDeltaTime);
            PrivateTransform.rotation = Quaternion.Euler(0f, 0f, BaseRotation + m_TargetAdditiveRotation);
        }


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
        PrivateTransform.rotation = Quaternion.Euler(0f, 0f, m_CurrentBaseRotation + m_CurrentAdditiveRotation);
        //Debug.Log($"Current rotation: {m_BeginRotation + m_CurrentAdditiveRotation}");
    }
}
