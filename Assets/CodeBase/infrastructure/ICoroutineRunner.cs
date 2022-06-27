using System.Collections;
using UnityEngine;

namespace CodeBase.infrastructure
{
  public interface ICoroutineRunner
  { 
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}

