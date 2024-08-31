using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleReaction : MonoBehaviour
{
    [System.Serializable]
    public struct ObstacleAction
    {
        public static ObstacleAction Empty
        {
            get
            {
                return new ObstacleAction();
            }
        }



        public string m_TagName;
        public UnityEvent m_EnterAction;
        public UnityEvent m_ExitAction;

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(m_TagName);
        }
    }
    [SerializeField]
    private ObstacleAction[] m_ObstacleActionList;

    private ObstacleAction GetObstacleActionByTag(string p_TagName)
    {
        if (string.IsNullOrEmpty(p_TagName))
        {
            return ObstacleAction.Empty;
        }

        foreach (var OA in m_ObstacleActionList)
        {
            if (OA.m_TagName != p_TagName)
            {
                continue;
            }

            return OA;
        }

        return ObstacleAction.Empty;
    }



    private void OnTriggerEnter2D(Collider2D p_Other)
    {
        var ExpectedReaction = GetObstacleActionByTag(p_Other.tag);
        if (ExpectedReaction.IsEmpty())
        {
            return;
        }

        ExpectedReaction.m_EnterAction?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D p_Other)
    {
        var ExpectedReaction = GetObstacleActionByTag(p_Other.tag);
        if (ExpectedReaction.IsEmpty())
        {
            return;
        }

        ExpectedReaction.m_ExitAction?.Invoke();
    }
}
