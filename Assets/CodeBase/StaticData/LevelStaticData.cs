using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{

   public enum TypeField
    {
        path,
        wall,
        startPoint,
        EndPoint
    }

        
    [CreateAssetMenu(fileName = "LvlData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public LevelWaveId LevelWaveId;
        public List<MonsterStaticData> MonsterStaticData;
    }
}
