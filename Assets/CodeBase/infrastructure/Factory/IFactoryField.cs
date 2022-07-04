using CodeBase.infrastructure.Services;
using CodeBase.StaticData;
using System.Collections.Generic;
using UnityEngine;


namespace CodeBase.infrastructure.Factory
{
  public interface IFactoryField : IService
  {
        WayPoint[] Waypoints { get; }

        public void CreateGameField(GameObject prefab);
        public GameObject CreateMob(SpawnPoint spawnPoint, GameObject prefab, LinkedList<GameObject> linkedMinicStaticData, List<GameObject> listMinicStaticData);
        public GameObject CreateMinic(SpawnPointMinic spawnPoint, GameObject prefab);


    }
}