using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// Indestructible
/// 
/// When added to an object (PREFAB!!! IS BEST)
/// it will make sure that only a single copy of it exists at a time
/// 
/// </summary>
public class Indestructible : MonoBehaviour
{
    private static Dictionary<string, Indestructible> NamedIndestructibleObjects = new Dictionary<string, Indestructible>();


    private GameObject ThisGameObject
    {
        get
        {
            return this.gameObject;
        }
    }
    private string CompleteName
    {
        get
        {
            return $"{ThisGameObject.name}---{this.tag}";
        }
    }


    private void Awake()
    {
        //same object with the same name already exists!
        //DESTROY IT!!!!!
        if (NamedIndestructibleObjects.TryAdd(CompleteName, this) == false)
        {
            GameObject.Destroy(ThisGameObject);
            return;
        }

        GameObject.DontDestroyOnLoad(ThisGameObject);
    }

    private void OnDestroy()
    {
        if (NamedIndestructibleObjects.TryGetValue(CompleteName, out var Val) == false ||
            this != Val)
        {
            return;
        }

        NamedIndestructibleObjects.Remove(CompleteName);
    }
}
