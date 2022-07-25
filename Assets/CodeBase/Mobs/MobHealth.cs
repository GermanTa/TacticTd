using System;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Mobs {
    public class MobHealth : MonoBehaviour, IHealth {
        public EnemyAnimator Animator;
        public event Action HealthChanged;
        public event Action DeathEvent;
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }

        public void TakeDamage(int damage) {
            Debug.Log("tut " + CurrentHp);
            CurrentHp -= damage;
            HealthChanged?.Invoke();
            
            if (CurrentHp <= 0) {
                DeathEvent?.Invoke();
            }

            Animator.PlayHit();
            HealthChanged?.Invoke();
        }
    }
}