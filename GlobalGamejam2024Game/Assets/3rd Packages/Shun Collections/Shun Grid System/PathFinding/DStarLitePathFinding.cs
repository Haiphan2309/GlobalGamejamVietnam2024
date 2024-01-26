using System;
using System.Collections.Generic;
using UnityEngine;


/* <summary>
D* Lite is a pathfinding algorithm that is similar to A* but is designed to handle changing environments, such as a partially mapped or dynamic grid. The algorithm uses two values for each cell on the grid: the g-value and the rhs-value.

The g-value represents the cost of the optimal path from the start cell to the current cell. Initially, all g-values are set to infinity except for the start cell, which is set to zero. As the algorithm explores the grid, it updates the g-values of cells that are closer to the start cell with more accurate costs.

The rhs-value represents the cost of the second-best path from the start cell to the current cell. Initially, all rhs-values are set to infinity except for the start cell, which is set to zero. As the algorithm explores the grid, it updates the rhs-values of cells that are farther from the start cell with more accurate costs.

The algorithm starts by creating a priority queue of cells to be explored. The priority queue is sorted by the sum of the g-value and the heuristic value (H) of each cell. At each iteration, the algorithm checks the cell with the lowest priority in the priority queue. If the cell's g-value is greater than its rhs-value, the cell's g-value is updated to its rhs-value and the cell's predecessors are updated. If the cell's g-value is less than its rhs-value, the cell's rhs-value is updated to its g-value and the cell is added to the priority queue.

As the algorithm progresses, it updates the g-values and rhs-values of cells and adds new cells to the priority queue. When the goal cell is found, the algorithm returns the path from the start cell to the goal cell.

One of the key features of D* Lite is that it is designed to work efficiently in dynamic environments. When the environment changes, the algorithm can be re-run with the updated costs and a new path will be found that takes the changes into account. This makes it a useful algorithm for applications such as robotics, where the environment can change frequently.
*/
namespace Shun_Grid_System
{
    public class DStarLitePathFinding<TGrid,TCell,TItem> : Pathfinding<TGrid, TCell, TItem> 
        where TGrid : BaseGrid2D<TCell,TItem> 
        where TCell : BaseGridCell2D<TItem>
    {
        private TCell _startCell, _endCell;
        private IPathFindingDistanceCost _distanceCostFunction;
        private IPathFindingAdjacentCellSelection<TCell, TItem> _adjacentCellSelectionFunction;
        private Priority_Queue.SimplePriorityQueue<TCell, QueueKey > _openCells = new ( new CompareFCostHCost()); // priority queue of open cells
        private double _km = 0; // km = heuristic for estimating cost of travel along the last path
        private Dictionary<TCell, double> _rhsValues = new (); // rhsValues[x] = the current best estimate of the cost from x to the goal
        private Dictionary<TCell, double> _gValues = new (); // gValues[x] = the cost of the cheapest path from the start to x
        private Dictionary<TCell, TCell> _predecessors = new (); // predecessors[x] = the cell that comes before x on the best path from the start to x
        private Dictionary<TCell, float> _dynamicObstacles = new(); // dynamicObstacle[x] = the cell that is found obstacle after find path and its found time
        
        
        public DStarLitePathFinding(TGrid gridXZ, IPathFindingAdjacentCellSelection<TCell, TItem> adjacentCellSelectionFunction = null, PathFindingCostFunction costFunctionType = PathFindingCostFunction.Manhattan) : base(gridXZ)
        {
            _adjacentCellSelectionFunction = adjacentCellSelectionFunction ?? new PathFindingAllAdjacentCellAccept<TCell, TItem>();
            _distanceCostFunction = costFunctionType switch
            {
                PathFindingCostFunction.Manhattan => new ManhattanDistanceCost(),
                PathFindingCostFunction.Euclidean => new EuclideanDistanceCost(),
                PathFindingCostFunction.Octile => new OctileDistanceCost(),
                PathFindingCostFunction.Chebyshev => new ChebyshevDistanceCost(),
                _ => throw new ArgumentOutOfRangeException(nameof(costFunctionType), costFunctionType, null)
            };
        }
    
        public DStarLitePathFinding(TGrid gridXZ, IPathFindingDistanceCost pathFindingDistanceCost) : base(gridXZ)
        {
            _distanceCostFunction = pathFindingDistanceCost;
        }

        private struct QueueKey
        {
            public double FCost;
            public double HCost;

            public QueueKey(double fCost = 0, double hCost = 0)
            {
                FCost = fCost;
                HCost = hCost;
            }
        }
        private class CompareFCostHCost : IComparer<QueueKey>
        {
            public int Compare(QueueKey x, QueueKey y)
            {
                int compare = x.FCost.CompareTo(y.FCost);
                if (compare == 0)
                {
                    compare = x.HCost.CompareTo(y.HCost);
                }
                return compare;
            }
        }

        public override LinkedList<TCell> FirstTimeFindPath(TCell startCell, TCell endCell, double maxCost = Double.PositiveInfinity)
        {
            ResetPathFinding(startCell, endCell);
        
            return FindPath();
        }

        public LinkedList<TCell> FindPath(double maxCost = Double.PositiveInfinity)
        {
            while (_openCells.Count > 0 &&
                   (CalculateKey(TryDequeue(out TCell currentCell)) < CalculateKey(_startCell) ||
                    GetRhsValue(_startCell) != GetGValue(_startCell)))
            {
                
                //Debug.Log("DStar current Cell" + currentCell.XIndex + " " + currentCell.ZIndex);

                if (GetGValue(currentCell) > GetRhsValue(currentCell))
                {
                    _gValues[currentCell] = _rhsValues[currentCell];

                    foreach (TCell neighbor in currentCell.InDegreeCells)
                    {
                        if (neighbor != null && !neighbor.IsObstacle 
                                             && _adjacentCellSelectionFunction.CheckMovableCell(currentCell, neighbor) 
                                             && !_dynamicObstacles.ContainsKey(neighbor))
                        {
                            UpdateCell(neighbor, currentCell);
                        }
                    }
                }
                else
                {
                    _gValues[currentCell] = double.PositiveInfinity;
                    UpdateCell(currentCell);

                    foreach (TCell neighbor in currentCell.InDegreeCells)
                    {
                        if (neighbor != null && !neighbor.IsObstacle 
                                             && _adjacentCellSelectionFunction.CheckMovableCell(currentCell, neighbor) 
                                             && !_dynamicObstacles.ContainsKey(neighbor))
                        {
                            UpdateCell(neighbor, currentCell);
                        }
                    }

                }
            }

            return RetracePath(_startCell, _endCell);
        }
    
        /*
        public override LinkedList<TCell> UpdatePathWithDynamicObstacle(TCell currentStartCell, List<TCell> foundDynamicObstacles, double maxCost = Double.PositiveInfinity)
        {
            ResetPathFinding(currentStartCell, _endCell);
        
            foreach (var obstacleCell in foundDynamicObstacles)
            {
                if (_dynamicObstacles.ContainsKey(obstacleCell)) continue;
                _dynamicObstacles[obstacleCell] = Time.time;
            }
        
            return FindPath(maxCost);
        }
        */
        public override LinkedList<TCell> UpdatePathWithDynamicObstacle(TCell currentStartCell, List<TCell> foundDynamicObstacles, double maxCost = Double.PositiveInfinity)
        {
            _km += GetDistanceCost(_startCell,currentStartCell);
            _startCell = currentStartCell;
            
            foreach (var obstacleCell in foundDynamicObstacles)
            {
                if (_dynamicObstacles.ContainsKey(obstacleCell)) continue;
                _dynamicObstacles[obstacleCell] = Time.time;

                foreach (TCell adjacentToObstacleCell in obstacleCell.InDegreeCells)
                {
                    UpdateCell(adjacentToObstacleCell);
                }
            }
        
            return FindPath(maxCost);
        }

        public override Dictionary<TCell, double> FindAllCellsSmallerThanCost(TCell currentStartNode, double maxCost = Double.PositiveInfinity)
        {
            throw new NotImplementedException();
        }


        private void UpdateCell(TCell updateCell, TCell itsPredecessorCell = null)
        {
            //Debug.Log("DStar UpdateCell " + cell.XIndex + " " + cell.ZIndex);
            if (updateCell != _endCell)
            {
                /*
             * Get the min rhs from Successors, then add it to the predecessors for traverse
             */
                double minRhs = double.PositiveInfinity;
                TCell minSucc = null;

                foreach (TCell successor in updateCell.OutDegreeCells)
                {
                    double rhs = _gValues.ContainsKey(successor)
                        ? GetGValue(successor) + GetDistanceCost(updateCell, successor)
                        : double.PositiveInfinity;
                    if (rhs < minRhs && !successor.IsObstacle
                                     && _adjacentCellSelectionFunction.CheckMovableCell(updateCell, successor) 
                                     && !_dynamicObstacles.ContainsKey(successor))
                        // Is the min successor, if it the same, choose the one not its press 
                        //if  ((rhs < minRhs||(rhs == minRhs && rhs !=double.PositiveInfinity  && successor != itsPredecessorCell)) && !successor.IsObstacle && !_dynamicObstacles.ContainsKey(successor))
                    {
                        minRhs = rhs;
                        minSucc = successor;
                    }
                }

                _rhsValues[updateCell] = minRhs;
                //_predecessors[upgradeCell] = minSucc ?? (_predecessors[upgradeCell] ?? null);
                _predecessors[updateCell] = minSucc;

            }

            if (_openCells.Contains(updateCell)) // refresh the old cell
            {
                _openCells.TryRemove(updateCell);
            }

            if (GetGValue(updateCell) != GetRhsValue(updateCell)) // Mainly if both not equal double.PositiveInfinity, meaning it found a path that is shorter
            {
                //_gValues[updateCell] = GetRhsValue(updateCell);
                var gValue = (double.IsPositiveInfinity(GetGValue(updateCell)) ? 999999999 : _gValues[updateCell]);
                var hValue = GetDistanceCost(updateCell, _startCell);
                var fValue = (gValue + hValue + _km);
                updateCell.GCost = gValue;
                updateCell.HCost = hValue;
                updateCell.FCost = fValue;

                _openCells.Enqueue(updateCell, new QueueKey(fValue, hValue)); // Enqueue the new cell
            }
        }

        /*
        private LinkedList<TCell> RetracePath(TCell startXZCell, TCell endXZCell)
        {
            LinkedList<TCell> path = new LinkedList<TCell>();
            TCell currentCell = startXZCell;
            HashSet<TCell> visitedCells = new();
        
            while (currentCell != endXZCell)
            {
                path.AddLast(currentCell);
                visitedCells.Add(currentCell);
            
                TCell nextCell = null;
                double minGCost = Double.PositiveInfinity;
                foreach (TCell successor in currentCell.AdjacentCells)
                {
                    double successorGCost = GetGValue(successor);
                    if ( successorGCost <= minGCost && !successor.IsObstacle
                                                    && _adjacentCellSelectionFunction.CheckMovableCell(currentCell, successor) 
                                                    && !_dynamicObstacles.ContainsKey(successor) 
                                                    && !visitedCells.Contains(successor))
                    {
                        nextCell = successor;
                        minGCost = successorGCost;
                    }
                
                }

                if (nextCell != null) currentCell = nextCell;
                else return null;
            }

            path.AddLast(endXZCell);

            return path;
        }
        */
        
        
        private LinkedList<TCell> RetracePath(TCell startCell, TCell endCell)
        {
            if (_endCell.IsObstacle|| _dynamicObstacles.ContainsKey(_endCell)) return null;
            
            LinkedList<TCell> path = new LinkedList<TCell>();
            TCell currentCell = startCell;
            
        
            while (currentCell != endCell)
            {
                path.AddLast(currentCell);
                
                TCell nextCell = null;
                double minGCost = Double.PositiveInfinity;
                foreach (TCell successor in currentCell.OutDegreeCells)
                {
                    if (successor.IsObstacle
                        || !_adjacentCellSelectionFunction.CheckMovableCell(currentCell, successor) 
                        || _dynamicObstacles.ContainsKey(successor)) continue;
                    
                    double successorGCost = GetGValue(successor);
                    
                    if ( successorGCost < minGCost || (nextCell != null && successorGCost == minGCost && CalculateKey(successor) < CalculateKey(nextCell)))
                    {
                        nextCell = successor;
                        minGCost = successorGCost;
                    }
                    
                }

                if (nextCell != null) 
                    currentCell = nextCell;
                else return null;
            }

            path.AddLast(endCell);

            return path;
        }

        private void ResetPathFinding(TCell startCell, TCell endCell)
        {
            _openCells = new ( new CompareFCostHCost()); // priority queue of open cells
            _km = 0; // km = heuristic for estimating cost of travel along the last path
            _rhsValues = new (); // rhsValues[x] = the current best estimate of the cost from x to the goal
            _gValues = new (); // gValues[x] = the cost of the cheapest path from the start to x
            _predecessors = new (); // predecessors[x] = the cell that comes before x on the best path from the start to x
            _dynamicObstacles = new(); // dynamicObstacle[x] = the cell that is found obstacle after find path and its found time

            this._startCell = startCell;
            this._endCell = endCell;
        
            _gValues[endCell] = double.PositiveInfinity;
            _rhsValues[endCell] = 0;

            _gValues[startCell] =  double.PositiveInfinity;
            _rhsValues[startCell] = double.PositiveInfinity;
            _predecessors[startCell] = null;

            _openCells.Enqueue(endCell, new QueueKey((int) CalculateKey(endCell) , 0));

        
        }

        private TCell TryDequeue(out TCell topCell)
        {
            topCell = _openCells.Dequeue();
            return topCell;
        }

        private double CalculateKey(TCell currCell)
        {
            return Math.Min(GetRhsValue(currCell), GetGValue(currCell)) + GetDistanceCost(currCell, _startCell) + _km;
        }

        private double GetRhsValue(TCell cell)
        {
            return _rhsValues.TryGetValue(cell, out double value) ? value : double.PositiveInfinity;
        }

        private double GetGValue(TCell cell)
        {
            return _gValues.TryGetValue(cell, out double value) ? value : double.PositiveInfinity;
        }

        protected virtual double GetDistanceCost(TCell start, TCell end)
        {
            var indexDifferenceAbsolute = Grid.GetIndexDifferenceAbsolute(start,end);

            return _distanceCostFunction.GetDistanceCost(indexDifferenceAbsolute.x, indexDifferenceAbsolute.y) + start.GetAdditionalOutDegreeAdjacentCellCost(end);
        }
    
    }
}