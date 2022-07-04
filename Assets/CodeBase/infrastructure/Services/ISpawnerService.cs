using CodeBase.infrastructure.Services;
using CodeBase.StaticData;
using System;

public interface ISpawnerService : IService
{
    public void StartSapwnWaveCoroutine(LevelWaveId waveId, SpawnPoint spawnPoint);
    public void SpawnSelectedMinics(SpawnPointMinic[] spawnPoinst, MinicId[] minicsId);
    public void DeleteMobFromList(int index);
    public event Action<int> ChangedListMobsGO;

}
