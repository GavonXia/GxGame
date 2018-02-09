using System;
using UnityEngine;

namespace GaGyroscope
{
    public class MobileScreenOrientation : MonoBehaviour
    {
        private Vector3 m_MobileOrientation;   //手机陀螺仪变化的值
        private Vector3 m_tagerTransform;
        private Vector3 m_tagerPos;
        public Vector3 ReversePosition = Vector3.one; //基于陀螺仪方向的取反

        void Awake()
        {
            Screen.orientation = ScreenOrientation.Landscape;
            m_tagerTransform = Vector3.zero;
            m_tagerPos = Vector3.zero;
        }

        void LateUpdate()
        {
            if (!GyroscopeInput.Enable || GyroscopeInput.IsSystemSupports)
                return;

            m_MobileOrientation = Input.acceleration;

            if (GyroscopeInput.MotionAxial1 == EMotionAxial.None && GyroscopeInput.MotionAxial2 == EMotionAxial.None)   //不操作任何轴
                return;
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.x && GyroscopeInput.MotionAxial2 == EMotionAxial.None)   // X轴
            {
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * GyroscopeInput.MaxTiltSpeed * ReversePosition.x, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.y && GyroscopeInput.MotionAxial2 == EMotionAxial.None)   //Y 轴
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * GyroscopeInput.MaxMoveSpeed * ReversePosition.y, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.z && GyroscopeInput.MotionAxial2 == EMotionAxial.None)   // z轴
            {
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_MobileOrientation.z * GyroscopeInput.MaxTiltSpeed * ReversePosition.z, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.x && GyroscopeInput.MotionAxial2 == EMotionAxial.y)   // X和Y轴
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * GyroscopeInput.MaxMoveSpeed * ReversePosition.y, 0.2f);
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * GyroscopeInput.MaxTiltSpeed * ReversePosition.x, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.y && GyroscopeInput.MotionAxial2 == EMotionAxial.x) // Y和X轴
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * GyroscopeInput.MaxMoveSpeed * ReversePosition.y, 0.2f);
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * GyroscopeInput.MaxTiltSpeed * ReversePosition.x, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.x && GyroscopeInput.MotionAxial2 == EMotionAxial.z)  // x 和 Z 轴
            {
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * GyroscopeInput.MaxTiltSpeed * ReversePosition.x, 0.2f);
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_MobileOrientation.z * GyroscopeInput.MaxTiltSpeed * ReversePosition.z, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.z && GyroscopeInput.MotionAxial2 == EMotionAxial.x)  // Z 和 X 轴
            {
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * GyroscopeInput.MaxTiltSpeed * ReversePosition.x, 0.2f);
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_MobileOrientation.z * GyroscopeInput.MaxTiltSpeed * ReversePosition.z, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.y && GyroscopeInput.MotionAxial2 == EMotionAxial.z)   // Y和Z 轴
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * GyroscopeInput.MaxMoveSpeed * ReversePosition.y, 0.2f);
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_MobileOrientation.z * GyroscopeInput.MaxTiltSpeed * ReversePosition.z, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.z && GyroscopeInput.MotionAxial2 == EMotionAxial.y)   // Z和 Y轴
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * GyroscopeInput.MaxMoveSpeed * ReversePosition.y, 0.2f);
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_MobileOrientation.z * GyroscopeInput.MaxTiltSpeed * ReversePosition.z, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.All && GyroscopeInput.MotionAxial2 == EMotionAxial.All)   // 所有轴向都运动
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_MobileOrientation.x * GyroscopeInput.MaxMoveSpeed * ReversePosition.y, 0.2f);
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_MobileOrientation.y * GyroscopeInput.MaxTiltSpeed * ReversePosition.x, 0.2f);
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, m_MobileOrientation.z * GyroscopeInput.MaxTiltSpeed * ReversePosition.z, 0.2f);
            }

            m_tagerPos.x = m_tagerTransform.y;
            m_tagerPos.y = -m_tagerTransform.x;
            m_tagerPos.z = m_tagerTransform.z;

            GyroscopeInput.OnScroll.Invoke(m_tagerPos * GyroscopeInput.PosRate, Quaternion.Euler(m_tagerTransform), Time.deltaTime * GyroscopeInput.Sensitivity);

            //    if (motionMode == EMotionMode.Postion)
            //{
            //    GyroscopeInput.OnScroll.Invoke(m_tagerPos * GyroscopeInput.PosRate, Quaternion.Euler(m_tagerTransform), Time.deltaTime * GyroscopeInput.Sensitivity);

            //    tager.transform.localPosition = Vector3.Lerp(tager.transform.localPosition, m_tagerPos * GyroscopeInput.PosRate, Time.deltaTime * GyroscopeInput.Sensitivity);
            //}
            //else if (motionMode == EMotionMode.Rotation)
            //{
            //    tager.transform.localRotation = Quaternion.Lerp(tager.transform.localRotation, Quaternion.Euler(m_tagerTransform), Time.deltaTime * GyroscopeInput.Sensitivity);
            //}
            //else
            //{
            //    tager.transform.localPosition = Vector3.Lerp(tager.transform.localPosition, m_tagerPos * GyroscopeInput.PosRate, Time.deltaTime * GyroscopeInput.Sensitivity);
            //    tager.transform.localRotation = Quaternion.Lerp(tager.transform.localRotation, Quaternion.Euler(m_tagerTransform), Time.deltaTime * GyroscopeInput.Sensitivity);
            //}
        }
    }
}