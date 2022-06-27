using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.infrastructure.AssetsManager
{
    public class AssetsProvider : IAssets
    {
        
        private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
     
        public AssetsProvider()
       {
           
            _prefabs = Resources.LoadAll<AssetsProviderPrefab>(AssetsPath.AssetsProviderPrefab).ToDictionary(x => x.transform.name, x => x.gameObject);
           

        }

        public GameObject GetPrefabByName(string name)
        {
            if (_prefabs.ContainsKey(name))
            {
                return _prefabs[name];
            }

            return null;
        }
      
    }
}