using CodeBase.Enemy;
using CodeBase.Mobs;
using CodeBase.Services.SpawnerService;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public string Id;
    public MovingToWaypoints MovingToWaypoints;
    public EnemyAttack Attack;
    public MobHealth MobHealth;
    public EnemyDeath EnemyDeath;

   
}

public enum AttackType
{
    melee,
    archer,
    longArcher
}
