using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOfLife
{
    public class ViewMap_Screeen : ViewMap
    {
        public Color _colorOfLivingCells;
        public Color _colorOfDeadCells;

        public GameObject _cellImage;
        Image[,] _mapGameObjects;

        Vector2 _ScreenSize;

        private void Awake()
        {
            _ScreenSize = new Vector2(Screen.width, Screen.height);
        }

        public override void Reset()
        {
            _cellSize.x = _ScreenSize.x / (float)_viewMap._width;
            _cellSize.y = _ScreenSize.y / (float)_viewMap._height;

            if (_mapGameObjects == null || (_mapGameObjects.GetLength(0) != _viewMap._width || _mapGameObjects.GetLength(1) != _viewMap._height))
            {
                if (_mapGameObjects != null)
                {
                    foreach (Image obj in _mapGameObjects)
                    {
                        if (obj != null)
                        {
                            Destroy(obj.gameObject);
                        }
                    }
                }

                _mapGameObjects = new Image[_viewMap._width, _viewMap._height];
                for (int w = 0; w < _viewMap._width; w++)
                {
                    for (int h = 0; h < _viewMap._height; h++)
                    {
                        GameObject obj = Instantiate(_cellImage, transform);
                        obj.name = "_mapGameObjects[" + w + ", " + h + "]";

                        _mapGameObjects[w, h] = obj.GetComponent<Image>();

                        RectTransform rect = _mapGameObjects[w, h].GetComponent<RectTransform>();
                        rect.position = GetScreenPostionOfNode(w, h);
                    }
                }
            }
            for (int w = 0; w < _viewMap._width; w++)
            {
                for (int h = 0; h < _viewMap._height; h++)
                {
                    RectTransform rect = _mapGameObjects[w, h].GetComponent<RectTransform>();
                    rect.sizeDelta = new Vector2(_cellSize.x, _cellSize.y) - _epsilonBetweenCells;
                }
            }

            ResetMap();
        }

        protected override void UpdateView()
        {
            if (_cellImage != null && _mapGameObjects != null)
            {
                int width = _mapGameObjects.GetLength(0);
                int height = _mapGameObjects.GetLength(1);
                for (int w = 0; w < width; w++)
                {
                    for (int h = 0; h < height; h++)
                    {
                        _mapGameObjects[w, h].color = _gameOfLifeRules._mapOfLife._map[w, h]._living ? _colorOfLivingCells : _colorOfDeadCells;
                    }
                }
            }                
        }
        Vector3 GetScreenPostionOfNode(int w, int h)
        {
            Vector3 position = new Vector3((w + 0.5f) * _cellSize.x, (h + 0.5f) * _cellSize.y, 0);
            return position;
        }
    }
}