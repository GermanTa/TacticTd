using CodeBase.infrastructure.Services;
using UnityEngine;

namespace CodeBase.infrastructure.AssetsManager
{
    public interface IAssets : IService
    {
  
        public GameObject GetPrefabByName(string name);
        
    }
      
    
}