using UnityEngine;

namespace Shun_Grid_System
{
    public interface IPathFindingAdjacentCellSelection<TCell, TItem> where TCell : BaseGridCell2D<TItem>
    {
        bool CheckMovableCell(TCell from, TCell to);
    }

    public class PathFindingAllAdjacentCellAccept<TCell, TItem> : IPathFindingAdjacentCellSelection<TCell,TItem> 
        where TCell : BaseGridCell2D<TItem>
    {
        public bool CheckMovableCell(TCell from, TCell to)
        {
            return true;
        }
    }
}