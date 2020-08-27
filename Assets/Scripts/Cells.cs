using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    public class Cells
    {
        public bool _living;
        public int _livingNeighbour;

        public Cells(bool living)
        {
            _living = living;
            _livingNeighbour = 0;
        }
    }
}
