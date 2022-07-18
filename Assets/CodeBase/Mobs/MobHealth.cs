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
      public int CurrentHp { get; set; }
      public int MaxHp { get; set; }
      
      public void TakeDamage(int damage)
      {
            CurrentHp -= damage;
            Debug.Log(CurrentHp);
    
         Animator.PlayHit();
    
         HealthChanged?.Invoke();
      }
   }
}
