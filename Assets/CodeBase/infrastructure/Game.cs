using CodeBase.infrastructure;
using CodeBase.infrastructure.Services;
using CodeBase.infrastructure.States;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, coroutineRunner);
      
        }

    
    }
}