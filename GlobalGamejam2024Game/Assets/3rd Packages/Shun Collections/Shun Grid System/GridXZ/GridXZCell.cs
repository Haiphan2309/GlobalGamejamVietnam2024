using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shun_Grid_System
{
    [Serializable]
    public class GridXZCell<TItem> : BaseGridCell2D<TItem>
    {
        [Header("Base")] private GridXZ<TItem> _gridXZ;
        
        
        public GridXZCell(GridXZ<TItem> grid, int x, int y, TItem item = default) : base(x,y,item)
        {
            _gridXZ = grid;
        }
        
    }
}