using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    public class BuildObstaclesWithMouse : MonoBehaviour
    {
        public MapToBuild _map; // _map._width && _map._height
        public ViewMap _viewMap; // _viewMap._cellSize

        Vector2 _ScreenSize = new Vector2(Screen.width, Screen.height);
        bool addLivingCells = true;

        private void Start()
        {
            Debug.Log("_ScreenSize : " + _ScreenSize);
        }

        // Update is called once per frame
        void Update()
        {
            if (!_viewMap._solving && (0 <= Input.mousePosition.x && Input.mousePosition.x < _ScreenSize.x) && (0 <= Input.mousePosition.y && Input.mousePosition.y < _ScreenSize.y))
            {
                MapPosition mousePose = GetMapPostionOfScreenPoint(Input.mousePosition);

                if (Input.GetMouseButtonDown(2))
                {
                    if (_map._livingCells.Contains(mousePose))
                    {
                        addLivingCells = false;
                    }
                    else
                    {
                        addLivingCells = true;
                    }
                }
                else if (Input.GetMouseButton(2))
                {
                    bool contains = _map._livingCells.Contains(mousePose);
                    if (!addLivingCells && contains)
                    {
                        _map._livingCells.Remove(mousePose);
                    }
                    else if (addLivingCells && !contains)
                    {
                        _map._livingCells.Add(mousePose);
                    }
                    _viewMap.Reset();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_viewMap._solving)
                {
                    Debug.Log("StopSolving");
                    _viewMap.StopSolving();
                }
                else
                {
                    Debug.Log("StartSolving");
                    _viewMap.StartSolving();
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (_viewMap._solving)
                {
                    Debug.Log("StopSolving");
                    _viewMap.StopSolving();
                }
                Debug.Log("Reset");
                _viewMap.Reset();
            }
        }

        public virtual MapPosition GetMapPostionOfScreenPoint(Vector2 screenPoint)
        {
            int w = Mathf.FloorToInt(screenPoint.x / _viewMap._cellSize.x);
            int h = Mathf.FloorToInt(screenPoint.y / _viewMap._cellSize.y);

            return new MapPosition(w, h);
        }
    }
}