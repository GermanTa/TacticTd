using CodeBase.infrastructure;
using CodeBase.infrastructure.Factory;
using CodeBase.StaticData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.SpawnerService
{
    public class SpawnerService : ISpawnerService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticDataService _staticDataService;
        private readonly IFactoryField _factoryField;
        private LevelStaticData _wave;
        private LinkedList<MonsterStaticData> _linkedMinicStaticData = new LinkedList<MonsterStaticData>();
        public SpawnerService(IStaticDataService staticDataService, IFactoryField factoryField, ICoroutineRunner coroutineRunner)
        {
            _staticDataService = staticDataService;
            _factoryField = factoryField;
            _coroutineRunner = coroutineRunner;
        }

        public void SpawnSelectedMinics(SpawnPointMinic[] spawnPoinst, MinicId[] minicsId)
        {
            for (int i = 0; i < spawnPoinst.Length; i++)
            {
                var spawnPoint = spawnPoinst[i];
                var minic = _staticDataService.ForMinic(minicsId[i]).Prefab;
                _factoryField.CreateMinic(spawnPoint, minic);
            }
        }

        public void StartSapwnWaveCoroutine(LevelWaveId waveId, SpawnPoint spawnPoint)
        {
            _wave = _staticDataService.ForWave(waveId);
            foreach (var item in _wave.MonsterStaticData)
            {
                _linkedMinicStaticData.AddLast(item);
            }
            _coroutineRunner.StartCoroutine(SapwnWave(spawnPoint));
        }

     
        IEnumerator SapwnWave(SpawnPoint spawnPoint)
        {
            var i = 0;
            while(_wave.MonsterStaticData.Count > i)
            {
                var mob = _wave.MonsterStaticData[i].Prefab;
                _factoryField.CreateMob(spawnPoint, mob);
                i++;
                yield return new WaitForSeconds(1.5f);
            }
           
        }
    }

}
