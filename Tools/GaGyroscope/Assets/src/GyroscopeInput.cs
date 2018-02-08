using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

namespace GaGyroscope
{
    public class GyroscopeInput
    {
        public class OnScrollHandle : UnityEvent<Vector3> { }

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
                return m_enable && (IsSystemSupports || IgnoreSystem);
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

        public static bool IgnoreSystem
        {
            get
            {
                return m_ignoreSystem;
            }

            set
            {
                m_ignoreSystem = value;
            }
        }

        public static bool IgnoreSystem1
        {
            get
            {
                return m_ignoreSystem;
            }

            set
            {
                m_ignoreSystem = value;
            }
        }

        public static float Sensitive
        {
            get
            {
                return m_sensitive;
            }

            set
            {
                m_sensitive = value;
            }
        }

        public static float LerpIntensity
        {
            get
            {
                return m_lerpIntensity;
            }

            set
            {
                m_lerpIntensity = value;
            }
        }

        public static Vector3 IgnoreGrad
        {
            get
            {
                return m_ignoreGrad;
            }

            set
            {
                m_ignoreGrad = value;
            }
        }

        public static void SimulateInput(Vector3 delta)
        {
            OnScroll.Invoke(delta);
        }

        private static OnScrollHandle m_onScroll;
        private static bool m_enable = true;
        private static bool m_ignoreSystem = true;
        private static float m_sensitive = 1f;
        private static float m_lerpIntensity = 1f;
        private static Vector3 m_ignoreGrad = new Vector3(0.1f, 0.1f, 0.1f);

    }
}
