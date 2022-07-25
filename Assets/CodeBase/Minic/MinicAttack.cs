using CodeBase.Minic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MinicAnimator))]
public class MinicAttack : MonoBehaviour
{
    public MinicAnimator Animator;
    private float _effectiveDistance = 1f;
    public float AttackCooldown = 1f;
    private int _damage = 10;

    private AttackState _currentState;
    private IHealth _target;
    private float _attackCooldown;
    private Coroutine attackCoroutineLink;

    public IHealth Target
    {
        set {
            if (_target != null) {
                _target.DeathEvent -= OnTargetDeath;
            }
            
            _target = value;
            if (_target != null) {
                _target.DeathEvent += OnTargetDeath;
            }
        }
    }

    private void OnTargetDeath() {
        _target.DeathEvent -= OnTargetDeath;
        _currentState = AttackState.EndAttack;
        _target = null;
        _attackInProgress = false;
        //Animator.PlayIdle();
    }

    public float EffectiveDistance => _effectiveDistance;

    private void Start()
    {
        attackCoroutineLink = StartCoroutine(AttackCoroutine());
    }

    public void OnDeath()
    {
        StopCoroutine(attackCoroutineLink);
        attackCoroutineLink = null;
    }

    private bool _attackInProgress;
    private IEnumerator AttackCoroutine()
    {

        WaitForSeconds waitForSeconds = new WaitForSeconds(AttackCooldown);

        while (true)
        {
            if (_currentState == AttackState.StartAttack && !_attackInProgress) {
                _attackInProgress = true;
                StartAttack();
            }

            yield return null;
        }
    }

    private void OnAttack()
    {
        _target.TakeDamage(_damage);
    }

    private void OnAttackEnded()
    {
        _attackInProgress = false;
    }

    private void StartAttack()
    {
        Animator.PlayAttack();
    }

    public void SetState(AttackState state)
    {
        _currentState = state;
    }

    
}

