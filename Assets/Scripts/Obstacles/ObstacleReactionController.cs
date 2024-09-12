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
    //private void OnDestroy()
    //{
    //    Debug.LogWarning($"HUH!HUHHSDIHNL?>?!");
    //}
    //private void OnDisable()
    //{
    //    Debug.LogWarning($"HUH!HUHHSDIHNL?>?!");
    //}

    public static ObstacleReactionController CurrentObj;

    //private Transform m_MainLevelParent;
    private ObjectSocket m_SpawnPointSocket;
    private OuterBorderSocketManager m_OBSM;
    private void Start()
    {
        CurrentObj = this;

        //m_MainLevelParent = GameObject.FindGameObjectWithTag($"MainLevelParent").transform;
        m_SpawnPointSocket = GameObject.FindGameObjectWithTag($"PlayerSpawnPoint").GetComponent<ObjectSocket>();
        m_OBSM = GameObject.FindGameObjectWithTag($"OuterBorderHolder").GetComponent<OuterBorderSocketManager>();

        SceneManager.sceneLoaded += SceneLoaded;
    }
    private void SceneLoaded(Scene p_Scene, LoadSceneMode _p_SceneMode)
    {
        //var MLP_Objects = GameObject.FindGameObjectsWithTag($"MainLevelParent");
        //foreach (var MLP_Obj in MLP_Objects)
        //{
        //    if (MLP_Obj.scene != p_Scene)
        //    {
        //        continue;
        //    }
        //    m_MainLevelParent = MLP_Obj.transform;
        //    break;
        //}

        var SPS_Objects = GameObject.FindGameObjectsWithTag($"PlayerSpawnPoint");
        foreach (var SPS_Obj in SPS_Objects)
        {
            if (SPS_Obj.scene == m_SpawnPointSocket.gameObject.scene)
            {
                continue;
            }
            m_SpawnPointSocket = SPS_Obj.GetComponent<ObjectSocket>();
            break;
        }

        var OBSM_Objects = GameObject.FindGameObjectsWithTag($"OuterBorderHolder");
        foreach(var OBSM_Obj in OBSM_Objects)
        {
            if (OBSM_Obj.scene == m_OBSM.gameObject.scene)
            {
                continue;
            }
            m_OBSM = OBSM_Obj.GetComponent<OuterBorderSocketManager>();
            break;
        }

        //NEWLY LOADED SCENE MEANS THAT THE LEVEL CHANGED
        //PLACE THE PLAYER ON THE NEW SPAWN POINT
        PlacePlayerOnSpawn();
    }


    [SerializeField]
    private Transform m_PlayerParentTransform;
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
        m_PlayerReactiveComponents.m_PlayerJumpTarget.ResetVelocity();
        m_PlayerReactiveComponents.m_PlayerJumpTarget.EnableGravity();
        m_PlayerReactiveComponents.m_ObstacleReactionCollider.enabled = true;
        m_PlayerReactiveComponents.m_PlayerControlsComponent.enabled = true;
    }
    public void SetPlayerStatus_Inactive()
    {
        m_PlayerReactiveComponents.m_PlayerCollider.enabled = false;
        m_PlayerReactiveComponents.m_PlayerJumpTarget.DisableGravity();
        m_PlayerReactiveComponents.m_PlayerJumpTarget.ResetVelocity();
        m_PlayerReactiveComponents.m_ObstacleReactionCollider.enabled = false;
        m_PlayerReactiveComponents.m_PlayerControlsComponent.enabled = false;
    }

    public void PlacePlayerOnSpawn()
    {
        LeanTarget.CompleteRotationReset();
        SetPlayerStatus_Inactive();
        if (m_SpawnPointSocket == null)
        {
            return;
        }
        m_SpawnPointSocket.Stack(m_PlayerReactiveComponents.m_PlayerTransform);
    }
    public void PlacePlayerInLevel()
    {
        //LeanTarget.CompleteRotationReset();
        SetPlayerStatus_Active();
        if (m_SpawnPointSocket == null || m_SpawnPointSocket.AvailableForStack)
        {
            return;
        }
        m_SpawnPointSocket.RemoveObj().parent = m_PlayerParentTransform;
        //m_PlayerReactiveComponents.m_PlayerTransform.parent = m_PlayerParentTransform;
    }


    public void LoadNextLevel_Complete()
    {
        m_OBSM.RemoveBorders();
        SceneLoader.LoadNextLevel();
    }
    public void UnloadPreviousLevel()
    {
        SceneLoader.UnloadPreviousLevel();
    }
}
