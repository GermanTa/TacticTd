using System;
using UnityEngine;

namespace CodeBase.Minic {
    [RequireComponent(typeof(UnitAnimator))]
    public class UnitHealth : UnitComponent {
        public event Action HealthChanged;
        public event Action<string> DeathEvent;
        
        public UnitAnimator _animator;

        private int _health = 50;
        private int _maxHp = 50;

        public int MaxHp { get; set; }

        public int CurrentHp {
            get { return _health; }
            set {
                _health = value;
                HealthChanged?.Invoke();
            }
        }

        public void InjectDependencies(UnitBase unitComponents, UnitAnimator unitAnimator) {
            InjectDependencies(unitComponents);
            _animator = unitAnimator;
        }

        public void TakeDamage(int damage) {
            CurrentHp -= damage;
            
            if (CurrentHp <= 0) {
                Death();
            } else {
                _animator.PlayHit();
            }
        }
        
        private void Death()
        {
            
            _animator.PlayDeath();
            _unitComponents.unitHealth.HealthChanged -= HealthChanged;
            DeathEvent?.Invoke(_unitComponents.id);        
        }

        public void Init(int maxHp) {
            MaxHp = maxHp;
            CurrentHp = MaxHp;
        }
    }
}