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

        void Update()
        {
            if (GyroscopeInput.UseEditor)
            {
              //  if (Input.GetKey(KeyCode.LeftControl))
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        if (GyroscopeInput.EditorInput.y < 1)
                        {
                            GyroscopeInput.EditorInput += (Vector3.up * Time.deltaTime);
                        }
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        if (GyroscopeInput.EditorInput.y > -1)
                        {
                            GyroscopeInput.EditorInput += (Vector3.down * Time.deltaTime);
                        }
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        if (GyroscopeInput.EditorInput.x > -1)
                        {
                            GyroscopeInput.EditorInput += (Vector3.left * Time.deltaTime);
                        }
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        if (GyroscopeInput.EditorInput.x < 1)
                        {
                            GyroscopeInput.EditorInput += (Vector3.right * Time.deltaTime);
                        }
                    }

                  //  GyroscopeInput.EditorInput = Vector3.Lerp(GyroscopeInput.EditorInput, Vector3.zero, 100f);
                }
            }
        }

        void LateUpdate()
        {
            if (!GyroscopeInput.Enable)
                return;

            bool shouldNext = GyroscopeInput.IsSystemSupports || GyroscopeInput.UseEditor;
            if (!shouldNext)
            {
                return;
            }

            if (GyroscopeInput.UseEditor)
            {
                m_mobileOrientation = GyroscopeInput.EditorInput;
            }
            else
            {
                m_mobileOrientation = Input.acceleration;
            }

            if (GyroscopeInput.MotionAxial1 == EMotionAxial.None && GyroscopeInput.MotionAxial2 == EMotionAxial.None)   //不操作任何轴
                return;
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.x && GyroscopeInput.MotionAxial2 == EMotionAxial.None)   // X轴
            {
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_mobileOrientation.y * GyroscopeInput.MaxTiltSpeed * m_reversePosition.x, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.y && GyroscopeInput.MotionAxial2 == EMotionAxial.None)   //Y 轴
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_mobileOrientation.x * GyroscopeInput.MaxMoveSpeed * m_reversePosition.y, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.z && GyroscopeInput.MotionAxial2 == EMotionAxial.None)   // z轴
            {
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_mobileOrientation.z * GyroscopeInput.MaxTiltSpeed * m_reversePosition.z, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.x && GyroscopeInput.MotionAxial2 == EMotionAxial.y)   // X和Y轴
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_mobileOrientation.x * GyroscopeInput.MaxMoveSpeed * m_reversePosition.y, 0.2f);
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_mobileOrientation.y * GyroscopeInput.MaxTiltSpeed * m_reversePosition.x, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.y && GyroscopeInput.MotionAxial2 == EMotionAxial.x) // Y和X轴
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_mobileOrientation.x * GyroscopeInput.MaxMoveSpeed * m_reversePosition.y, 0.2f);
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_mobileOrientation.y * GyroscopeInput.MaxTiltSpeed * m_reversePosition.x, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.x && GyroscopeInput.MotionAxial2 == EMotionAxial.z)  // x 和 Z 轴
            {
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_mobileOrientation.y * GyroscopeInput.MaxTiltSpeed * m_reversePosition.x, 0.2f);
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_mobileOrientation.z * GyroscopeInput.MaxTiltSpeed * m_reversePosition.z, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.z && GyroscopeInput.MotionAxial2 == EMotionAxial.x)  // Z 和 X 轴
            {
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_mobileOrientation.y * GyroscopeInput.MaxTiltSpeed * m_reversePosition.x, 0.2f);
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_mobileOrientation.z * GyroscopeInput.MaxTiltSpeed * m_reversePosition.z, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.y && GyroscopeInput.MotionAxial2 == EMotionAxial.z)   // Y和Z 轴
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_mobileOrientation.x * GyroscopeInput.MaxMoveSpeed * m_reversePosition.y, 0.2f);
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_mobileOrientation.z * GyroscopeInput.MaxTiltSpeed * m_reversePosition.z, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.z && GyroscopeInput.MotionAxial2 == EMotionAxial.y)   // Z和 Y轴
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_mobileOrientation.x * GyroscopeInput.MaxMoveSpeed * m_reversePosition.y, 0.2f);
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, -m_mobileOrientation.z * GyroscopeInput.MaxTiltSpeed * m_reversePosition.z, 0.2f);
            }
            else if (GyroscopeInput.MotionAxial1 == EMotionAxial.All && GyroscopeInput.MotionAxial2 == EMotionAxial.All)   // 所有轴向都运动
            {
                m_tagerTransform.y = Mathf.Lerp(m_tagerTransform.y, -m_mobileOrientation.x * GyroscopeInput.MaxMoveSpeed * m_reversePosition.y, 0.2f);
                m_tagerTransform.x = Mathf.Lerp(m_tagerTransform.x, m_mobileOrientation.y * GyroscopeInput.MaxTiltSpeed * m_reversePosition.x, 0.2f);
                m_tagerTransform.z = Mathf.Lerp(m_tagerTransform.z, m_mobileOrientation.z * GyroscopeInput.MaxTiltSpeed * m_reversePosition.z, 0.2f);
            }

            m_tagerPos.x = m_tagerTransform.y;
            m_tagerPos.y = -m_tagerTransform.x;
            m_tagerPos.z = m_tagerTransform.z;

            GyroscopeInput.OnScroll.Update(m_tagerPos * GyroscopeInput.PosRate, m_tagerTransform, Time.deltaTime * GyroscopeInput.Sensitivity);
        }

        private void OnGUI()
        {
            Rect rect = new Rect(0,0,200,50);
            GUI.Label(rect, m_mobileOrientation.ToString());
        }
    }
}