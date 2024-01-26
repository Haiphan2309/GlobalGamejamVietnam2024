using UnityEngine;

namespace Shun_Grid_System
{
    public abstract class BaseGrid2D<TCell, TItem> where TCell : BaseGridCell2D<TItem>
    {
        public readonly int Width, Height;
        public readonly float CellWidthSize, CellHeightSize;
        protected Vector3 WorldOriginPosition;
        protected readonly TCell[,] GridCells;

        public BaseGrid2D(int width = 100, int height = 100, float cellWidthSize = 1f, float cellHeightSize = 1f, Vector3 worldOriginPosition = new Vector3())
        {
            Width = width;
            Height = height;
            CellHeightSize = cellHeightSize;
            CellWidthSize = cellWidthSize;
            WorldOriginPosition = worldOriginPosition;
            GridCells = new TCell[Width, Height];
    
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    GridCells[x,y] = default;
                    //Debug.DrawLine(GetWorldPosition(x,y) , GetWorldPosition(x+1,y), Color.red, 10f);
                    //Debug.DrawLine(GetWorldPosition(x,y) , GetWorldPosition(x,y+1), Color.red, 10f);
                }
            }
        }

        public abstract Vector2Int GetIndex(Vector3 position);
        public bool CheckValidCell(int xIndex, int yIndex)
        {
            return (xIndex < Width && xIndex >= 0 && yIndex < Height && yIndex >= 0);
        }

        public bool CheckValidCell(Vector3 worldPosition)
        {
            var index = GetIndex(worldPosition);
            return (index.x < Width && index.x >= 0 && index.y < Height && index.y >= 0);
        }

        public abstract Vector3 GetWorldPositionOfNearestCell(int x, int y);

        public Vector3 GetWorldPositionOfNearestCell(Vector3 worldPosition)
        {
            var index = GetIndex(worldPosition);
            return GetWorldPositionOfNearestCell(index.x, index.y);
        }
        
        public Vector3 GetWorldPositionOfNearestCell(TCell cell)
        {
            return GetWorldPositionOfNearestCell(cell.XIndex, cell.YIndex);
        }

        public void SetCell(TCell cell, int xIndex, int yIndex)
        {
            if (xIndex < Width && xIndex >= 0 && yIndex < Height && yIndex >= 0)
            {
                GridCells[xIndex, yIndex] = cell;
            }
        }

        public void SetCell(TCell cell, Vector3 position)
        {
            Vector2Int index = GetIndex(position);
            if(CheckValidCell(index.x, index.y))
            {
                GridCells[index.x, index.y] = cell;
            };
        }

        public TCell GetCell(int xIndex, int yIndex)
        {
            if(CheckValidCell(xIndex, yIndex)) return GridCells[xIndex, yIndex];
            return default(TCell);
        }

        public TCell GetCell(Vector3 position)
        {
            Vector2Int index = GetIndex(position);
            if(CheckValidCell(index.x, index.y))
            {
                return GridCells[index.x, index.y];
            }
            return default(TCell);
        }

        
        public Vector2Int GetIndexDifferenceFrom(BaseGridCell2D<TItem> subtrahendCell,BaseGridCell2D<TItem> minuendCell)
        {
            return new (minuendCell.XIndex - subtrahendCell.XIndex, minuendCell.YIndex - subtrahendCell.YIndex);
        }

        public Vector2Int GetIndexDifferenceAbsolute(BaseGridCell2D<TItem> subtrahendCell,BaseGridCell2D<TItem> minuendCell)
        {
            return new (Mathf.Abs(minuendCell.XIndex - subtrahendCell.XIndex), Mathf.Abs(minuendCell.YIndex - subtrahendCell.YIndex));
        }

        public Vector2Int GetMaxIndex()
        {
            return new (Width, Height);
        }

        public Vector2 GetCellWorldSize()
        {
            return new (CellWidthSize, CellHeightSize);
        }
    
    }
}
