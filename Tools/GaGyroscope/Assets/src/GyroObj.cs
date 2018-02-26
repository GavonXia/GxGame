using UnityEngine;

namespace GaGyroscope
{
    public class GyroObj : MonoBehaviour
    {
        public bool m_enable = true;

        public float m_speed = 1f;

        public float m_rateX = 1f;

        public float m_rateY = 0.5f;

        public float m_rateZ = 1f;

        public bool m_enablePosition = true;

        public bool m_enableScall = true;

        private Vector3 m_gyroVec = Vector3.zero;
        private Quaternion m_gyroQua = Quaternion.identity;
        public void Awake()
        {
            GyroscopeInput.OnScroll.AddListener(this);
            m_gyroVec = transform.localPosition;
        }

        public void Destroy()
        {
            GyroscopeInput.OnScroll.RemoveListener(this);
        }

        public void UpdateGyro(Vector3 position, Vector3 rotationVec, float deltaTime)
        {
            if (m_enable && gameObject.activeSelf)
            {
                if (m_enablePosition)
                {
                    //      position = new Vector3(position.x * m_rateX, position.y * m_rateY, position.z * m_rateZ);
                    position = position * m_speed;

                    //       rotationVec = new Vector3(rotationVec.x * m_rateX, rotationVec.y * m_rateY, rotationVec.z * m_rateZ);

                    transform.localPosition = Vector3.Lerp(transform.localPosition, m_gyroVec + position, deltaTime);
                }
                if (m_enableScall)
                {
                    Quaternion rotation = Quaternion.Euler(rotationVec);
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, deltaTime);
                }
            }
        }

        public void Update()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, m_gyroVec, Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
        }
    }
}