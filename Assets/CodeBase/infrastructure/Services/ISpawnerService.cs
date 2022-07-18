using CodeBase.infrastructure.Services;
using CodeBase.StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnerService : IService
{
    public void SpawnWave(LevelWaveId waveId, SpawnPoint spawnPoint);
    public void SpawnSelectedMinics(SpawnPointMinic[] spawnPoinst, MinicId[] minicsId);
    public void DeleteMobFromList(string id);
    public event Action<int> ChangedListMobsGO;

    public List<GameObject> GetAllMobs();
    List<GameObject> GetMinics { get; }
}
