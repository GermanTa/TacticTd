using CodeBase.infrastructure;
using CodeBase.infrastructure.Factory;
using CodeBase.StaticData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.SpawnerService
{
    public class SpawnerService : ISpawnerService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticDataService _staticDataService;
        private readonly IFactoryField _factoryField;
        private LevelStaticData _wave;

        private Dictionary<string, GameObject> _mobs = new Dictionary<string, GameObject>();

        public event Action<int> ChangedListMobsGO;
        public List<GameObject> GetAllMobs() => _mobs.Values.ToList();
        public List<GameObject> _minics = new List<GameObject>();
        public List<GameObject> GetMinics => _minics;

       

        public SpawnerService(IStaticDataService staticDataService, IFactoryField factoryField, ICoroutineRunner coroutineRunner)
        {
            _staticDataService = staticDataService;
            _factoryField = factoryField;
            _coroutineRunner = coroutineRunner;
        }

        public void DeleteMobFromList(string id)
        {
            _mobs.Remove(id);
        }

        public void SpawnSelectedMinics(SpawnPointMinic[] spawnPoinst, MinicId[] minicsId)
        {
            for (int i = 0; i < spawnPoinst.Length; i++)
            {
                var spawnPoint = spawnPoinst[i];
                var minic = _staticDataService.ForMinic(minicsId[i]).Prefab;
                var minicGO =  _factoryField.CreateMinic(spawnPoint, minic);
                _minics.Add(minicGO);
            }
        }

        public void SpawnWave(LevelWaveId waveId, SpawnPoint spawnPoint)
        {
            _wave = _staticDataService.ForWave(waveId);
            for (int i = 0; i < _wave.MonsterStaticData.Count; i++)
            {
                var item = _wave.MonsterStaticData[i];
                var mobGO = _factoryField.CreateMob(spawnPoint, item.Prefab); //item.prefabName
                //Resources.LoadAll<Mob>("Mobs");

                Mob mob = mobGO.GetComponent<Mob>();
                mob.MovingToWaypoints.Construct(_factoryField.Waypoints);
                mobGO.SetActive(false);
                var id = Guid.NewGuid().ToString();
                mob.MovingToWaypoints.id = id;
                _mobs.Add(id, mobGO);
            }

            _coroutineRunner.StartCoroutine(SpawnWaveCoroutine());
        }

     
        IEnumerator SpawnWaveCoroutine() {
            
            var spawnWaveCoroutine = new WaitForSeconds(1.5f);
            
            foreach (var mob in _mobs.Values) {
                mob.SetActive(true);
                yield return spawnWaveCoroutine;
            }

        }
    }

}
