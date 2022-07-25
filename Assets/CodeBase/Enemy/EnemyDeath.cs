using CodeBase.Enemy;
using CodeBase.Mobs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public EnemyAnimator enemyAnimator;
    public MobHealth mobHealth;
    public event Action HappenedDeath;
    private ISpawnerService spawnerService;
    private Mob mob;


    private void Start()
    {
        mob = GetComponent<Mob>();
        mobHealth.HealthChanged += HealthChanged;
    }

    public void Construct(ISpawnerService spawnerService)
    {
        this.spawnerService = spawnerService;
        Debug.Log(spawnerService);
    }
    private void HealthChanged()
    {
        Debug.Log(mobHealth.CurrentHp + " mobHealth.CurrentHp ");
        if (mobHealth.CurrentHp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        spawnerService.DeleteMobFromList(mob.Id);
        mobHealth.HealthChanged -= HealthChanged;
        Destroy(gameObject);
        HappenedDeath?.Invoke();
    }
}
