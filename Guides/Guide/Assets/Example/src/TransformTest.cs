using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMotionType
{
    Translate,
    Rotate,
    RotateAround,
    LookAt
}

public enum EVecType
{
    Forward,
    Back,
    Left,
    Right,
    Up,
    Down,
    Zero
}



public class TransformTest : MonoBehaviour
{
    public bool m_isEnable = false;
    public float m_speed = 0.01f;
    public EMotionType m_motionType = EMotionType.Rotate;
    public EVecType m_vecType = EVecType.Forward;

    // Use this for initialization
    void Start()
    {
        Debug.Log("transform.childCount " + transform.childCount);

        if (transform.childCount > 0)
        {
            Debug.Log("transform.GetChild(0).name " + transform.GetChild(0).name);
        }

        Transform son = transform.Find("a");
        Transform grandson = transform.Find("a/b");

        if (son && grandson)
        {
            Debug.Log(son == grandson.parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isEnable)
        {
            switch (m_motionType)
            {
                case EMotionType.Translate:
                    transform.Translate(GetVectorByType() * m_speed * Time.deltaTime); break;
                case EMotionType.Rotate:
                    transform.Rotate(GetVectorByType(), m_speed * Time.deltaTime); break;
                case EMotionType.LookAt:
                    transform.LookAt(GetVectorByType()); break;
                case EMotionType.RotateAround:
                    if (transform.parent)
                    {
                        transform.RotateAround(transform.parent.position, GetVectorByType(), m_speed * Time.deltaTime);
                    }
                    break;
            }
        }
    }

    private Vector3 GetVectorByType()
    {
        Vector3 answer = Vector3.zero;
        switch (m_vecType)
        {
            case EVecType.Forward: answer = Vector3.forward; break;
            case EVecType.Back: answer = Vector3.back; break;
            case EVecType.Left: answer = Vector3.left; break;
            case EVecType.Right: answer = Vector3.right; break;
            case EVecType.Up: answer = Vector3.up; break;
            case EVecType.Down: answer = Vector3.down; break;
            case EVecType.Zero: answer = Vector3.zero; break;
        }
        return answer;
    }
}