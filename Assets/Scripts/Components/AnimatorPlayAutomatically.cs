using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayAutomatically : MonoBehaviour
{
    [SerializeField] private string stateName;

    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play(stateName);
    }

    private void OnEnable() => animator.Play(stateName);
}
