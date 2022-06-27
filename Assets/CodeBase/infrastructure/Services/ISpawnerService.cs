using CodeBase.infrastructure.Services;
using CodeBase.StaticData;

public interface ISpawnerService : IService
{
    public void StartSapwnWaveCoroutine(LevelWaveId waveId, SpawnPoint spawnPoint);
    public void SpawnSelectedMinics(SpawnPointMinic[] spawnPoinst, MinicId[] minicsId);
}
