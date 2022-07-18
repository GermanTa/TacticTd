using CodeBase.Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.infrastructure.Services {
    public class DistanceControlService : IService {
        
        private List<Mob> _mobs;
        private List<GameObject> _minics;
        ICoroutineRunner _coroutineRunner;
        ISpawnerService _spawnerService; 

       public DistanceControlService(ISpawnerService spawnerService, ICoroutineRunner coroutineRunner) {
            _coroutineRunner = coroutineRunner;
            _spawnerService = spawnerService;

       }

        public void DistanceControllerUpdate()
        {
            _mobs = _spawnerService.GetAllMobs().Select(x => x.GetComponent<Mob>()).ToList();
            _minics = _spawnerService.GetMinics;
            _coroutineRunner.StartCoroutine(DistanseControllerCorutine());
        }


        IEnumerator DistanseControllerCorutine()
        {
            var i = 1;
            var indexMinics = 0;
         
            while (true)
            {
                yield return null;
                
                var previousMob = _mobs[i - 1];
                var mob = _mobs[i];

                
                if ((_minics[0].transform.position - _mobs[0].transform.position ).magnitude <= 1f)
                {
                    _mobs[0].MovingToWaypoints.SetState(MovingState.Staying);
                    
                }
                else
                {
                    mob.MovingToWaypoints.SetState(MovingState.Walking);
                }
                 
                if ((mob.transform.position - previousMob.transform.position).magnitude <= 1f)
                {
                    mob.MovingToWaypoints.SetState(MovingState.Staying);
                }
                else
                {
                    mob.MovingToWaypoints.SetState(MovingState.Walking);
                }

                if (mob.MovingToWaypoints.CurrentState == MovingState.Staying)
                {
                    if ((_mobs[0].transform.position - _minics[0].transform.position).magnitude <= 1f)
                    {
                        _mobs[0].Attack.Target = _minics[0].GetComponent<IHealth>();
                        _mobs[0].Attack.SetState(AttackState.StartAttack);
                    }
                  
                    //_mobs[0].Attack.Target = _minics[0].GetComponent<IHealth>();
                    //_mobs[0].Attack.SetState(AttackState.StartAttack);
                }



                i++;
                indexMinics++;

                if (i >= _mobs.Count)
                {
                    i = 1;
                }

                if (indexMinics >= _minics.Count)
                {
                    indexMinics = 0;
                }
            }
        }

        


    }
}

