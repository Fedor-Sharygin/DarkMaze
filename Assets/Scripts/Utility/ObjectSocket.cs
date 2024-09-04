using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSocket : MonoBehaviour
{
    public Transform Socket
    {
        get
        {
            return transform;
        }
    }
    public bool AvailableForStack
    {
        get
        {
            return transform.childCount <= 0;
        }
    }

    private bool m_Locked = false;
    public bool IsLocked
    {
        get
        {
            return m_Locked;
        }
    }

    public bool m_DestroyObjects;
    [SerializeField]
    private UnityEvent m_EventOnArrival;
    [SerializeField]
    private float m_ObjectSpeed = 5f;
    private void FixedUpdate()
    {
        if (AvailableForStack)
        {
            return;
        }

        for (int i = 0; i < transform.childCount; ++i)
        {
            if (!m_ObjectsState.TryGetValue(transform.GetChild(i), out bool Val) || Val == true)
            {
                continue;
            }
            if (Vector3.Magnitude(transform.GetChild(i).localPosition) <= .03162f)
            {
                m_EventOnArrival?.Invoke();
                if (m_DestroyObjects)
                {
                    m_ObjectsState.Remove(transform.GetChild(i));
                    Destroy(transform.GetChild(i).gameObject);
                }
                m_ObjectsState[transform.GetChild(i)] = true;
                continue;
            }
            transform.GetChild(i).localPosition = Vector3.Lerp(transform.GetChild(i).localPosition, Vector3.zero, m_ObjectSpeed * Time.fixedDeltaTime);
            if (m_DestroyObjects)
            {
                transform.GetChild(i).localScale = Vector3.Lerp(transform.GetChild(i).localScale, Vector3.zero, m_ObjectSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private Dictionary<Transform, bool> m_ObjectsState = new Dictionary<Transform, bool>();
    public void Stack(Transform p_StackObj)
    {
        if (!AvailableForStack || p_StackObj == null)
        {
            return;
        }

        p_StackObj.SetParent(Socket);
        m_ObjectsState.Add(p_StackObj, false);
        //p_StackObj.localPosition = Vector3.zero;
    }
    public void ForceStack(Transform p_StackObj)
    {
        if (p_StackObj == null)
        {
            return;
        }
        p_StackObj.SetParent(Socket);
        m_ObjectsState.Add(p_StackObj, false);
    }

    public Transform RemoveObj()
    {
        if (AvailableForStack)
        {
            return null;
        }

        var Ret = transform.GetChild(0);
        m_ObjectsState.Remove(Ret);
        transform.DetachChildren();
        return Ret;
    }
    public Transform[] RemoveObjs()
    {
        if (AvailableForStack)
        {
            return null;
        }

        Transform[] Ret = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            Ret[i] = transform.GetChild(i);
            m_ObjectsState.Remove(Ret[i]);
        }
        transform.DetachChildren();
        return Ret;
    }

    public Transform PeekObj()
    {
        if (AvailableForStack)
        {
            return null;
        }

        return transform.GetChild(0);
    }
    public Transform[] PeekObjs()
    {
        if (AvailableForStack)
        {
            return null;
        }

        Transform[] Ret = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            Ret[i] = transform.GetChild(i);
        }
        return Ret;
    }

    public void Lock(bool p_Locked = true)
    {
        m_Locked = p_Locked;
    }
    public void Unlock()
    {
        Lock(false);
    }
}
