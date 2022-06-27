using System;
using CodeBase.Enemy;
using CodeBase.infrastructure.Logic;
using UnityEngine;

namespace CodeBase.Mobs
{
   public class MobHealth : MonoBehaviour, IHealth
   {
      public EnemyAnimator Animator;
      public event Action HealthChanged;
      public float Current { get; set; }
      public float Max { get; set; }
      
      public void TakeDamage(float damage)
      {
         Current -= damage;
    
         Animator.PlayHit();
    
         HealthChanged?.Invoke();
      }
   }
}
