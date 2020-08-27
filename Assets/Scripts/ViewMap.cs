using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    public class ViewMap : MonoBehaviour
    {
        public int _width = 5;
        public int _height = 3;
        public List<MapPosition> _livingCells;
    }
}