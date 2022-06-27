using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.infrastructure
{
    public class SceneLoader 
    {  
        private readonly ICoroutineRunner _coroutineRunner;
        public SceneLoader(ICoroutineRunner coroutineRunner) => _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) => _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        //Корутина есть только у MonoBehevirius а это просто класс размышляйем дальше
        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {

            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }
      
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

            while (!waitNextScene.isDone)
                yield return null;
      
            onLoaded?.Invoke();
        }
    }
}

