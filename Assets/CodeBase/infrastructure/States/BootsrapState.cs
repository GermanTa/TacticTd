using CodeBase.infrastructure.AssetsManager;
using CodeBase.infrastructure.Factory;
using CodeBase.infrastructure.Services.MapData;
using CodeBase.infrastructure.Services;
using CodeBase.StaticData;
using CodeBase.Services.SpawnerService;

namespace CodeBase.infrastructure.States
{
  public class BootsrapState : IState
  {
    private const string Initial = "initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootsrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;

      RegisterServices();


      
    }
    
    private void RegisterServices()
    {
      RegisterStaticData();
     
      _services.RegisterSingle<IMapDataService>(new MapData());
      _services.RegisterSingle<IAssets>(new AssetsProvider());
      _services.RegisterSingle<IFactoryField>(new FactoryField(
        _services.Single<IStaticDataService>(),
        _services.Single<IAssets>(),
        _services.Single<IMapDataService>()
        ));
       _services.RegisterSingle<ISpawnerService>(new SpawnerService(
           _services.Single<IStaticDataService>(),
           _services.Single<IFactoryField>(),
           _stateMachine.CoroutineRunner
           ));




        }
    
    private void RegisterStaticData()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.LoadWaveMonsters();
      staticData.LoadMinics();
      staticData.LoadGameFields();
   
      _services.RegisterSingle(staticData); 
    }


    public void Exit()
    {
      
    }
    public void Enter()
    {
     
      _sceneLoader.Load(Initial,EnterLoadLevel);
      
    }

    private void EnterLoadLevel()
    {
      
      _stateMachine.Enter<LoadLevelState,string>("Main");
      
    } 
  }
}