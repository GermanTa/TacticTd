using UnityEngine;


namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
  public class MonsterStaticData : ScriptableObject {
    
    public string prefabName;
    public GameObject Prefab;
    public MonsterTypeId MonsterTypeId;
    
    
    [Range(1,100)]
    public int Hp;
    [Range(1,30)]
    public float Damage;
    [Range(1,10)]
    public float MoveSpeed;
    
  }
}