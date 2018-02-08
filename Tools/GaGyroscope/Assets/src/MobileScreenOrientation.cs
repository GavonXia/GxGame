using System;
using UnityEngine;

[Serializable]
public class SetUp
{
    [Tooltip("敏感度")]
    public float sensitivity = 15f;   //敏感度

    [Tooltip("最大水平移动速度")]
    public float maxturnSpeed = 35f;    // 最大移动速度

    [Tooltip("最大垂直傾斜角移动速度")]
    public float maxTilt = 35f;    // 最大倾斜角

    [Tooltip("位移加成速率")]
    public float posRate = 1.5f;
}

public class MobileScreenOrientation : MonoBehaviour
{

    public enum EMotionAxial
    {
        All = 1,  //全部轴
        None = 2,
        x = 3,
        y = 4,
        z = 5
    }

    public enum EMotionMode
    {
        Postion = 1,   //只是位置辩护
        Rotation = 2,
        All = 3    //全部变化
    }

    //就是这里比较笨了。本来使用UnityEditor类库的多选功能。但是这个类库不支持移动平台。
    public EMotionAxial motionAxial1 = EMotionAxial.y;
    public EMotionAxial motionAxial2 = EMotionAxial.None;

    public EMotionMode motionMode = EMotionMode.Rotation;   //运动模式

    public SetUp setUp;

    public GameObject tager;     //被移动的对象

    Vector3 m_MobileOrientation;   //手机陀螺仪变化的值

    Vector3 m_tagerTransform;
    Vector3 m_tagerPos;
    public Vector3 ReversePosition = Vector3.one; //基于陀螺仪方向的取反

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        m_tagerTransform = Vector3.zero;
        m_tagerPos = Vector3.zero;

    }

    void LateUpdate()
    {
        if (tager == null)
            return;

        m_MobileOrientation = Input.acceleration;



        if (motionAxial1 == EMotionAxial.None && motionAxial2 == EMotionAxial.None)   //不操作任何轴
            return;
        else if (motionAxial1 == EMotionAxial.x && motionAxial2 == EMotionAxial.None)   // X轴
        {
            m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * setUp.maxTilt * ReversePosition.x, 0.2f);
        }
        else if (motionAxial1 == EMotionAxial.y && motionAxial2 == EMotionAxial.None)   //Y 轴
        {
            m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * setUp.maxturnSpeed * ReversePosition.y, 0.2f);
        }
        else if (motionAxial1 == EMotionAxial.z && motionAxial2 == EMotionAxial.None)   // z轴
        {
            m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_MobileOrientation.z * setUp.maxTilt * ReversePosition.z, 0.2f);
        }
        else if (motionAxial1 == EMotionAxial.x && motionAxial2 == EMotionAxial.y)   // X和Y轴
        {
            m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * setUp.maxturnSpeed * ReversePosition.y, 0.2f);
            m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * setUp.maxTilt * ReversePosition.x, 0.2f);
        }
        else if (motionAxial1 == EMotionAxial.y && motionAxial2 == EMotionAxial.x) // Y和X轴
        {
            m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * setUp.maxturnSpeed * ReversePosition.y, 0.2f);
            m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * setUp.maxTilt * ReversePosition.x, 0.2f);
        }
        else if (motionAxial1 == EMotionAxial.x && motionAxial2 == EMotionAxial.z)  // x 和 Z 轴
        {
            m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * setUp.maxTilt * ReversePosition.x, 0.2f);
            m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_MobileOrientation.z * setUp.maxTilt * ReversePosition.z, 0.2f);
        }
        else if (motionAxial1 == EMotionAxial.z && motionAxial2 == EMotionAxial.x)  // Z 和 X 轴
        {
            m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * setUp.maxTilt * ReversePosition.x, 0.2f);
            m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_MobileOrientation.z * setUp.maxTilt * ReversePosition.z, 0.2f);
        }
        else if (motionAxial1 == EMotionAxial.y && motionAxial2 == EMotionAxial.z)   // Y和Z 轴
        {
            m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * setUp.maxturnSpeed * ReversePosition.y, 0.2f);
            m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_MobileOrientation.z * setUp.maxTilt * ReversePosition.z, 0.2f);
        }
        else if (motionAxial1 == EMotionAxial.z && motionAxial2 == EMotionAxial.y)   // Z和 Y轴
        {
            m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * setUp.maxturnSpeed * ReversePosition.y, 0.2f);
            m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_MobileOrientation.z * setUp.maxTilt * ReversePosition.z, 0.2f);
        }
        else if (motionAxial1 == EMotionAxial.All && motionAxial2 == EMotionAxial.All)   // 所有轴向都运动
        {
            m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * setUp.maxturnSpeed * ReversePosition.y, 0.2f);
            m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * setUp.maxTilt * ReversePosition.x, 0.2f);
            m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, m_MobileOrientation.z * setUp.maxTilt * ReversePosition.z, 0.2f);
        }

        m_tagerPos.x = m_tagerTransform.y;
        m_tagerPos.y = -m_tagerTransform.x;
        m_tagerPos.z = m_tagerTransform.z;

        if (motionMode == EMotionMode.Postion)
        {
            tager.transform.localPosition = Vector3.Lerp(tager.transform.localPosition, m_tagerPos * setUp.posRate, Time.deltaTime * setUp.sensitivity);
        }
        else if (motionMode == EMotionMode.Rotation)
        {
            tager.transform.localRotation = Quaternion.Lerp(tager.transform.localRotation, Quaternion.Euler(m_tagerTransform), Time.deltaTime * setUp.sensitivity);
        }
        else
        {
            tager.transform.localPosition = Vector3.Lerp(tager.transform.localPosition, m_tagerPos * setUp.posRate, Time.deltaTime * setUp.sensitivity);
            tager.transform.localRotation = Quaternion.Lerp(tager.transform.localRotation, Quaternion.Euler(m_tagerTransform), Time.deltaTime * setUp.sensitivity);
        }
    }

}