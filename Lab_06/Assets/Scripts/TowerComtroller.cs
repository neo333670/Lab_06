using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerComtroller : MonoBehaviour
{
    GameObject m_Jib;
    GameObject m_Trolley;
    GameObject m_Hook;

    ConfigurableJoint m_HookcableJoint;
    Vector3 dis_vector;
    float dis;

    LineRenderer m_HookCableLine;
    public Vector3 fixHookZ;

    FlyingHook m_flyingHook;

    float trolley_y;
    float hook_z;
    
    const float spinSpeed = 20f;
    const float trolleySpeed = 3f;
    const float hooksSpeed = 5f;

    private void Start()
    {
        m_Jib = this.transform.GetChild(1).gameObject;
        m_Trolley = this.transform.GetChild(1).GetChild(0).gameObject;
        //m_Hook = this.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        m_Hook = this.transform.GetChild(1).GetChild(1).gameObject;

        m_HookcableJoint = m_Trolley.GetComponent<ConfigurableJoint>();

        dis_vector = m_HookcableJoint.anchor - m_HookcableJoint.connectedAnchor;
        dis = 0;

        m_HookCableLine = m_Trolley.GetComponent<LineRenderer>();
        m_HookCableLine.startWidth = 0.05f;
        m_HookCableLine.endWidth = 0.05f;

        m_flyingHook = m_Hook.GetComponent<FlyingHook>();
    }

    // Update is called once per frame
    void Update()
    {
        JibControll();
        TrolleyControll();
        HookControll();
        CreateCable();
    }

    void JibControll()
    {
        if (Input.GetKey(KeyCode.D))
        {
            m_Jib.transform.Rotate(Vector3.forward * Time.deltaTime * spinSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_Jib.transform.Rotate(Vector3.back * Time.deltaTime * spinSpeed);
        }
    }

    void TrolleyControll()
    {
        if (Input.GetKey(KeyCode.W))
        {
            trolley_y = -trolleySpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            trolley_y = trolleySpeed * Time.deltaTime;           
        }
        else { return; }
        m_Trolley.transform.localPosition = new Vector3(0, Mathf.Clamp(m_Trolley.transform.localPosition.y + trolley_y, -17.5f, 0f), 0f);
    }

    private void UpdateJointLimit()
    {
        dis += hook_z;
        
        var m_limit = m_HookcableJoint.linearLimit;
        m_limit.limit = dis;
        m_HookcableJoint.linearLimit = m_limit;
    }

    void HookControll()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (m_flyingHook.IsHookHited) {
                return;
            }

            hook_z = hooksSpeed * Time.deltaTime;
            UpdateJointLimit();           
            //m_Hook.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(m_Hook.transform.localPosition.z + hook_z, -15.75f, -0.25f));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            hook_z = -hooksSpeed * Time.deltaTime;
            UpdateJointLimit();            
            //m_Hook.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(m_Hook.transform.localPosition.z + hook_z, -15.75f, -0.25f));
        }
        
    }
    void CreateCable()
    {
        m_HookCableLine.SetPosition(0, m_Trolley.transform.position);
        m_HookCableLine.SetPosition(1, m_Hook.transform.position + fixHookZ);
    }
}



