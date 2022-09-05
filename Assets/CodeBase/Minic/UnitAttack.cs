using CodeBase.Minic;
using System.Collections;
using CodeBase.Services.SpawnerService;
using UnityEngine;


[RequireComponent(typeof(UnitAnimator))]
public class UnitAttack : UnitComponent
{
    public UnitAnimator _animator;
    private SpawnerService _spawnerService;
    
    public float _effectiveDistance = 1f;
    public float AttackCooldown = 1f;
    private int _damage = 10;

    private AttackState _currentState;
    private UnitHealth _targetUnitHealth;
    private float _attackCooldown;
    private Coroutine attackCoroutineLink;
    private bool _attackInProgress;

    public void InjectDependencies(UnitBase unitBase, UnitAnimator animator, SpawnerService spawnerService) {
        InjectDependencies(unitBase);
        _animator = animator;
        _spawnerService = spawnerService;
    }
    
    public float EffectiveDistance => _effectiveDistance;

    public UnitHealth Target
    {
        get { return _targetUnitHealth; }
        set {
            if (_targetUnitHealth != null) {
                _targetUnitHealth.DeathEvent -= OnTargetUnitHealthDeath;
            }
            
            _targetUnitHealth = value;
            if (_targetUnitHealth != null) {
                _targetUnitHealth.DeathEvent += OnTargetUnitHealthDeath;
            }
        }
    }

    private void OnTargetUnitHealthDeath(string id) {
        _targetUnitHealth.DeathEvent -= OnTargetUnitHealthDeath;
        _currentState = AttackState.EndAttack;
        //_target = null;
        _attackInProgress = false;
        //Animator.PlayIdle();
    }

    private void Start()
    {
      
        attackCoroutineLink = StartCoroutine(AttackCoroutine());
        
    }

    public override void Dispose()
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
        _targetUnitHealth.TakeDamage(_damage);
    }

    private void OnAttackEnded()
    {
        _attackInProgress = false;
    }

    private void StartAttack()
    {
       
        _animator.PlayAttack();
    }

    public void SetState(AttackState state)
    {
       
        _currentState = state;
    }
}
