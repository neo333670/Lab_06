using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        var box = other as BoxCollider;

        if (box != null) {
            box.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var box = other as BoxCollider;
        
        if (box != null && box.GetComponent<MeshRenderer>().material.color != Color.green)
        {
            box.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var box = other as BoxCollider;

        if (box != null)
        {
            box.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
