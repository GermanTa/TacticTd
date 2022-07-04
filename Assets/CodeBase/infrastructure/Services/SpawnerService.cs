using CodeBase.infrastructure;
using CodeBase.infrastructure.Factory;
using CodeBase.StaticData;
using System;
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
        private LinkedList<GameObject> _linkedMinicStaticData = new LinkedList<GameObject>();
        List<GameObject> _listMobsGO = new List<GameObject>();

        public event Action<int> ChangedListMobsGO;
        public SpawnerService(IStaticDataService staticDataService, IFactoryField factoryField, ICoroutineRunner coroutineRunner)
        {
            _staticDataService = staticDataService;
            _factoryField = factoryField;
            _coroutineRunner = coroutineRunner;
        }

        public void DeleteMobFromList(int index)
        {
            if (index > _listMobsGO.Count) return;
            _listMobsGO.RemoveAt(index);
            ChangedListMobsGO?.Invoke(index);
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
            for (int i = 0; i < _wave.MonsterStaticData.Count; i++)
            {
                var item = _wave.MonsterStaticData[i];
                var mobGO = _factoryField.CreateMob(spawnPoint, item.Prefab, _linkedMinicStaticData, _listMobsGO);
                Mob mob = mobGO.GetComponent<Mob>();
                mob.MovingToWaypoints.Index = i;
                mob.MovingToWaypoints.Construct(this);
                mobGO.SetActive(false);

                _linkedMinicStaticData.AddLast(mobGO);
                _listMobsGO.Add(mobGO);
            }
           

  
            _coroutineRunner.StartCoroutine(SapwnWave(spawnPoint));
        }

     
        IEnumerator SapwnWave(SpawnPoint spawnPoint)
        {
            var current = _linkedMinicStaticData.First;
            var i = 0;
           

            while (current.Next != null)
            {
                current.Value.SetActive(true);
                current = current.Next;
                i++;
                yield return new WaitForSeconds(1.5f);
            }

            _linkedMinicStaticData.Last.Value.SetActive(true);


        }
    }

}
