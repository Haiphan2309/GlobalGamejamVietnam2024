using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shun_Grid_System
{
    [Serializable]
    public class GridXYCell<TItem> : BaseGridCell2D<TItem>
    {
        [Header("Base")] private GridXY<TItem> _gridXY;
        
        
        public GridXYCell(GridXY<TItem> grid, int x, int y, TItem item = default) : base(x,y,item)
        {
            _gridXY = grid;
        }
        
    }
}