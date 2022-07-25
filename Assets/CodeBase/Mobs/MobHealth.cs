using System;
using CodeBase.Enemy;
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
            Debug.Log("tut " + CurrentHp);
            CurrentHp -= damage;
    
         Animator.PlayHit();
    
         HealthChanged?.Invoke();
      }
   }
}
