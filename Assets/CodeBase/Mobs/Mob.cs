using CodeBase.Enemy;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public MovingToWaypoints MovingToWaypoints;
    public Attack Attack;
}

public enum AttackType
{
    melee,
    archer,
    longArcher
}
