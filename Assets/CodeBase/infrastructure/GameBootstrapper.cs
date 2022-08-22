using System;
using CodeBase.Infrastructure;
using CodeBase.infrastructure.Services;
using CodeBase.infrastructure.States;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

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
