using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private int _attackTrigger = Animator.StringToHash(ConstantData.AttackParametr);
    private Animator _animator;

    public event Action AttackHitted;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_attackTrigger);
    }

    private void OnAttackHit()
    {
        AttackHitted?.Invoke();
    }
}