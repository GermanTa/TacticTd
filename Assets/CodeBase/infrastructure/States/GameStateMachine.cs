using System;
using System.Collections.Generic;
using CodeBase.infrastructure.Factory;
using CodeBase.infrastructure.Logic;
using CodeBase.infrastructure.Services;
using CodeBase.StaticData;

namespace CodeBase.infrastructure.States
{
    public class GameStateMachine 
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;
        private ICoroutineRunner _coroutineRunner;

        public ICoroutineRunner CoroutineRunner
        {
            get { return _coroutineRunner; }
        }

        public GameStateMachine(SceneLoader sceneLoader, AllServices services, ICoroutineRunner coroutineRunner,
            DistanceControlService distanceControlService)
        {
            _coroutineRunner = coroutineRunner;
            _states = new Dictionary<Type, IExitableState>()
            {
                
                [typeof(BootsrapState)] = new BootsrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this,sceneLoader,
                services.Single<IFactoryField>(),
                services.Single<IStaticDataService>(),
                services.Single<ISpawnerService>()
                ),
            };
            
            services.RegisterSingle(distanceControlService);
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            //? если не null
            TState state = ChangeState<TState>();
            state.Enter();
        }
    
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            //? если не null
            TState state = ChangeState<TState>();
            state.Enter(payload);

        }
    
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;
    }
    
    
}
