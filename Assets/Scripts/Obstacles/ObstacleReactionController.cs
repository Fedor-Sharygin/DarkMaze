using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 
/// ObstacleReactionController
/// 
/// This component defines commonly used reactions for Permanent Objects
/// for when levels change from one to another
/// 
/// </summary>
public class ObstacleReactionController : MonoBehaviour
{
    private Transform m_MainLevelParent;
    private ObjectSocket m_SpawnPointSocket;
    private void Awake()
    {
        m_MainLevelParent = GameObject.FindGameObjectWithTag($"MainLevelParent").transform;
        m_SpawnPointSocket = GameObject.FindGameObjectWithTag($"PlayerSpawnPoint").GetComponent<ObjectSocket>();

        SceneManager.sceneLoaded += SceneLoaded;
    }
    private void SceneLoaded(Scene p_Scene, LoadSceneMode _p_SceneMode)
    {
        var MLP_Objects = GameObject.FindGameObjectsWithTag($"MainLevelParent");
        foreach (var MLP_Obj in MLP_Objects)
        {
            if (MLP_Obj.scene != p_Scene)
            {
                continue;
            }
            m_MainLevelParent = MLP_Obj.transform;
            break;
        }

        var SPS_Objects = GameObject.FindGameObjectsWithTag($"PlayerSpawnPoint");
        foreach (var SPS_Obj in SPS_Objects)
        {
            if (SPS_Obj.scene != p_Scene)
            {
                continue;
            }
            m_SpawnPointSocket = SPS_Obj.GetComponent<ObjectSocket>();
            break;
        }
    }



    [System.Serializable]
    public struct PlayerReactiveComponents
    {
        public Collider2D m_PlayerCollider;
        public JumpTarget m_PlayerJumpTarget;
        public Collider2D m_ObstacleReactionCollider;
        public PlayerControls m_PlayerControlsComponent;
        public Transform m_PlayerTransform;
    }
    [SerializeField]
    private PlayerReactiveComponents m_PlayerReactiveComponents;
    
    
    public void SetPlayerStatus_Active()
    {
        m_PlayerReactiveComponents.m_PlayerCollider.enabled = true;
        m_PlayerReactiveComponents.m_PlayerJumpTarget.EnableGravity();
        m_PlayerReactiveComponents.m_ObstacleReactionCollider.enabled = true;
        m_PlayerReactiveComponents.m_PlayerControlsComponent.enabled = true;
        m_PlayerReactiveComponents.m_PlayerTransform.parent = m_MainLevelParent;
    }
    public void SetPlayerStatus_Inactive()
    {
        m_PlayerReactiveComponents.m_PlayerCollider.enabled = false;
        m_PlayerReactiveComponents.m_PlayerJumpTarget.DisableGravity();
        m_PlayerReactiveComponents.m_PlayerJumpTarget.ResetVelocity();
        m_PlayerReactiveComponents.m_ObstacleReactionCollider.enabled = false;
        m_PlayerReactiveComponents.m_PlayerControlsComponent.enabled = false;
        m_PlayerReactiveComponents.m_PlayerTransform.parent = m_MainLevelParent;
    }
}
