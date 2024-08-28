using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomObject : MonoBehaviour
{
    private static bool Initialized = false;
    public static List<BottomObject> ObjectsToGround;

    public void Awake()
    {
        if (!Initialized)
        {
            ObjectsToGround = new List<BottomObject>();
            Initialized = true;
        }
        ObjectsToGround.Add(this);
    }
}
