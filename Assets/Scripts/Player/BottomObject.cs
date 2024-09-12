using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomObject : MonoBehaviour
{
    public static List<BottomObject> ObjectsToGround = new List<BottomObject>();

    public void Awake()
    {
        ObjectsToGround.Add(this);
    }

    public void OnDestroy()
    {
        ObjectsToGround.Remove(this);
    }
}
