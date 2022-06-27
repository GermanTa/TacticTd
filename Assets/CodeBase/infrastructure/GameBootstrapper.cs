using CodeBase.Infrastructure;
using CodeBase.infrastructure.States;
using UnityEngine;

namespace CodeBase.infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game1;
      
        private void Awake()
        {
            //точка входа в игру
            _game1 = new Game(this);
            _game1.StateMachine.Enter<BootsrapState>();

            DontDestroyOnLoad(this); 
        }
        
        
    }
}
