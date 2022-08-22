using System;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Mobs {
    public class MobHealth : MonoBehaviour, IHealth {

        public Mob mob;
        public EnemyAnimator Animator;
        public event Action HealthChanged;
        public event Action<string> DeathEvent;
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }

        public void TakeDamage(int damage) {
            if (CurrentHp <= 0) {
                DeathEvent?.Invoke(mob.Id);
            }

            CurrentHp -= damage;
            HealthChanged?.Invoke();

            Animator.PlayHit();
            
        }
    }
}