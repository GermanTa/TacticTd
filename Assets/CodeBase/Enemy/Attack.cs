using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Minic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator Animator;
        private float _effectiveDistance = 0.5f;
        public float AttackCooldown = 0.5f;
        private int _damage = 10;

        private AttackState _currentState;
        private IHealth _target;
        private bool _isAttacking;
        private float _attackCooldown;

        public IHealth Target { set { _target = value; } }
        public float EffectiveDistance => _effectiveDistance;

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
            {
                StartAttack();
            }
        }

        private void OnAttack()
        {
            _target.TakeDamage(_damage);
        }
        private void OnAttackEnded()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        public bool CanAttack()
        {
            return _currentState == AttackState.StartAttack && !_isAttacking && CooldownIsUp();
        }

        private void StartAttack()
        {
            Animator.PlayAttack();
            _isAttacking = true;
        }

        private bool CooldownIsUp() => _attackCooldown <= 0;
        private void UpdateCooldown()
        {
            if (!CooldownIsUp()) _attackCooldown -= Time.deltaTime;
        }

        public void SetState(AttackState state)
        {
            _currentState = state;
        }

        public void SetTarget(List<GameObject> _minics)
        {

        }
    }

    public enum AttackState
    {
        EndAttack,
        StartAttack,
        
    }
}
