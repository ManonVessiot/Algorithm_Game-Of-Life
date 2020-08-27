using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameOfLife
{
    public class ViewMapGameOfLife_Screeen : MonoBehaviour
    {
        public ViewMap _viewMap;
        public Color _colorOfLivingCells;
        public Color _colorOfDeadCells;

        public GameOfLife _gameOfLife;

        public Vector2 _epsilonBetweenCells = 0.1f * Vector2.one;
        public float _secondBetweenStages = 1f;

        public GameObject _cellImage;
        Image[,] _mapGameObjects;

        Vector2 _ScreenSize;
        Vector2 _cellSizeScreen;

        [System.NonSerialized]
        public bool solving = false;

        int stage = 0;

        public void StartSolving()
        {
            StartCoroutine(Solve());
        }
        IEnumerator Solve()
        {
            solving = true;
            while (solving)
            {
                yield return new WaitForSeconds(_secondBetweenStages);
                Debug.Log("stage : " + stage++);
                solving = !EvolveGameNextStage();
            }
        }
        public bool EvolveGameNextStage()
        {
            Map oldMap = _gameOfLife._mapOfLife;
            Map newMap = _gameOfLife.EvolveNextStage();

            _gameOfLife._mapOfLife = newMap;
            UpdateView();

            return oldMap.Equals(_gameOfLife._mapOfLife);
        }

        public void StopSolving()
        {
            StopAllCoroutines();
            solving = false;
    }
        public void Reset()
        {
            _cellSizeScreen.x = _ScreenSize.x / (float)_viewMap._width;
            _cellSizeScreen.y = _ScreenSize.y / (float)_viewMap._height;

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
                    rect.sizeDelta = new Vector2(_cellSizeScreen.x, _cellSizeScreen.y) - _epsilonBetweenCells;
                }
            }

            stage = 0;
            Map map = new Map(_viewMap._width, _viewMap._height, _viewMap._livingCells);
            _gameOfLife = new GameOfLife(map);
            solving = false;
            UpdateView();
        }

        private void Start()
        {
            _ScreenSize = new Vector2(Screen.width, Screen.height);
            Reset();
        }

        private void UpdateView()
        {
            if (_cellImage != null && _mapGameObjects != null)
            {
                int width = _mapGameObjects.GetLength(0);
                int height = _mapGameObjects.GetLength(1);
                for (int w = 0; w < width; w++)
                {
                    for (int h = 0; h < height; h++)
                    {
                        _mapGameObjects[w, h].color = _gameOfLife._mapOfLife._map[w, h]._living ? _colorOfLivingCells : _colorOfDeadCells;
                    }
                }
            }                
        }
        Vector3 GetScreenPostionOfNode(int w, int h)
        {
            Vector3 position = new Vector3((w + 0.5f) * _cellSizeScreen.x, (h + 0.5f) * _cellSizeScreen.y, 0);
            return position;
        }
    }
}