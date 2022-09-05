using CodeBase.Enemy;
using CodeBase.Services.SpawnerService;


// Есть Mob, есть Minic, у них много общего.
// Выделяем у них общий класс - UnitBase
// У компонентов тоже выделяем общий класс, в котором описываем их связь с Unit-ом.
// BattleService - обращается с мобами и миниками как с UnitBase
// 



public class Mob : UnitBase
{
    //public string Id;
    public MovingToWaypoints MovingToWaypoints;
    public override void Construct(SpawnerService spawnerService) {
        base.Construct(spawnerService);
        MovingToWaypoints.InjectDependencies(this, unitAnimator);
    }
}

public enum AttackType
{
    melee,
    archer,
    longArcher
}
