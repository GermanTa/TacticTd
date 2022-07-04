using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.SpawnerService;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.infrastructure.Services {
    public class DistanceControlService : IService, ITickable {
        private List<Mob> _mobs;

        public void Contructor(ISpawnerService spawnerService) {
            _mobs = spawnerService.GetAllMobs()
                .Select(x=>x.GetComponent<Mob>()).ToList();
            
        }

        public void Tick() {
            for (var i = 1; i < _mobs.Count; i++) {
                var previousMob = _mobs[i-1];
                var mob = _mobs[i];

                if ((previousMob.transform.position - mob.transform.position).magnitude < 1f) {
                    mob.MovingToWaypoints.SetState(MovingState.Staying);
                } else {
                    mob.MovingToWaypoints.SetState(MovingState.Walking);
                }
                
            }
        }
    }

    public interface ITickable {
        public void Tick();
    }
}