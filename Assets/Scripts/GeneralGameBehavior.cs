#if UNITY_EDITOR
#define DEBUG_CONTROLS
#endif



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GeneralGameBehavior
{
    public enum GameState
    {
        MENU_STATE,
        DEFAULT_GAME_STATE,

        DEFAULT
    }
    public static GameState m_CurState { get; private set; }

    //MUST BE CALLED ONLY FROM THE MAIN MENU
    private static bool m_Initialized = false;
    private static PlayerControlsScheme m_FullPlayerControls;
    public static PlayerControlsScheme Controls
    {
        get
        {
            return m_FullPlayerControls;
        }
    }
    public static void Initialize()
    {
        if (m_Initialized)
        {
            return;
        }


        m_FullPlayerControls = new PlayerControlsScheme();
        if (m_FullPlayerControls == null)
        {
            Debug.LogError($"Could not load PlayerControlScheme. Quitting the game!");
            Application.Quit();
            return;
        }
        m_FullPlayerControls.Enable();

        GeneralGameBehavior.SwitchState(GameState.MENU_STATE);

        
        m_Initialized = true;
    }

    private static void OnGameEnd()
    {
    }

    public static void SwitchState(GameState p_GameState)
    {
        m_CurState = p_GameState;
        switch (m_CurState)
        {
            case GameState.MENU_STATE:
                {
                    m_FullPlayerControls.GameControls.Disable();
                    m_FullPlayerControls.MenuControls.Enable();
                }
                break;
            case GameState.DEFAULT_GAME_STATE:
                {
                    m_FullPlayerControls.GameControls.Enable();
                    m_FullPlayerControls.MenuControls.Disable();
                }
                break;


            default:
                { }
                break;
        }
    }
}