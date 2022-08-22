using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Enemy {
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour {
        public EnemyAnimator Animator;
        public float _effectiveDistance = 1f;
        private int _damage = 50;
        public float AttackCooldown = 1f;
        public float EffectiveDistance => _effectiveDistance;

        private AttackState _currentState;
        private IHealth _target;
        private Coroutine attackCoroutineLink;
        private bool attackInProgress = false;

        public IHealth Target {           
            set
            {    
                if(_target == null)
                {
                    _target = value;
                    _target.DeathEvent += OnTargetDeath;
                }                 
            }
        }

        private void Start() {
            attackCoroutineLink = StartCoroutine(AttackCoroutine());
            Mob mob = GetComponent<Mob>();
            mob.EnemyDeath.HappenedDeath += OnDeath;
        }

        public void OnDeath() {
            StopCoroutine(attackCoroutineLink);
            attackCoroutineLink = null;
        }

        private void OnTargetDeath(string id)
        {
            
           
            _target.DeathEvent -= OnTargetDeath;
            _currentState = AttackState.EndAttack;
            //_target = null;
            attackInProgress = false;
            //Animator.PlayIdle();
        }

        private IEnumerator AttackCoroutine() {
           
            WaitForSeconds waitForSeconds = new WaitForSeconds(AttackCooldown);
            
            while (true) {
                
                if (_currentState == AttackState.StartAttack && !attackInProgress)
                {
                    attackInProgress = true;
                    StartAttack();
                }

                yield return waitForSeconds;

            }
        }

        private void OnAttack() {
            _target.TakeDamage(_damage);
        }

        private void OnAttackEnded() {
            attackInProgress = false;
        }

        private void StartAttack() {
            Animator.PlayAttack();
        }

        public void SetState(AttackState state) {
            _currentState = state;
        }

        public void SetTarget(List<GameObject> _minics) {
        }
    }

    
}