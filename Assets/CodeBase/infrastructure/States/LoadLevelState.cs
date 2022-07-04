using CodeBase.infrastructure.Factory;
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
     
        private string nameLevel;
        MinicId[] minicsId = new MinicId[] { MinicId.ShieldMaidenMary, MinicId.Countess, MinicId.BountyHunter, MinicId.BoneArcher};

        public LoadLevelState(GameStateMachine gameStateMachine,
            SceneLoader sceneLoader, 
            IFactoryField factoryField, 
            IStaticDataService staticDataService,
            ISpawnerService spawnerService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _factoryField = factoryField;
            _staticDataService = staticDataService;
            _spawnerService = spawnerService;
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



            if (nameLevel == "Main")
            {
                
             
               

              
            }

            

        }
    }
}
