using CodeBase.UI.Elements;
using CodeBase.Services.SpawnerService;

public class UnitComponents : UnitBase
{
    public ActorUi ActorUi;

    public override void Construct(SpawnerService spawnerService) {
        base.Construct(spawnerService);
    }
}