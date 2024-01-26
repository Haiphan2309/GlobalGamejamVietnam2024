using System;
using System.Collections.Generic;

namespace Shun_Grid_System
{
    public enum PathFindingAlgorithmType
    {
        DStar,
        AStar
    }

    public interface IPathfindingAlgorithm<TGrid,TCell, TItem> 
        where TGrid : BaseGrid2D<TCell, TItem> 
        where TCell : BaseGridCell2D<TItem>
    {
        public LinkedList<TCell> FirstTimeFindPath(TCell startCell, TCell endCell, double maxCost = Double.PositiveInfinity);
        public LinkedList<TCell> UpdatePathWithDynamicObstacle(TCell currentStartNode, List<TCell> foundDynamicObstacles, double maxCost = Double.PositiveInfinity);
        public Dictionary<TCell, double> FindAllCellsSmallerThanCost(TCell currentStartNode, double maxCost = Double.PositiveInfinity);
    }

    public abstract class Pathfinding<TGrid, TCell, TItem> : IPathfindingAlgorithm<TGrid,TCell,TItem> 
        where TGrid : BaseGrid2D<TCell, TItem> 
        where TCell : BaseGridCell2D<TItem>
    {
        protected TGrid Grid;

        public Pathfinding(TGrid grid)
        {
            this.Grid = grid;
        }

        public abstract LinkedList<TCell> FirstTimeFindPath(TCell startCell, TCell endCell, double maxCost = Double.PositiveInfinity);

        public abstract LinkedList<TCell> UpdatePathWithDynamicObstacle(TCell currentStartNode, List<TCell> foundDynamicObstacles, double maxCost = Double.PositiveInfinity);
        public abstract Dictionary<TCell, double> FindAllCellsSmallerThanCost(TCell currentStartNode, double maxCost = Double.PositiveInfinity);
    }
}