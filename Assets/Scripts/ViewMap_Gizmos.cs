using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    public class ViewMap_Gizmos : ViewMap
    {
        public Vector2 _cellSize = Vector2.one;

        private void OnValidate()
        {
            Reset();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(_viewMap._width * _cellSize.x, _viewMap._height * _cellSize.x, 1));

            int width = _gameOfLifeRules._mapOfLife._map.GetLength(0);
            int height = _gameOfLifeRules._mapOfLife._map.GetLength(1);
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    Gizmos.color = _gameOfLifeRules._mapOfLife._map[w, h]._living ? Color.black : Color.white;
                    Gizmos.DrawCube(GetWorldPostionOfNode(w, h), _cellSize - _epsilonBetweenCells);
                }
            }
        }
        Vector3 GetWorldPostionOfNode(int w, int h)
        {
            int width = _gameOfLifeRules._mapOfLife._map.GetLength(0);
            int height = _gameOfLifeRules._mapOfLife._map.GetLength(1);

            Vector3 position = transform.position + new Vector3(_cellSize.x * (w + 0.5f - width * 0.5f), _cellSize.y * (h + 0.5f - height * 0.5f), 0);
            return position;
        }
    }
}