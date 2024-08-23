using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerControlsScheme m_Controls;
    private InputAction m_LeanLevel;
    private InputAction m_RotateLevel;
    private void Awake()
    {
        GeneralGameBehavior.Initialize();
        GeneralGameBehavior.SwitchState(GeneralGameBehavior.GameState.DEFAULT_GAME_STATE);

        m_Controls = GeneralGameBehavior.Controls;

        m_LeanLevel = m_Controls.GameControls.Lean;
        m_RotateLevel = m_Controls.GameControls.Rotate;
    }

    private void EnableInput()
    {
        m_LeanLevel.Enable();
        m_LeanLevel.started += LeanLevel_Started;
        m_LeanLevel.canceled += LeanLevel_Canceled;

        m_RotateLevel.Enable();
        m_RotateLevel.started += RotateLevel_Started;
    }
    private void OnEnable()
    {
        EnableInput();
    }

    private void DisableInput()
    {
        m_LeanLevel.started -= LeanLevel_Started;
        m_LeanLevel.canceled -= LeanLevel_Canceled;
        m_LeanLevel.Disable();

        m_RotateLevel.started -= RotateLevel_Started;
        m_RotateLevel.Disable();
    }
    private void OnDisable()
    {
        DisableInput();
    }
    private void OnDestroy()
    {
        DisableInput();
    }



    [SerializeField]
    private LeanTarget m_LeanTarget;
    [SerializeField]
    private float m_RotationValue = 20f;
    private bool m_LeanPerformed = false;
    private void LeanLevel_Started(InputAction.CallbackContext p_CallbackContext)
    {
        if (m_LeanPerformed)
        {
            return;
        }

        float FLeanDir = m_LeanLevel.ReadValue<float>();
        int LeanDir = (FLeanDir > 0f ? 1 : (FLeanDir < 0f ? -1 : 0));
        m_LeanTarget.SetTargetRotation(LeanDir * m_RotationValue);

        m_LeanPerformed = true;
    }
    private void LeanLevel_Canceled(InputAction.CallbackContext p_CallbackContext)
    {
        if (!m_LeanPerformed)
        {
            return;
        }

        m_LeanTarget.ResetRotation();
        m_LeanPerformed = false;
    }



    private void RotateLevel_Started(InputAction.CallbackContext p_CallbackContext)
    {
        throw new System.NotImplementedException();
    }

}
