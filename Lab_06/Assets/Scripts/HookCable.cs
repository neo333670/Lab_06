using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCable : MonoBehaviour
{
    LineRenderer m_CableConnectedHook;

    // Start is called before the first frame update
    void Start()
    {
        m_CableConnectedHook = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_CableConnectedHook.SetPosition(0, this.transform.position);
    }
}
