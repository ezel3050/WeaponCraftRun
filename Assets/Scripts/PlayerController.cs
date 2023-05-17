using System;
using System.Collections.Generic;
using DefaultNamespace.Components;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        [SerializeField] private Transform weaponSpot;
        [SerializeField] private Transform gloveSpot;

        public void SetDeltaPosition(float value)
        {
            movement.SetDeltaPosition(value);
        }

        public void CancelMovement()
        {
            movement.CancelMovement();
        }
    }
}