using System.Collections.Generic;
using CodeBase.infrastructure.Services;
using UnityEngine;

namespace CodeBase.StaticData
{
  public interface IStaticDataService : IService
  {
    void LoadWaveMonsters();
    void LoadMinics();
    public void LoadGameFields();
    public GameFieldStaticData ForGameField(string id);
    LevelStaticData ForWave(LevelWaveId typeId);
    MinicStaticData ForMinic(MinicId typeId);

  }

 
}