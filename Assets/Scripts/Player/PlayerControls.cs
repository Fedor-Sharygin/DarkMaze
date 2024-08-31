using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerControlsScheme m_Controls;
    private InputAction m_LeanLevel;
    private InputAction m_RotatePositiveLevel;
    private InputAction m_RotateNegativeLevel;
    private InputAction m_JumpAction;
    private void Awake()
    {
        GeneralGameBehavior.Initialize();
        GeneralGameBehavior.SwitchState(GeneralGameBehavior.GameState.DEFAULT_GAME_STATE);

        m_Controls = GeneralGameBehavior.Controls;

        m_LeanLevel = m_Controls.GameControls.Lean;
        m_RotatePositiveLevel = m_Controls.GameControls.RotatePositive;
        m_RotateNegativeLevel = m_Controls.GameControls.RotateNegative;
        m_JumpAction = m_Controls.GameControls.Jump;
    }

    public void EnableRotationInput()
    {
        m_RotatePositiveLevel.Enable();
        m_RotatePositiveLevel.started += RotatePositiveLevel_Started;
        m_RotatePositiveLevel.performed += RotatePositiveLevel_Performed;
        m_RotatePositiveLevel.canceled += RotatePositiveLevel_Canceled;

        m_RotateNegativeLevel.Enable();
        m_RotateNegativeLevel.started += RotateNegativeLevel_Started;
        m_RotateNegativeLevel.performed += RotateNegativeLevel_Performed;
        m_RotateNegativeLevel.canceled += RotateNegativeLevel_Canceled;
    }
    public void EnableInput()
    {
        m_LeanLevel.Enable();
        m_LeanLevel.started += LeanLevel_Started;
        m_LeanLevel.canceled += LeanLevel_Canceled;

        EnableRotationInput();

        m_JumpAction.Enable();
        m_JumpAction.started += JumpAction_Started;
    }
    private void OnEnable()
    {
        EnableInput();
    }


    public void DisableRotationInput()
    {
        m_RotatePositiveLevel.started -= RotatePositiveLevel_Started;
        m_RotatePositiveLevel.performed -= RotatePositiveLevel_Performed;
        m_RotatePositiveLevel.canceled -= RotatePositiveLevel_Canceled;
        m_RotatePositiveLevel.Disable();

        m_RotateNegativeLevel.started -= RotateNegativeLevel_Started;
        m_RotateNegativeLevel.performed -= RotateNegativeLevel_Performed;
        m_RotateNegativeLevel.canceled -= RotateNegativeLevel_Canceled;
        m_RotateNegativeLevel.Disable();
    }
    public void DisableInput()
    {
        m_LeanLevel.started -= LeanLevel_Started;
        m_LeanLevel.canceled -= LeanLevel_Canceled;
        m_LeanLevel.Disable();

        DisableRotationInput();

        m_JumpAction.started -= JumpAction_Started;
        m_JumpAction.Disable();
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
    private float m_RotationValue = 20f;
    private void LeanLevel_Started(InputAction.CallbackContext p_CallbackContext)
    {
        float FLeanDir = m_LeanLevel.ReadValue<float>();
        int LeanDir = (FLeanDir > 0f ? 1 : (FLeanDir < 0f ? -1 : 0));
        if (m_InDoubleTapMode)
        {
            LeanTarget.CurrentObj.ToNextBaseRotation(LeanDir);
            m_InDoubleTapMode = false;
        }
        LeanTarget.CurrentObj.SetTargetRotation(LeanDir * m_RotationValue);
    }
    private void LeanLevel_Canceled(InputAction.CallbackContext p_CallbackContext)
    {
        LeanTarget.CurrentObj.ResetRotation();
    }



    private bool m_InDoubleTapMode = false;
    private void RotatePositiveLevel_Started(InputAction.CallbackContext p_CallbackContext)
    {
        m_InDoubleTapMode = true;
    }
    private void RotatePositiveLevel_Performed(InputAction.CallbackContext p_CallbackContext)
    {
    }
    private void RotatePositiveLevel_Canceled(InputAction.CallbackContext p_CallbackContext)
    {
        m_InDoubleTapMode = false;
    }
    private void RotateNegativeLevel_Started(InputAction.CallbackContext p_CallbackContext)
    {
        m_InDoubleTapMode = true;
    }
    private void RotateNegativeLevel_Performed(InputAction.CallbackContext p_CallbackContext)
    {
    }
    private void RotateNegativeLevel_Canceled(InputAction.CallbackContext p_CallbackContext)
    {
        m_InDoubleTapMode = false;
    }



    [SerializeField]
    private JumpTarget m_JumpTarget;
    private void JumpAction_Started(InputAction.CallbackContext p_CallbackContext)
    {
        m_JumpTarget?.Jump();
    }
}
