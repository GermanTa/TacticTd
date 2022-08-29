using UnityEngine;

namespace CodeBase.Minic
{
  public class UnitAnimator : MonoBehaviour
  {
    private static readonly int Attack = Animator.StringToHash("Attack_1");
        private static readonly int RangeAttack = Animator.StringToHash("RangeAttack");
        private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int AttackBool = Animator.StringToHash("Attack_1");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Animator _animator;

    private void Awake() =>
      _animator = GetComponent<Animator>();

    public void PlayHit() => _animator.SetTrigger(Hit);
    public void PlayDeath() => _animator.SetTrigger(Die);
    public void PlayAttack() => _animator.SetTrigger(Attack);
    public void PlayIdle() => _animator.Play(Idle);
    public void Move() =>_animator.SetBool(IsMoving, true);
    public void Staying() => _animator.SetBool(IsMoving, false);
   }
}
