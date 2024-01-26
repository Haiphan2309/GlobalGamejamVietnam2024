using System;
using UnityEngine;

namespace Shun_Grid_System
{
    public class GridXY<TItem> : BaseGrid2D<GridXYCell<TItem>, TItem>
    {
        public GridXY(int width, int height, float cellWidthSize, float cellHeightSize, Vector3 worldOriginPosition) 
            : base(width, height, cellWidthSize, cellHeightSize, worldOriginPosition)
        {
            
        }

        public override Vector2Int GetIndex(Vector3 position)
        {
            int x = Mathf.RoundToInt((position - WorldOriginPosition).x / CellWidthSize);
            int y = Mathf.RoundToInt((position - WorldOriginPosition).y / CellHeightSize);
            return new (x,y);
        }

        public override Vector3 GetWorldPositionOfNearestCell(int x, int y)
        {
            return new Vector3(x * CellWidthSize, y * CellHeightSize, 0) + WorldOriginPosition;
        }
    }
}
