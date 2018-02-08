using UnityEngine;
using System.Collections;

namespace GaGyroscope
{
    internal class SimpleGyroscope : MonoBehaviour
    {
        private bool m_isSystemSupports;
        private Gyroscope m_target;
        void Start()
        {
            m_isSystemSupports = GyroscopeInput.IsSystemSupports;
            m_target = Input.gyro;
            m_target.enabled = true;
        }
        void Update()
        {
            if (m_isSystemSupports)
            {
                Vector3 a = m_target.attitude.eulerAngles;
                a = new Vector3(-a.x, -a.y, a.z); //直接使用读取的欧拉角发现不对，于是自己调整一下符号
                //this.transform.eulerAngles = a;
                //this.transform.Rotate(Vector3.right * 90, Space.World);
                //draw = false;
            }
        }

        void OnGUI()
        {

        }
    }
}
