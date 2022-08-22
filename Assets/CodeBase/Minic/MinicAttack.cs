using CodeBase.Minic;
using System.Collections;
using CodeBase.Services.SpawnerService;
using UnityEngine;

[RequireComponent(typeof(MinicAnimator))]
public class MinicAttack : MonoBehaviour
{
    public MinicAnimator Animator;
    public float _effectiveDistance = 1f;
    public float AttackCooldown = 1f;
    private int _damage = 10;

    private AttackState _currentState;
    private Mob _target;
    private float _attackCooldown;
    private Coroutine attackCoroutineLink;
    private bool _attackInProgress;
    private SpawnerService _spawnerService;

    public float EffectiveDistance => _effectiveDistance;

    public Mob Target
    {
        set {
            if (_target != null) {
                _target.MobHealth.DeathEvent -= OnTargetDeath;
            }
            
            _target = value;
            if (_target != null) {
                _target.MobHealth.DeathEvent += OnTargetDeath;
            }
        }
    }

    private void OnTargetDeath(string id) {
        _target.MobHealth.DeathEvent -= OnTargetDeath;
        _currentState = AttackState.EndAttack;
        //_target = null;
        _attackInProgress = false;
        //Animator.PlayIdle();
    }

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
            if (_currentState == AttackState.StartAttack && !_attackInProgress) {
                _attackInProgress = true;
                StartAttack();
            }

            yield return waitForSeconds;
        }
    }

    protected virtual void OnAttack()
    {
        _target.MobHealth.TakeDamage(_damage);
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

    public void Construct(SpawnerService spawnerService) {
        _spawnerService = spawnerService;
    }
}