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
        set { _target = value; }
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

    private IEnumerator AttackCoroutine()
    {

        WaitForSeconds waitForSeconds = new WaitForSeconds(AttackCooldown);

        while (true)
        {
            yield return waitForSeconds;
            if (_currentState == AttackState.StartAttack)
            {
                StartAttack();
            }
        }
    }

    private void OnAttack()
    {
        _target.TakeDamage(_damage);
    }

    private void OnAttackEnded()
    {

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

