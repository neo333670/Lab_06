using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    MeshRenderer m_MeshRender;
    Rigidbody m_Rigidbody;

    public float damageThreshold;

    // Start is called before the first frame update
    void Start()
    {
        m_MeshRender = this.GetComponent<MeshRenderer>();
        m_Rigidbody = this.GetComponent<Rigidbody>();

        damageThreshold = 1.5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_Rigidbody.velocity.magnitude > damageThreshold)
        {
            BeDamagedChangeColor();
        }
    }

    private void BeDamagedChangeColor()
    {
        ChangeRed();
        Invoke("ChangeColorBack", 0.2f);
        Invoke("ChangeRed", 0.4f);
        Invoke("ChangeColorBack", 0.6f);
        Invoke("ChangeRed", 0.8f);
        Invoke("ChangeColorBack", 1f);
    }

    private void ChangeColorBack()
    {
        m_MeshRender.material.color = Color.white;
    }
    private void ChangeRed()
    {
        m_MeshRender.material.color = Color.red;
    }

    public void ChangeColorGreen()
    {
        m_MeshRender.material.color = Color.green;
    }
    public void ChangeColorBlue()
    {
        m_MeshRender.material.color = Color.blue;
    }

    public void DelayChangeGreen() {
        Invoke("ChangeColorGreen", 1.2f);
    }
    public void DelayChangeBlue()
    {
        Invoke("ChangeColorBlue", 1f);
    }

}
