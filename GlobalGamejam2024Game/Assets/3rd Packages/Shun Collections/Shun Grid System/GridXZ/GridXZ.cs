using System;
using UnityEngine;

namespace Shun_Grid_System
{
    public class GridXZ<TItem> : BaseGrid2D<GridXZCell<TItem>, TItem>
    {
        public GridXZ(int width, int height, float cellWidthSize, float cellHeightSize, Vector3 worldOriginPosition) 
            : base(width, height, cellWidthSize, cellHeightSize, worldOriginPosition)
        {
            
        }

        public override Vector2Int GetIndex(Vector3 position)
        {
            int x = Mathf.RoundToInt((position - WorldOriginPosition).x / CellWidthSize);
            int z = Mathf.RoundToInt((position - WorldOriginPosition).z / CellHeightSize);
            return new (x,z);
        }

        public override Vector3 GetWorldPositionOfNearestCell(int x, int y)
        {
            
            return new Vector3(x * CellWidthSize, 0,y * CellHeightSize) + WorldOriginPosition;
        }
    }
}
