using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    int m_FinishedNum;
    public int FinishedNum { get { return m_FinishedNum; } }
    
    Collider m_Collider;

    // Start is called before the first frame update
    void Start()
    {
        m_FinishedNum = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        var box = other as BoxCollider;

        if (box != null) {
            var boxinfo = box.GetComponent<ItemCollision>();
            if (box.GetComponent<Rigidbody>().velocity.magnitude > boxinfo.damageThreshold)
            {
                boxinfo.DelayChangeGreen();
            }
            else {
                boxinfo.ChangeColorGreen();
            }
            m_FinishedNum++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var box = other as BoxCollider;

        if (box != null)
        {
            m_FinishedNum --;
            box.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
