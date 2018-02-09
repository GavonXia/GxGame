using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace GaGyroscope
{
    public class GyroscopeInput
    {
        public class OnScrollHandle
        {
            // 一定要解开注销
            private List<Transform> m_handleList = new List<Transform>();

            public void AddListener(Transform target)
            {
                if (target && !m_handleList.Contains(target))
                {
                    m_handleList.Add(target);
                }
            }

            public void RemoveListener(Transform target)
            {
                if (target && m_handleList.Contains(target))
                {
                    m_handleList.Remove(target);
                }
            }

            public void Invoke(Vector3 position, Quaternion rotation, float deltaTime)
            {
                for (int i = 0; i < m_handleList.Count; i++)
                {
                    Transform tran = m_handleList[i];
                    if (tran)
                    {
                        InvokeSingle(tran, position, rotation, deltaTime);
                    }
                    else
                    {
                        m_handleList.Remove(tran);
                    }
                }
            }

            private void InvokeSingle(Transform tager, Vector3 position, Quaternion rotation, float deltaTime)
            {
                if (MotionMode == EMotionMode.Postion)
                {
                    tager.transform.localPosition = Vector3.Lerp(tager.transform.localPosition, position, deltaTime);
                }
                else if (MotionMode == EMotionMode.Rotation)
                {
                    tager.transform.localRotation = Quaternion.Lerp(tager.transform.localRotation, rotation, deltaTime);
                }
                else
                {
                    tager.transform.localPosition = Vector3.Lerp(tager.transform.localPosition, position, deltaTime);
                    tager.transform.localRotation = Quaternion.Lerp(tager.transform.localRotation, rotation, deltaTime);
                }
            }
        }

        public static OnScrollHandle OnScroll
        {
            get
            {
                if (m_onScroll == null)
                {
                    m_onScroll = new OnScrollHandle();
                }
                return m_onScroll;
            }
        }

        public static bool Enable
        {
            get
            {
                return m_enable && (IsSystemSupports || UseEditor);
            }

            set
            {
                m_enable = value;
            }
        }

        public static bool IsSystemSupports
        {
            get
            {
                return SystemInfo.supportsGyroscope;
            }
        }

        public static bool UseEditor
        {
            get
            {
                return m_useEditor;
            }

            set
            {
                m_useEditor = value;
            }
        }

        public static float Sensitivity
        {
            get
            {
                return m_sensitivity;
            }

            set
            {
                m_sensitivity = value;
            }
        }

        public static float MaxMoveSpeed
        {
            get
            {
                return maxMoveSpeed;
            }

            set
            {
                maxMoveSpeed = value;
            }
        }

        public static float MaxTiltSpeed
        {
            get
            {
                return maxTiltSpeed;
            }

            set
            {
                maxTiltSpeed = value;
            }
        }

        public static float PosRate
        {
            get
            {
                return posRate;
            }

            set
            {
                posRate = value;
            }
        }

        public static EMotionAxial MotionAxial1
        {
            get
            {
                return m_motionAxial1;
            }

            set
            {
                m_motionAxial1 = value;
            }
        }

        public static EMotionAxial MotionAxial2
        {
            get
            {
                return m_motionAxial2;
            }

            set
            {
                m_motionAxial2 = value;
            }
        }

        public static EMotionMode MotionMode
        {
            get
            {
                return m_motionMode;
            }

            set
            {
                m_motionMode = value;
            }
        }

        public static void OnScrollInput(Vector3 delta)
        {
            //if (Enable)
            //{
            //    OnScroll.Invoke(delta);
            //}
        }

        private static OnScrollHandle m_onScroll;
        private static OnScrollHandle m_onScrollRotation;
        private static bool m_enable = true;
        [Tooltip("敏感度")]
        private static bool m_useEditor = true;
        [Tooltip("敏感度")]
        private static float m_sensitivity = 15f;
        [Tooltip("最大水平移动速度")]
        private static float maxMoveSpeed = 35f;
        [Tooltip("最大垂直傾斜角移动速度")]
        private static float maxTiltSpeed = 35f;
        [Tooltip("位移加成速率")]
        private static float posRate = 1.5f;

        private static EMotionAxial m_motionAxial1 = EMotionAxial.y;
        private static EMotionAxial m_motionAxial2 = EMotionAxial.None;
        private static EMotionMode m_motionMode = EMotionMode.Rotation;   //运动模式

    }

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
}
