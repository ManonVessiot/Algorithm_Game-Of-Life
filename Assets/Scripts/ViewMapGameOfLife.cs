using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameOfLife
{
    public class ViewMapGameOfLife : MonoBehaviour
    {
        public int _width = 5;
        public int _height = 3;
        public List<MapPosition> _livingCells;

        public GameOfLife _gameOfLife;


        public float _cellSize = 1f;
        public float _epsilonBetweenCells = 0.1f;

        public float _secondBetweenStages = 1f;

        [System.NonSerialized]
        public bool solving = false;

        public void StartSolving()
        {
            StartCoroutine(Solve());
        }
        IEnumerator Solve()
        {
            solving = true;
            int stage = 0;
            Debug.Log("stage : " + stage++);
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

            return oldMap.Equals(_gameOfLife._mapOfLife);
        }

        public void StopSolving()
        {
            StopAllCoroutines();
            solving = false;
    }
        public void Reset()
        {
            Map map = new Map(_width, _height, _livingCells);
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
            Gizmos.DrawWireCube(transform.position, new Vector3(_width * _cellSize, _height * _cellSize, 1));

            for (int w = 0; w < _width; w++)
            {
                for (int h = 0; h < _height; h++)
                {
                    Gizmos.color = _gameOfLife._mapOfLife._map[w, h]._living ? Color.black : Color.white;
                    Gizmos.DrawCube(GetWorldPostionOfNode(w, h), Vector3.one * (_cellSize - _epsilonBetweenCells));
                }
            }
        }
        Vector3 GetWorldPostionOfNode(int w, int h)
        {
            Vector3 position = new Vector3(w + _cellSize * (1 - _width) / 2f, h + _cellSize * (1 - _height) / 2f, 0);
            return position;
        }
    }
}