using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "MinicData", menuName = "StaticData/Minic")]
  public class MinicStaticData : ScriptableObject
  {
    public GameObject Prefab;
    public MinicId MinicId;

    [Range(1,100)]
    public int Hp;
    [Range(1,30)]
    public float Damage;
    [Range(1,100)]
    public int Armor;
        
        
    [System.Serializable]
    public class CharLevelUps
    {
      public int Hp;
      public float Damage;
      public int Armor;
    }
           
  }
}