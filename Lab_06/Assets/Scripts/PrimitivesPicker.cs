using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitivesPicker : MonoBehaviour
{
    GameObject m_clickedObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            FireRaycast();
        }
        if (Input.GetMouseButtonUp(0)) {
            RecoverClickedItem();
        }
    }

    void FireRaycast() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();

            if (renderer != null) {
                renderer.material.color = Color.red;
                m_clickedObject = hit.collider.gameObject;
            }
        }
    }

    void RecoverClickedItem() {
        if (m_clickedObject != null) {
            m_clickedObject.GetComponent<MeshRenderer>().material.color = Color.white;
            m_clickedObject = null;
        }
    }
}
