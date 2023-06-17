using DefaultNamespace;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance => s_Instance;
        static InputManager s_Instance;

        [SerializeField]
        float m_InputSensitivity = 1.5f;

        private float m_tempVal;

        bool m_HasInput;
        Vector3 m_InputPosition;
        Vector3 m_PreviousInputPosition;
        private Player _player;

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
        }

        void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow) 
                || Input.GetKey(KeyCode.LeftArrow) 
                || Input.GetKey(KeyCode.D) 
                || Input.GetKey(KeyCode.A))
            {
                var x = Input.GetAxis("Horizontal");
                m_tempVal += Time.deltaTime * (x >= 0 ? 1 : -1);

                m_InputPosition.x = m_tempVal * Screen.width;
                if (!m_HasInput)
                {
                    m_PreviousInputPosition = m_InputPosition;
                }
                
                m_HasInput = true;
            }
            else
            {
                if (Mouse.current.leftButton.isPressed || Touch.activeTouches.Count > 0)
                {
                    if (Touch.activeTouches.Count > 0)
                    {
                        m_InputPosition = Touch.activeTouches[0].screenPosition;
            
                        if (!m_HasInput)
                        {
                            m_PreviousInputPosition = m_InputPosition;
                        }
                
                        m_HasInput = true;
                    }

                    if (Mouse.current.leftButton.isPressed)
                    {
                        m_InputPosition = Mouse.current.position.ReadValue();

                        if (!m_HasInput)
                        {
                            m_PreviousInputPosition = m_InputPosition;
                        }
                        m_HasInput = true;
                    }
                }
                else
                {
                    m_HasInput = false;
                    m_tempVal = 0;
                }
            }

            if (_player)
            {
                if (m_HasInput)
                {
                    float normalizedDeltaPosition = (m_InputPosition.x - m_PreviousInputPosition.x) / Screen.width * m_InputSensitivity;
                    _player.SetDeltaPosition(normalizedDeltaPosition);
                }
                else
                {
                    _player.CancelMovement();
                }
            }

            m_PreviousInputPosition = m_InputPosition;
        }

        public void SetPlayer(Player playerController)
        {
            _player = playerController;
        }
    }
}
