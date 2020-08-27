using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    #pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
    public class Map
    #pragma warning restore CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
    {
        public Cells[,] _map;

        public Map(int width, int height, List<MapPosition> livingCells = null)
        {
            _map = new Cells[width, height];

            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    _map[w, h] = new Cells((livingCells != null && livingCells.Contains(new MapPosition(w, h))));
                }
            }
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;

            Map c = obj as Map;
            if ((System.Object)c == null)
                return false;

            int width = _map.GetLength(0);
            int height = _map.GetLength(1);
            bool sameCells = true;
            for (int w = 0; w < width && sameCells; w++)
            {
                for (int h = 0; h < height && sameCells; h++)
                {
                    sameCells = _map[w, h]._living == c._map[w, h]._living;
                }
            }
            return sameCells;
        }

        public void UpdateNeighbour()
        {
            int width = _map.GetLength(0);
            int height = _map.GetLength(1);
            // init living neighbour
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    _map[w, h]._livingNeighbour = 0;
                }
            }

            // count living neighbour
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    if (_map[w, h]._living)
                    {
                        CellLiving(w, h);
                    }
                }
            }
        }

        private void CellLiving(int w, int h)
        {
            int width = _map.GetLength(0);
            int height = _map.GetLength(1);
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        int neighbourX = w + x;
                        int neighbourY = h + y;
                        if ((neighbourX >= 0 && neighbourX < width) && (neighbourY >= 0 && neighbourY < height))
                        {
                            _map[neighbourX, neighbourY]._livingNeighbour += 1;
                        }
                    }
                }
            }
        }
    }
}
