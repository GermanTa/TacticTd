using CodeBase.infrastructure;
using CodeBase.infrastructure.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


//ѕровер€ет всех мобов и миников, достаточно ли они близки дл€ атаки
//”станавливает target IHealth врага тому, кто может атаковать
//«апоминает кто кого атакует
//ѕодписываетс€ на смерть IHealth
//ѕри смерти очищает target у атакующего и посылает запрос на удаление умершего
//spawnerService.DeleteMobFromList(mob.Id); - spawnerService должен удал€ть gameobject умершего
//sdfsdf

public class BattleService : IService {
    private List<Mob> _mobs;
    private List<MinicComponents> _minics;
    ICoroutineRunner _coroutineRunner;
    ISpawnerService _spawnerService;
    private Dictionary<string, string> _minicAttacked = new Dictionary<string, string>();
    private Dictionary<string, string> _mobAttacked = new Dictionary<string, string>();


    public BattleService(ISpawnerService spawnerService, ICoroutineRunner coroutineRunner) {
        _coroutineRunner = coroutineRunner;
        _spawnerService = spawnerService;
        _spawnerService.ChangedListMobs += ChangedListMobsGO;
        _spawnerService.ChangedListMinics += ChangedListMinics;
    }

    public void BattleManagerUpdate() {
        _mobs = _spawnerService.GetAllMobs();
        _minics = _spawnerService.GetAllMinics();
        AddListenerOnTargetDeath();
        _coroutineRunner.StartCoroutine(BattleManagerCorutine());
    }

    IEnumerator BattleManagerCorutine() {
        while (true) {
            for (int i = 0; i < _mobs.Count; i++) {
                Mob mob = _mobs[i];
                for (int j = 0; j < _minics.Count; j++) {
                    MinicComponents minic = _minics[j];
                    if ((mob.transform.position - minic.transform.position).magnitude <= mob.Attack.EffectiveDistance) {
                        _minicAttacked[minic.id] = mob.Id;
                        mob.Attack.Target = _minics[j].GetComponent<IHealth>();
                        mob.Attack.SetState(AttackState.StartAttack);
                        break;
                    } else {
                        mob.Attack.SetState(AttackState.EndAttack);
                    }
                }
            }

            for (int k = 0; k < _minics.Count; k++) {
                MinicComponents minic = _minics[k];
                for (int x = 0; x < _mobs.Count; x++) {
                    Mob mob = _mobs[x];
                    if ((minic.transform.position - mob.transform.position).magnitude <=
                        minic.minicAttack.EffectiveDistance) {
                        _mobAttacked[mob.Id] = minic.id;
                        minic.minicAttack.Target = mob;
                        minic.minicAttack.SetState(AttackState.StartAttack);
                        break;
                    } else {
                        minic.minicAttack.SetState(AttackState.EndAttack);
                    }
                }
            }

            yield return null;
        }
    }

    private void AddListenerOnTargetDeath() {
        for (int i = 0; i < _minics.Count; i++) {
            MinicComponents minicCoponent = _minics[i];
            minicCoponent.minicHealth.DeathEvent += OnTargetDeathMinic;
        }

        for (int j = 0; j < _mobs.Count; j++) {
            Mob mob = _mobs[j];
            mob.MobHealth.DeathEvent += OnTargetDeathMob;
        }
    }

    private void OnTargetDeathMob(string id) {
        string attackersId = _mobAttacked[id];
        var minic = _spawnerService.Minics[attackersId];
        minic.minicAttack.Target = null;

        _spawnerService.DeleteMobFromList(id);
    }

    private void OnTargetDeathMinic(string id) {
        string attackersId = _minicAttacked[id];
        var mob = _spawnerService.Mobs[attackersId];
        mob.Attack.Target = null;

        _spawnerService.DeleteMinicFromList(id);
    }

    private void ChangedListMobsGO(string obj) {
        _mobs = _spawnerService.GetAllMobs();
    }

    private void ChangedListMinics(string obj) {
        _minics = _spawnerService.GetAllMinics();
    }
}