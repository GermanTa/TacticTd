using System;
using CodeBase.Infrastructure;
using CodeBase.infrastructure.Services;
using CodeBase.infrastructure.States;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game1;
        [SerializeField] private DistanceControlService _distanceControlService;
      
        private void Awake()
        {
            //точка входа в игру
            _game1 = new Game(this, _distanceControlService);

            _distanceControlService.Contructor(AllServices.Container.Single<ISpawnerService>());
            AllServices.Container.RegisterSingle(_distanceControlService);
            //Нужно получить всех наследников от ITickable
            
            _game1.StateMachine.Enter<BootsrapState>();

            DontDestroyOnLoad(this); 
        }

        private void Update() {
            
            //У всех наследников ITickable вызвать Tick();
        }
    }
}
