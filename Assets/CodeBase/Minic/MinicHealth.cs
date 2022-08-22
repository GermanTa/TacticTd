using System;
using UnityEngine;

namespace CodeBase.Minic
{
  [RequireComponent(typeof(MinicAnimator))]
  public class MinicHealth : MonoBehaviour, IHealth
  {
        MinicComponents minicComponent;
        private int _health = 50;
        private int _maxHp = 50;
        public MinicAnimator _animator;
        public event Action HealthChanged;
        public event Action<string> DeathEvent;

        public int MaxHp { get; set; }
        public int CurrentHp
        {
            get { return _health; }
            set { _health = value; HealthChanged?.Invoke(); }

        }

        private void Awake()
        {
            minicComponent = GetComponent<MinicComponents>();
        }

        public void TakeDamage(int damage)
        { 

            if(CurrentHp <= 0)
            {
                DeathEvent.Invoke(minicComponent.id);
                return;
            }

            CurrentHp -= damage;

            _animator.PlayHit();
        }
    }
}