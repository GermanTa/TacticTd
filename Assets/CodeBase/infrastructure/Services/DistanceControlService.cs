using System;
using CodeBase.Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.infrastructure.Services {
    public class DistanceControlService : IService {
        private List<Mob> _mobs;
        private List<MinicComponents> _minics;
        ICoroutineRunner _coroutineRunner;
        ISpawnerService _spawnerService;

        public DistanceControlService(ISpawnerService spawnerService, ICoroutineRunner coroutineRunner) {
            _coroutineRunner = coroutineRunner;
            _spawnerService = spawnerService;
            _spawnerService.ChangedListMobs += ChangedListMobsGO;
            _spawnerService.ChangedListMinics += ChangedListMinics;
        }

       

        public void DistanceControllerUpdate() {
            _mobs = _spawnerService.GetAllMobs().Select(x => x.GetComponent<Mob>()).ToList();
            _minics = _spawnerService.GetAllMinics().Select(x => x.GetComponent<MinicComponents>()).ToList();
            _coroutineRunner.StartCoroutine(DistanseControllerCorutine());
        }

        IEnumerator DistanseControllerCorutine() {
            while (true) {
                yield return null;
                Mob previousMob = null;

                var firstMinicPosition = _minics[0].transform.position;
                
                for (int i = 0; i < _mobs.Count; i++) {
                    Mob mob = _mobs[i];
                    if (i > 0) {
                        previousMob = _mobs[i - 1];
                    }


                    if (mob.MovingToWaypoints.CurrentState == MovingState.Walking) {
                        if (previousMob != null &&
                            (mob.transform.position - previousMob.transform.position).magnitude <= 1f) {
                            mob.MovingToWaypoints.SetState(MovingState.Staying);
                        } else if ((mob.transform.position - firstMinicPosition).magnitude <= 1f) {
                            mob.MovingToWaypoints.SetState(MovingState.Staying);
                        }
                    } else {
                        if (previousMob != null &&
                            (mob.transform.position - previousMob.transform.position).magnitude > 1f) {
                            mob.MovingToWaypoints.SetState(MovingState.Walking);
                        } else if (i == 0 && (mob.transform.position - firstMinicPosition).magnitude > 1f) {
                            mob.MovingToWaypoints.SetState(MovingState.Walking);
                        }
                    }

                    if ((mob.transform.position - firstMinicPosition).magnitude <= mob.Attack.EffectiveDistance) {
                        mob.Attack.Target = _minics[0].GetComponent<IHealth>();
                        mob.Attack.SetState(AttackState.StartAttack);
                    } else {
                        mob.Attack.SetState(AttackState.EndAttack);
                    }
                }


                for (int j = 0; j < _minics.Count; j++) {
                    MinicComponents minic = _minics[j];
                    for (int k = 0; k < _mobs.Count; k++) {
                        var mob = _mobs[k];
                        if ((minic.transform.position - mob.transform.position).magnitude <=
                            minic.minicAttack.EffectiveDistance) {
                            minic.minicAttack.Target = mob.MobHealth;                         
                            minic.minicAttack.SetState(AttackState.StartAttack);
                            break;
                        } else {
                            minic.minicAttack.SetState(AttackState.EndAttack);
                        }
                    }
                }
            }
        }

        private void ChangedListMobsGO(string obj) {
            _mobs = _spawnerService.GetAllMobs().Select(x => x.GetComponent<Mob>()).ToList();
        }

        private void ChangedListMinics(string obj)
        {
            _minics = _spawnerService.GetAllMinics().Select(x => x.GetComponent<MinicComponents>()).ToList();
        }
    }
}