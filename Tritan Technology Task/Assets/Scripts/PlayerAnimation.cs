using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int Walk = Animator.StringToHash("Walk");

    private void Start()
    {
        PlayerController.onStartWaking += OnStartWalking;
        UIManager.onClickSpeedBtn += UpdateAnimSpeed;
    }

    private void OnDestroy()
    {
        PlayerController.onStartWaking -= OnStartWalking;
        UIManager.onClickSpeedBtn -= UpdateAnimSpeed;
    }

    private void UpdateAnimSpeed(float value)
    {
        animator.speed += value / 4;
        animator.speed = Mathf.Clamp(animator.speed, 1, 2);
    }

    private void OnStartWalking(bool status)
    {
        animator.SetBool(Walk, status);
    }
}