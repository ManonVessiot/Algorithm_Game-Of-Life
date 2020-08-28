using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    public class ViewMap : MonoBehaviour
    {
        public MapToBuild _viewMap;
        public GameOfLife _gameOfLifeRules = new GameOfLife();

        public Vector2 _epsilonBetweenCells = 0.1f * Vector2.one;
        public float _secondBetweenStages = 1f;

        [System.NonSerialized]
        public bool solving = false;

        int stage = 0;
        
        private void Start()
        {
            Reset();
        }

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
            Map oldMap = _gameOfLifeRules._mapOfLife;
            Map newMap = _gameOfLifeRules.EvolveNextStage();

            _gameOfLifeRules._mapOfLife = newMap;
            UpdateView();
            Debug.Log("stage : " + stage++);

            return oldMap.Equals(_gameOfLifeRules._mapOfLife);
        }

        public void StopSolving()
        {
            StopAllCoroutines();
            solving = false;
        }

        public virtual void Reset()
        {
            ResetMap();
        }
        protected void ResetMap()
        {
            foreach (MapPosition cell in _viewMap._livingCells)
            {
                cell._x += _viewMap._cellShift._x;
                cell._y += _viewMap._cellShift._y;
            }
            _viewMap._cellShift._x = 0;
            _viewMap._cellShift._y = 0;

            stage = 0;
            Map map = new Map(_viewMap._width, _viewMap._height, _viewMap._livingCells);
            _gameOfLifeRules._mapOfLife = map;
            solving = false;
            UpdateView();
        }








        protected virtual void UpdateView()
        {
            UnityEditor.SceneView.RepaintAll();
        }
    }
}
