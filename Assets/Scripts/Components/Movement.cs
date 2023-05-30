using System;
using UnityEngine;

namespace DefaultNamespace.Components
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] PlayerSpeedPreset m_PlayerSpeed = PlayerSpeedPreset.Medium;
        
        [SerializeField] float m_CustomPlayerSpeed = 10.0f;
        
        [SerializeField] float m_AccelerationSpeed = 10.0f;
        
        [SerializeField] float m_DecelerationSpeed = 20.0f;
        
        [SerializeField] float m_HorizontalSpeedFactor = 0.5f;

        [SerializeField] bool m_AutoMoveForward;

        enum PlayerSpeedPreset
        {
            Slow,
            Medium,
            Fast,
            Custom
        }
        
        Transform m_Transform;
        bool m_HasInput;
        bool m_ManualInputHandler;
        private bool m_FullStop = true;
        float m_MaxXPosition;
        float m_MinXPosition;
        float m_XPos;
        float m_ZPos;
        float m_TargetPosition;
        float m_Speed;
        float m_TargetSpeed;
        private string m_LastAnimatorState = "Idle";
        
        
        public Transform Transform => m_Transform;
        
        public float Speed => m_Speed;
        
        public float TargetSpeed => m_TargetSpeed;
        
        public float TargetPosition => m_TargetPosition;
        
        public float MaxXPosition => m_MaxXPosition;
        
        public Action onPlayerDied;
        public Action onPlayerKicked;
        
        void Start()
        {
            Initialize();
        }
        
        public void Initialize()
        {
            m_Transform = transform;
            ResetSpeed();
        }
        
        public float GetDefaultSpeed()
        {
            switch (m_PlayerSpeed)
            {
                case PlayerSpeedPreset.Slow:
                    return 5.0f;
        
                case PlayerSpeedPreset.Medium:
                    return 10.0f;
        
                case PlayerSpeedPreset.Fast:
                    return 20.0f;
            }
        
            return m_CustomPlayerSpeed;
        }
        public void ResetSpeed()
        {
            m_Speed = 0.0f;
            m_TargetSpeed = GetDefaultSpeed();
        }
        
        public void SetDeltaPosition(float normalizedDeltaPosition)
        {
            if (m_ManualInputHandler)
            {
                CancelMovement();
                return;
            }
            
            m_MaxXPosition = 3.2f;
            m_MinXPosition = -4f;
        
            float fullWidth = m_MaxXPosition * 2.0f;
            m_TargetPosition += fullWidth * normalizedDeltaPosition;
            m_TargetPosition = Mathf.Clamp(m_TargetPosition, m_MinXPosition, m_MaxXPosition);
            m_HasInput = true;
        }
        
        public void CancelMovement()
        {
            m_HasInput = false;
        }
        void Update()
        {
            if (m_FullStop) return;
            float deltaTime = Time.deltaTime;

            if (!m_AutoMoveForward && !m_HasInput)
            {
                Decelerate(deltaTime, 0.0f);
            }
            else if (m_TargetSpeed < m_Speed)
            {
                Decelerate(deltaTime, m_TargetSpeed);
            }
            else if (m_TargetSpeed > m_Speed)
            {
                Accelerate(deltaTime, m_TargetSpeed);
            }
        
            float speed = m_Speed * deltaTime;

            m_ZPos += speed;
        
            if (m_HasInput)
            {
                float horizontalSpeed = speed * m_HorizontalSpeedFactor;
        
                float newPositionTarget = Mathf.Lerp(m_XPos, m_TargetPosition, horizontalSpeed);
                float newPositionDifference = newPositionTarget - m_XPos;
        
                newPositionDifference = Mathf.Clamp(newPositionDifference, -horizontalSpeed, horizontalSpeed);
        
                m_XPos += newPositionDifference;
            }
        
            if (m_AutoMoveForward)
            {
                m_Transform.position = new Vector3(m_XPos, m_Transform.position.y, m_ZPos);
            }
        }

        void Accelerate(float deltaTime, float targetSpeed)
        {
            m_Speed += deltaTime * m_AccelerationSpeed;
            m_Speed = Mathf.Min(m_Speed, targetSpeed);
        }
        
        void Decelerate(float deltaTime, float targetSpeed)
        {
            m_Speed -= deltaTime * m_DecelerationSpeed;
            m_Speed = Mathf.Max(m_Speed, targetSpeed);
        }

        public void SetCustomZPos(float value)
        {
            m_ZPos = value;
        }

        public void FullStop(bool isActive)
        {
            m_FullStop = isActive;
        }

        public void SyncZPos()
        {
            m_ZPos = transform.position.z;
        }
    }
}