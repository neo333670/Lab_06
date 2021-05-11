using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject m_cube;
    [SerializeField] GameObject m_shpere;
    GameObject m_CubeDestnation;
    Collider m_collider;

    public float x_range;
    public float z_range;

    // Start is called before the first frame update
    void Start()
    {
        x_range = 5;
        z_range = 10;

        m_CubeDestnation = GameObject.Find("Cube_Destination");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GeneratePrimitves(m_cube, 5);
            GeneratePrimitves(m_shpere, 5);
        }
    }

    void GeneratePrimitves(GameObject gameObject, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var item = Instantiate(gameObject);
            item.transform.position = new Vector3(Random.Range(-x_range, x_range)
                , 3f
                , Random.Range(-z_range, z_range)
                );
        }
    }

}
