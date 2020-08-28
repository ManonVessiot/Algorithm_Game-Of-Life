using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    [System.Serializable]
    public class GameOfLife
    {
        public Map _mapOfLife;
        public List<int> _numberOfNeighbourToLive;
        public List<int> _numberOfNeighbourToBeBorn;

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
                    if (_mapOfLife._map[w, h]._living && _numberOfNeighbourToLive.Contains(_mapOfLife._map[w, h]._livingNeighbour))
                    {
                        livingCells.Add(new MapPosition(w, h));
                    }
                    else if (!_mapOfLife._map[w, h]._living && _numberOfNeighbourToBeBorn.Contains(_mapOfLife._map[w, h]._livingNeighbour))
                    {
                        livingCells.Add(new MapPosition(w, h));
                    }
                }
            }

            return new Map(width, height, livingCells);
        }
    }
}
