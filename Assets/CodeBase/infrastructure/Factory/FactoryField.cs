using CodeBase.infrastructure.AssetsManager;
using CodeBase.infrastructure.Services.MapData;
using CodeBase.StaticData;
using GameFieldMono;
using System.Collections.Generic;
using UnityEngine;



namespace CodeBase.infrastructure.Factory
{
  public  class FactoryField : IFactoryField
  {
    private IStaticDataService _staticDataService;
    private IAssets _assetsProvider;
    private GameObject _fieldControllerPrefab;
    private GameObject _spawnPoint;
    private WayPoint[] _waypoints;

    public WayPoint[] Waypoints
    {
        get { return _waypoints; }
    }


    public GameObject SpawnPoint
    {
      get { return _spawnPoint; }
    }

    public FactoryField(
      IStaticDataService staticDataService,
      IAssets assetsProvider,
      IMapDataService mapData)
    {
      _staticDataService = staticDataService;
      _assetsProvider = assetsProvider; 
    }

    public void CreateGameField(GameObject prefab)
    {
       var test = Object.Instantiate(prefab);
       _waypoints = test.GetComponent<GameFiled>().wayPoints;
            
    }

    public GameObject CreateMob(SpawnPoint spawnPoint, GameObject prefab, LinkedList<GameObject> linkedMinicStaticData, List<GameObject> listMinicStaticData)
    {
       var mobGO = Object.Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
       Mob mob = mobGO.GetComponent<Mob>();
       mob.MovingToWaypoints.Construct(_waypoints,linkedMinicStaticData,listMinicStaticData);
       return mobGO;
    }

     public GameObject CreateMinic(SpawnPointMinic spawnPoint, GameObject prefab)
     {
            var minic = Object.Instantiate(prefab, spawnPoint.transform.position, Quaternion.Euler(0f, 90f, 0f));
            return minic;
     } 
    }
}