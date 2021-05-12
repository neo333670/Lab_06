using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text m_NumCubeText;
    Text m_NumSphereText;

    CubeTrigger m_CubeTrigger;
    SphereTrigger m_SphereTrigger;

    int m_finishedBoxNum;
    int m_TotalCubeNum;
    int m_finishedSphereNum;
    int m_TotalSphereNum;

    // Start is called before the first frame update
    void Start()
    {
        m_NumCubeText = GameObject.Find("Txt-CubeNum").GetComponent<Text>();
        m_NumSphereText = GameObject.Find("Txt-SphereNum").GetComponent<Text>();

        m_CubeTrigger = GameObject.Find("Cube_Destination").transform.GetChild(0).GetComponent<CubeTrigger>();
        m_SphereTrigger = GameObject.Find("Sphere_Destination").transform.GetChild(0).GetComponent<SphereTrigger>();

        m_TotalCubeNum = GameObject.FindObjectsOfType<BoxItem>().Length;
        m_TotalSphereNum = GameObject.FindObjectsOfType<SphereItem>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        m_finishedBoxNum = m_CubeTrigger.FinishedNum;
        m_finishedSphereNum = m_SphereTrigger.FinishedNum;

        UpdateText(m_NumCubeText, m_finishedBoxNum, m_TotalCubeNum);
        UpdateText(m_NumSphereText, m_finishedSphereNum, m_TotalSphereNum);
    }

    void UpdateText(Text textInfo, int finished, int total) {
        textInfo.text = string.Format("  {0} / {1}", finished, total);
    }
}
