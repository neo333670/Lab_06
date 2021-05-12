using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTrigger : MonoBehaviour
{
    int m_FinishedNum;
    public int FinishedNum { get { return m_FinishedNum; } }

    void Start()
    {
        m_FinishedNum = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        var sphere = other as SphereCollider;

        if (sphere != null)
        {
            var sphereInfo = sphere.GetComponent<ItemCollision>();
            if (sphere.GetComponent<Rigidbody>().velocity.magnitude > sphereInfo.damageThreshold)
            {
                sphereInfo.DelayChangeBlue();
            }
            else
            {
                sphereInfo.ChangeColorBlue();
            }
            m_FinishedNum++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var sphere = other as SphereCollider;

        if (sphere != null)
        {
            m_FinishedNum--;
            sphere.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

}
