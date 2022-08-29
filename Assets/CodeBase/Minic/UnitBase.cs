using CodeBase.Minic;
using CodeBase.Services.SpawnerService;
using CodeBase.UI.Elements;
using UnityEngine;

public class UnitBase : MonoBehaviour {
    public string id;
    
    public UnitHealth unitHealth;
    public UnitAttack unitAttack;
    public UnitAnimator unitAnimator;
    private SpawnerService _spawnerService;
    public ActorUi ActorUi;
    
    public virtual void Construct(SpawnerService spawnerService) {
        _spawnerService = spawnerService;
        unitHealth.InjectDependencies(this, unitAnimator);
        unitAttack.InjectDependencies(this, unitAnimator, spawnerService);
        ActorUi.InjectDependencies(this, unitHealth);
    }

    public void Dispose() {
        unitHealth.Dispose();
        unitAttack.Dispose();
        ActorUi.Dispose();
    }
}