using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// ORC_User
/// 
/// this component is added to a single/multiple local objects
/// that can utilize the ObstacleReactionController methods
/// 
/// </summary>
public class ORC_User : MonoBehaviour
{
    public void SetPlayerStatus_Active()
    {
        ObstacleReactionController.CurrentObj.SetPlayerStatus_Active();
    }
    public void SetPlayerStatus_Inactive()
    {
        ObstacleReactionController.CurrentObj.SetPlayerStatus_Inactive();
    }
    public void PlacePlayerOnSpawn()
    {
        ObstacleReactionController.CurrentObj.PlacePlayerOnSpawn();
    }
    public void PlacePlayerInLevel()
    {
        ObstacleReactionController.CurrentObj.PlacePlayerInLevel();
    }
    public void LoadNextLevel_Complete()
    {
        ObstacleReactionController.CurrentObj.LoadNextLevel_Complete();
    }
    public void UnloadPreviousLevel()
    {
        ObstacleReactionController.CurrentObj.UnloadPreviousLevel();
    }
}
