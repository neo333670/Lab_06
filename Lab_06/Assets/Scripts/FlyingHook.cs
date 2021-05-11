using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingHook : MonoBehaviour
{
    const float MOVE_SPEED = 2f;
    const float ATTACH_DISTANCE = 2f;
    Vector3 fixCabley;

    GameObject m_DetectedObject;
    [SerializeField] Joint m_JointForObject;
    [SerializeField] LineRenderer m_cable;

    bool isHookHited;
    public bool IsHookHited { get { return isHookHited; } }

    private void Start()
    {
        m_DetectedObject = null;
        m_JointForObject = null;

        fixCabley = new Vector3(0, -0.35f, 0);

        m_cable.startWidth = 0.1f;
        m_cable.endWidth = 0.1f;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(-MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0, 0, MOVE_SPEED * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0, 0, -MOVE_SPEED * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Translate(0, MOVE_SPEED * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            this.transform.Translate(0, -MOVE_SPEED * Time.deltaTime, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move();

        if (m_JointForObject == null) { 
            DetectObject();
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            AttachOrDetachObject();
        }
        UpdateCable();
    }

    void DetectObject()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, ATTACH_DISTANCE) && hit.collider.CompareTag("Item"))
        {
            if (m_DetectedObject == hit.collider.gameObject)//if hit last one
            {
                return;
            }
            RecoverDetectedObject();

            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();

            if (renderer != null)
            {
                renderer.material.color = Color.yellow;

                m_DetectedObject = hit.collider.gameObject;
            }
        }
        else {
            RecoverDetectedObject();
        }
    }

    void RecoverDetectedObject() {
        if (m_DetectedObject != null) {
            m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.white;
            m_DetectedObject = null;
        }
    }

    void AttachOrDetachObject() {
        if (m_JointForObject == null)
        {
            if (m_DetectedObject != null)
            {
                var joint = this.gameObject.AddComponent<ConfigurableJoint>();
                //joint.transform.GetComponent<Rigidbody>().useGravity = false;
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = ConfigurableJointMotion.Free;
                joint.angularYMotion = ConfigurableJointMotion.Free;
                joint.angularZMotion = ConfigurableJointMotion.Free;

                var m_limit = joint.linearLimit;
                m_limit.limit = ATTACH_DISTANCE;

                joint.linearLimit = m_limit;

                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = new Vector3(0f, 0.5f, 0f);
                joint.anchor = new Vector3(0f, 0f, -0.35f);

                joint.enableCollision = true;

                joint.connectedBody = m_DetectedObject.GetComponent<Rigidbody>();

                m_JointForObject = joint;

                m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.red;
                m_DetectedObject = null;
            }
        }
        else {
            m_JointForObject.connectedBody.GetComponent<MeshRenderer>().material.color = Color.white;
            GameObject.Destroy(m_JointForObject);
            m_JointForObject = null;
        }
    }

    void UpdateCable() {
        m_cable.enabled = m_JointForObject != null && m_JointForObject.connectedBody != null;

        if (m_cable.enabled) {
            if(m_JointForObject != null){
                m_cable.SetPosition(0, this.transform.position + fixCabley);

                var connectedBodyTransform = m_JointForObject.connectedBody.transform;
                m_cable.SetPosition(1, connectedBodyTransform.TransformPoint( m_JointForObject.connectedAnchor));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isHookHited = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isHookHited = false;
    }
}
