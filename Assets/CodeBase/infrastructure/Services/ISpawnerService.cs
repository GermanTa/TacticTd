using CodeBase.infrastructure.Services;
using CodeBase.StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnerService : IService
{
    public event Action<string> ChangedListMobs;
    public event Action<string> ChangedListMinics;
    public List<Mob> GetAllMobs();
    public List<UnitComponents> GetAllMinics();
    public void SpawnWave(LevelWaveId waveId, SpawnPoint spawnPoint);
    public void SpawnSelectedMinics(SpawnPointMinic[] spawnPoinst, MinicId[] minicsId);
    public Dictionary<string, Mob> Mobs { get; }
    public Dictionary<string, UnitComponents> Minics { get; } 
}
