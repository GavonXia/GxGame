using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTest : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_tagObjs;
    [SerializeField]
    Camera m_camera;

    // Use this for initialization
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("UI");
        gameObject.tag = "Player";
        m_camera = FindObjectOfType<Camera>();
        m_tagObjs = GameObject.FindGameObjectsWithTag("Player");


        GameObject cubeTemplate = GameObject.Find("Cube");
        GameObject cubeParentNew = Instantiate(cubeTemplate);

        cubeParentNew.name = "new cube parent";
        cubeParentNew.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);


        GameObject cubeChildNew = Instantiate(cubeTemplate, Vector3.zero, Quaternion.identity);
        cubeChildNew.name = "new cube child";
        cubeChildNew.transform.SetParent(cubeParentNew.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
