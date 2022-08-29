using CodeBase.Minic;
using CodeBase.UI.Elements;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Services.SpawnerService;
using UnityEngine;
using CodeBase.infrastructure.Factory;

public class MinicComponents : MonoBehaviour
{
    public string id;
    public MinicAnimator minicAnimator;
    public MinicHealth minicHealth;
    public MinicAttack minicAttack;
    public ActorUi ActorUi;
    public MinicDeath MinicDeath;
    private SpawnerService _spawnerService;
    public bool isRange;

    public void Construct(SpawnerService spawnerService, IFactoryField factoryField) {
        _spawnerService = spawnerService;
        minicAttack.Construct(spawnerService, factoryField);
        MinicDeath.Construct(spawnerService);
    }
}
