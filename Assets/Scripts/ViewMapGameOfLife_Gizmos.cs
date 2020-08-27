using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    public class ViewMapGameOfLife_Gizmos : MonoBehaviour
    {
        public ViewMap _viewMap;

        public GameOfLife _gameOfLife;

        public Vector2 _cellSize = Vector2.one;
        public Vector2 _epsilonBetweenCells = 0.1f * Vector2.one;

        public float _secondBetweenStages = 0.25f;

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
            UnityEditor.SceneView.RepaintAll();
            Debug.Log("stage : " + stage++);

            return oldMap.Equals(_gameOfLife._mapOfLife);
        }

        public void StopSolving()
        {
            StopAllCoroutines();
            solving = false;
        }
        public void Reset()
        {
            stage = 0;
            Map map = new Map(_viewMap._width, _viewMap._height, _viewMap._livingCells);
            _gameOfLife = new GameOfLife(map);
            solving = false;
            UnityEditor.SceneView.RepaintAll();
        }

        private void Start()
        {
            Reset();
        }

        private void OnValidate()
        {
            Reset();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(_viewMap._width * _cellSize.x, _viewMap._height * _cellSize.x, 1));

            int width = _gameOfLife._mapOfLife._map.GetLength(0);
            int height = _gameOfLife._mapOfLife._map.GetLength(1);
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    Gizmos.color = _gameOfLife._mapOfLife._map[w, h]._living ? Color.black : Color.white;
                    Gizmos.DrawCube(GetWorldPostionOfNode(w, h), _cellSize - _epsilonBetweenCells);
                }
            }
        }
        Vector3 GetWorldPostionOfNode(int w, int h)
        {
            int width = _gameOfLife._mapOfLife._map.GetLength(0);
            int height = _gameOfLife._mapOfLife._map.GetLength(1);
            Vector3 position = new Vector3(w + _cellSize.x * (1 - width) / 2f, h + _cellSize.x * (1 - height) / 2f, 0);
            return position;
        }
    }
}