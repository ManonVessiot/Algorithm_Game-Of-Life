using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    public class GameOfLife
    {
        public Map _mapOfLife;

        public GameOfLife(Map mapOfLife)
        {
            _mapOfLife = mapOfLife;
            if (_mapOfLife == null)
            {
                Debug.Log("_mapOfLife null");
            }
        }

        public Map EvolveNextStage()
        {
            _mapOfLife.UpdateNeighbour();

            int width = _mapOfLife._map.GetLength(0);
            int height = _mapOfLife._map.GetLength(1);
            List<MapPosition> livingCells = new List<MapPosition>();
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    if (_mapOfLife._map[w, h]._living && (_mapOfLife._map[w, h]._livingNeighbour == 2 || _mapOfLife._map[w, h]._livingNeighbour == 3))
                    {
                        livingCells.Add(new MapPosition(w, h));
                    }
                    else if (!_mapOfLife._map[w, h]._living && _mapOfLife._map[w, h]._livingNeighbour == 3)
                    {
                        livingCells.Add(new MapPosition(w, h));
                    }
                }
            }

            return new Map(width, height, livingCells);
        }
    }
}
