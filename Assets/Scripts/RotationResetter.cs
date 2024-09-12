using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationResetter : MonoBehaviour
{
    [SerializeField]
    private float m_RotationSpeed = 5f;
    private void FixedUpdate()
    {
        if (transform.localEulerAngles.z == 0)
        {
            return;
        }

        if (Mathf.Abs(transform.localEulerAngles.z) < .5f)
        {
            transform.localEulerAngles = Vector3.zero;
            return;
        }

        transform.localEulerAngles = new Vector3(0f, 0f,
            Mathf.LerpAngle(transform.localEulerAngles.z, 0, m_RotationSpeed * Time.fixedDeltaTime));
    }
}
