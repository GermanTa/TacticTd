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
            _spawnerService.ChangedListMobsGO += ChangedListMobsGO;
       }

        public void DistanceControllerUpdate()
        {
            _mobs = _spawnerService.GetAllMobs().Select(x => x.GetComponent<Mob>()).ToList();
            _minics = _spawnerService.GetAllMinics().Select(x => x.GetComponent<MinicComponents>()).ToList(); 
            _coroutineRunner.StartCoroutine(DistanseControllerCorutine());
        }

        IEnumerator DistanseControllerCorutine()
        {
            var i = 0;
            var indexMinics = 0;
         
            while (true)
            {
                
                yield return null;
                Mob previousMob = null;

                if (i > 0) {
                    previousMob = _mobs[i - 1];
                }
                
                var mob = _mobs[i];
                MinicComponents minic = _minics[indexMinics];
                var firstMinicPosition = _minics[0].transform.position;
                
                
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

                
                if ( (minic.transform.position - mob.transform.position).magnitude <= minic.minicAttack.EffectiveDistance)
                {
                    minic.minicAttack.Target = mob.MobHealth;
                    Debug.Log(mob.MobHealth);
                    minic.minicAttack.SetState(AttackState.StartAttack);
                } else
                {
                    minic.minicAttack.SetState(AttackState.EndAttack);
                }

                i++;
                indexMinics++;

                if (i >= _mobs.Count)
                {
                    i = 0;
                }

                if (indexMinics >= _minics.Count)
                {
                    indexMinics = 0;
                }
            }
        }

        private void ChangedListMobsGO(string obj)
        {
            _mobs = _spawnerService.GetAllMobs().Select(x => x.GetComponent<Mob>()).ToList();
            _minics = _spawnerService.GetAllMinics().Select(x => x.GetComponent<MinicComponents>()).ToList();
        }
    }
}

