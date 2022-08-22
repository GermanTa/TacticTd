using CodeBase.infrastructure.Factory;
using CodeBase.infrastructure.Services;
using CodeBase.StaticData;
using GameFieldMono;
using UnityEngine;


namespace CodeBase.infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IFactoryField _factoryField;
        private readonly IStaticDataService _staticDataService;
        private readonly ISpawnerService _spawnerService;
        private readonly DistanceControlService _distanceControlService;
        private readonly BattleService _battleManagerService;
        private string nameLevel;
        MinicId[] minicsId = new MinicId[] { MinicId.ShieldMaidenMary, MinicId.Countess, MinicId.BountyHunter, MinicId.BoneArcher};

        public LoadLevelState(GameStateMachine gameStateMachine,
            SceneLoader sceneLoader, 
            IFactoryField factoryField, 
            IStaticDataService staticDataService,
            ISpawnerService spawnerService,
            DistanceControlService distanceControlService,
            BattleService battleManagerService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _factoryField = factoryField;
            _staticDataService = staticDataService;
            _spawnerService = spawnerService;
            _distanceControlService = distanceControlService;
            _battleManagerService = battleManagerService;
        }
        
        public void Enter(string payload)
        {
            nameLevel = payload;
            _sceneLoader.Load(payload, OnLoaded);
        }

        public void Exit()
        {
           
        }
        
        private void OnLoaded()
        {
            GameObject prefabGameField = _staticDataService.ForGameField(nameLevel).prefabGameField;
            _factoryField.CreateGameField(prefabGameField);
            GameFiled gameFiled = prefabGameField.GetComponent<GameFiled>();
            SpawnPoint spawnPoint = gameFiled.spawnPoint;
            SpawnPointMinic[] spawnPointsMinic = gameFiled.spawnPointsMinic;
            _spawnerService.SpawnWave(LevelWaveId.OneWave, spawnPoint);
            _spawnerService.SpawnSelectedMinics(spawnPointsMinic, minicsId);
            _distanceControlService.DistanceControllerUpdate();
            _battleManagerService.BattleManagerUpdate();

            if (nameLevel == "Main")
            {
                   
            }         
        }
    }
}
