using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static int TotalSceneCount
    {
        get
        {
            return SceneManager.sceneCountInBuildSettings;
        }
    }
    private static int CurrentScene
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }



    public static void LoadNextLevel()
    {
        SceneLoader.LoadLevelByIdxSkip(1);
    }
    public static void LoadLevelByIdxSkip(int p_LevelsToSkip)
    {
        SceneLoader.LoadLevelByIdx((CurrentScene + p_LevelsToSkip) % TotalSceneCount);
    }
    public static void LoadLevelByIdx(int p_Idx)
    {
        SceneManager.LoadSceneAsync(p_Idx, LoadSceneMode.Additive);
    }



    public static void UnloadSceneByIdx(int p_Idx)
    {
        //UnloadSceneOptions.UnloadAllEmbeddedSceneObjects - USE THIS PARAMETER IF OBJECTS ARE CREATED DURING PLAY
        SceneManager.UnloadSceneAsync(p_Idx);
    }
}
