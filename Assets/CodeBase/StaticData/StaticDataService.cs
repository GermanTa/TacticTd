using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string MonstersDataPath = "StaticData/Levels";
    private const string MinicsDataPath = "StaticData/Minics";
    private const string FieldDataPath = "StaticData/GameField";
    
    private Dictionary<LevelWaveId, LevelStaticData> _waves;
    private Dictionary<MinicId, MinicStaticData> _minics;
    private Dictionary<string, GameFieldStaticData> _gameFields;

   public void LoadGameFields()
   {
      _gameFields = Resources.LoadAll<GameFieldStaticData>(FieldDataPath).ToDictionary(x => x.id, x => x);
           
   }

    public void LoadWaveMonsters()
    {
      //секд
      //загружает MonsterStaticData и берет его id в виде ключа и саму статик дату в виде значения
      _waves = Resources.LoadAll<LevelStaticData>(MonstersDataPath)
        .ToDictionary(x => x.LevelWaveId, x => x);
      
    }
    
    public void LoadMinics()
    {
      //секд
      //загружает MonsterStaticData и берет его id в виде ключа и саму статик дату в виде значения
      _minics = Resources.LoadAll<MinicStaticData>(MinicsDataPath)
        .ToDictionary(x => x.MinicId, x => x);
    }
    
    public LevelStaticData ForWave(LevelWaveId typeId)
    {
      
      if (_waves.TryGetValue(typeId, out LevelStaticData staticData))
      {
        return staticData;
      }
      else
      {
        return null;
      }
    }
    
    public MinicStaticData ForMinic(MinicId typeId)
    {
      
      if (_minics.TryGetValue(typeId, out MinicStaticData staticData))
      {
        return staticData;
      }
      else
      {
        return null;
      }
    }

     public GameFieldStaticData ForGameField(string id)
      {

            if(_gameFields.TryGetValue(id, out GameFieldStaticData staticData))
            {
                return staticData;
            }
            else
            {
                return null;
            }
       }



    }

  
}