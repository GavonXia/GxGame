using System;
using UnityEngine;

namespace GaGyroscope
{
    public class MobileScreenOrientation : MonoBehaviour
    {
        private Vector3 m_mobileOrientation;   //手机陀螺仪变化的值
        private Vector3 m_tagerTransform;
        private Vector3 m_tagerPos;
        private Vector3 m_reversePosition = Vector3.one; //基于陀螺仪方向的取反

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

            m_mobileOrientation = Input.acceleration;

            m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_mobileOrientation.x * GyroscopeInput.MaxMoveSpeed * m_reversePosition.y, 0.2f);
            m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_mobileOrientation.y * GyroscopeInput.MaxTiltSpeed * m_reversePosition.x, 0.2f);
            m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, m_mobileOrientation.z * GyroscopeInput.MaxTiltSpeed * m_reversePosition.z, 0.2f);
            
            m_tagerPos.x = m_tagerTransform.y;
            m_tagerPos.y = -m_tagerTransform.x;
            m_tagerPos.z = m_tagerTransform.z;

            GyroscopeInput.OnScroll.Invoke(m_tagerPos * GyroscopeInput.PosRate, Quaternion.Euler(m_tagerTransform), Time.deltaTime * GyroscopeInput.Sensitivity);
        }
    }
}