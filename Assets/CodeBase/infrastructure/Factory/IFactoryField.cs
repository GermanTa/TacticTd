using CodeBase.infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;


namespace CodeBase.infrastructure.Factory
{
  public interface IFactoryField : IService
  {
        WayPoint[] Waypoints { get; }

        public void CreateGameField(GameObject prefab);
        public GameObject CreateMob(SpawnPoint spawnPoint, GameObject prefab);
        public GameObject CreateMinic(SpawnPointMinic spawnPoint, GameObject prefab);


    }
}