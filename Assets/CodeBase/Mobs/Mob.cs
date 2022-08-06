using CodeBase.Enemy;
using CodeBase.Mobs;
using CodeBase.UI.Elements;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public string Id;
    public MovingToWaypoints MovingToWaypoints;
    public EnemyAttack Attack;
    public MobHealth MobHealth;
    public EnemyDeath EnemyDeath;
    public ActorUi ActorUi;
}

public enum AttackType
{
    melee,
    archer,
    longArcher
}
