using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.infrastructure.Logic
{

    public class  SelectedMinics : MonoBehaviour
    {
      private MinicId[] _minics = new MinicId[4];
      
      public void Construct()
      {
        _minics[0] = MinicId.ShieldMaidenMary;
        _minics[1] = MinicId.Countess;
        _minics[2] = MinicId.BountyHunter;
        _minics[3] = MinicId.BoneArcher;
      }
      
      
      public MinicId[] Minics
      {
        get
        {
          return _minics;   
        }
        
      }

    }
}
