using CodeBase.infrastructure.AssetsManager;
using CodeBase.infrastructure.Services.MapData;
using CodeBase.Minic;
using CodeBase.Services.SpawnerService;
using CodeBase.StaticData;
using GameFieldMono;
using Unity.VisualScripting;
using UnityEngine;


namespace CodeBase.infrastructure.Factory {
    public class FactoryField : IFactoryField {
        private IStaticDataService _staticDataService;
        private IAssets _assetsProvider;
        private GameObject _fieldControllerPrefab;
        private GameObject _spawnPoint;
        private WayPoint[] _waypoints;
        private SpawnerService _spawnerService;

        public WayPoint[] Waypoints => _waypoints;

        public GameObject SpawnPoint {
            get { return _spawnPoint; }
        }

        public FactoryField(
            IStaticDataService staticDataService,
            IAssets assetsProvider,
            IMapDataService mapData
            ) {
            _staticDataService = staticDataService;
            _assetsProvider = assetsProvider;
        }

        public void InjectDependencies(SpawnerService spawnerService) {
            _spawnerService = spawnerService;
        }

        public void CreateGameField(GameObject prefab) {
            var test = Object.Instantiate(prefab);
            _waypoints = test.GetComponent<GameFiled>().wayPoints;
        }

        public GameObject CreateMob(SpawnPoint spawnPoint, GameObject prefab) {
            var mobGO = Object.Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
            var secondWayPoint = _waypoints[1].transform.position;
            mobGO.transform.LookAt(secondWayPoint);
            Mob mob = mobGO.GetComponent<Mob>();
            
            mob.unitHealth.Init(50);

            
            return mobGO;
        }

        public GameObject CreateMinic(SpawnPointMinic spawnPoint, GameObject prefab) {
            var minic = Object.Instantiate(prefab, spawnPoint.transform.position, Quaternion.Euler(0f, 90f, 0f));
            UnitBase minicComponents = minic.GetComponent<UnitBase>();
            //minicComponents.unitHealth.Init(200);
            //minicComponents.ActorUi.Construct(minicComponents.minicHealth);
            
            return minic;
        }

        public Projectile CreateProjectile(Projectile prefab, Vector3 origin) {
            return Object.Instantiate(prefab, origin, Quaternion.Euler(0f, 90f, 0f));
        }
    }
}
