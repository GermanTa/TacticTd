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

        private Dictionary<string, Mob> _mobs = new Dictionary<string, Mob>();
        private Dictionary<string, MinicComponents> _minics = new Dictionary<string, MinicComponents>();

        public event Action<string> ChangedListMobs;
        public event Action<string> ChangedListMinics;
        public List<Mob> GetAllMobs() => _mobs.Values.ToList();
        public List<MinicComponents> GetAllMinics() => _minics.Values.ToList();

        public Dictionary<string, Mob> Mobs => _mobs;
        public Dictionary<string, MinicComponents> Minics => _minics;

        public SpawnerService(IStaticDataService staticDataService, IFactoryField factoryField, ICoroutineRunner coroutineRunner)
        {
            _staticDataService = staticDataService;
            _factoryField = factoryField;
            _coroutineRunner = coroutineRunner;
        }

        public void DeleteMobFromList(string id) {
            _mobs.Remove(id);
            ChangedListMobs?.Invoke(id);
        }
        public void DeleteMinicFromList(string id)
        {
            _minics.Remove(id);
            ChangedListMinics?.Invoke(id);
        }

        public void SpawnSelectedMinics(SpawnPointMinic[] spawnPoinst, MinicId[] minicsId)
        {
            for (int i = 0; i < spawnPoinst.Length; i++)
            {
                var spawnPoint = spawnPoinst[i];
                var minic = _staticDataService.ForMinic(minicsId[i]).Prefab;
                var minicGO =  _factoryField.CreateMinic(spawnPoint, minic);
                var minicComponents = minicGO.GetComponent<MinicComponents>();
                minicComponents.Construct(this, _factoryField);
                var id = Guid.NewGuid().ToString();
                minicComponents.id = id;
                _minics.Add(minicComponents.id, minicComponents);  
            }
        }

        public void SpawnWave(LevelWaveId waveId, SpawnPoint spawnPoint)
        {
            _wave = _staticDataService.ForWave(waveId);
            for (int i = 0; i < _wave.MonsterStaticData.Count; i++)
            {
                var item = _wave.MonsterStaticData[i];
                var mobGO = _factoryField.CreateMob(spawnPoint, item.Prefab);

                Mob mob = mobGO.GetComponent<Mob>();
                mob.MovingToWaypoints.Construct(_factoryField.Waypoints);
                mobGO.SetActive(false);
                var id = Guid.NewGuid().ToString();
                mob.Id = id;
                mob.EnemyDeath.Construct(this);
                _mobs.Add(id, mob);
            }

            _coroutineRunner.StartCoroutine(SpawnWaveCoroutine());
        }

     
        IEnumerator SpawnWaveCoroutine() {
            
            var spawnWaveCoroutine = new WaitForSeconds(1.5f / 3);
            
            foreach (var mob in _mobs.Values) {
                mob.gameObject.SetActive(true);
                yield return spawnWaveCoroutine;
            }

        }

        public void SpawnProjectile(Mob target, Vector3 origin, float damage) {
            //_factoryField.CreateProjectile
        }
    }

}
