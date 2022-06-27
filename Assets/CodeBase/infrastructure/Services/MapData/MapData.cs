using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.infrastructure.Services.MapData
{
    public class MapData : IMapDataService
    {

        private int _widthField;
        private int _heigthField;
        private GameObject _textAsset;

        public void SetDimensions(List<string> textLines)
        {
            _widthField = textLines[0].Length;
            _heigthField = textLines.Count;
        }


        //public List<string> GetTextFromStaticData(GameFieldStaticData gameFieldStaticData)
        //{
        //    List<string> lines = new List<string>();
        //    _textAsset = gameFieldStaticData.prefabGameField;

        //    if (_textAsset != null)
        //    {
        //         string textData = _textAsset.text;
        //         string[] delimiters = { "\r\n", "\n" };
        //         lines.AddRange(textData.Split(delimiters, System.StringSplitOptions.None));
        //     }
        //     else
        //     {
        //       Debug.LogWarning("TextAssets null");
        //     }

        //        return lines;
        // }

        //  public int[,] MakeMap(GameFieldStaticData gameFieldStaticData)
        //  {
        //    List<string> lines = new List<string>();
        //    lines = GetTextFromStaticData(gameFieldStaticData);
        //    SetDimensions(lines);
        //    int[,] map = new int[_widthField, _heigthField];

        //    for (int y = 0; y < _heigthField; y++)
        //    {
        //      for (int x = 0; x < _widthField; x++)
        //      {
        //        map[x, y] = (int) Char.GetNumericValue(lines[y][x]);
        //      }
        //    }

        //    return map;
        //  }
        //}

    }
}