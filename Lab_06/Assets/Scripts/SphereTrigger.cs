using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        var box = other as SphereCollider;

        if (box != null)
        {
            box.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var box = other as SphereCollider;

        if (box != null && box.GetComponent<MeshRenderer>().material.color != Color.blue)
        {
            box.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var box = other as SphereCollider;

        if (box != null)
        {
            box.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }


}
