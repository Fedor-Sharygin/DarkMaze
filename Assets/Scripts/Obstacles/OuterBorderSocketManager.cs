using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OuterBorderSocketManager : MonoBehaviour
{
    [SerializeField]
    private ObjectSocket m_CentralBorderSocket;
    [SerializeField]
    private ObjectSocket[] m_OuterBorderSockets;
    public void BringInBorders()
    {
        foreach (var OS in m_OuterBorderSockets)
        {
            m_CentralBorderSocket?.ForceStack(OS.RemoveObj());
        }
    }
    public void RemoveBorders()
    {
        int ChIdx = 0;
        while (m_CentralBorderSocket.transform.childCount > 0)
        {
            m_OuterBorderSockets[ChIdx]?.Stack(m_CentralBorderSocket.transform.GetChild(0));
            ChIdx++;
        }
    }


    private int m_FunctionTriggerCount = 0;
    [SerializeField]
    private UnityEvent m_BorderEntryEvent;
    public void OnBorderdEnterFunction()
    {
        m_FunctionTriggerCount++;
        if (m_FunctionTriggerCount < m_OuterBorderSockets.Length)
        {
            return;
        }
        m_BorderEntryEvent?.Invoke();
    }
}
