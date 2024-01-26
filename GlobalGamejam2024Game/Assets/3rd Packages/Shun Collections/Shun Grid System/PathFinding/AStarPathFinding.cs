using System;
using System.Collections.Generic;


namespace Shun_Grid_System
{
    public class AStarPathFinding<TGrid,TCell,TItem> : Pathfinding<TGrid, TCell, TItem> 
        where TGrid : BaseGrid2D<TCell,TItem> 
        where TCell : BaseGridCell2D<TItem>
    {
        private TCell _startCell, _endCell;
        private Dictionary<TCell, double> _gValues = new (); // gValues[x] = the cost of the cheapest path from the start to x
        private Dictionary<TCell, TCell> _predecessors = new (); // predecessors[x] = the cell that comes before x on the best path from the start to x
        private Dictionary<TCell, float> _dynamicObstacles = new(); // dynamicObstacle[x] = the cell that is found obstacle after find path and its found time
        private IPathFindingDistanceCost _distanceCostFunction;
        private IPathFindingAdjacentCellSelection<TCell, TItem> _adjacentCellSelectionFunction;

        public AStarPathFinding(TGrid gridXZ, IPathFindingAdjacentCellSelection<TCell, TItem> adjacentCellSelectionFunction = null, PathFindingCostFunction costFunctionType = PathFindingCostFunction.Manhattan) : base(gridXZ)
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

        public override LinkedList<TCell> FirstTimeFindPath(TCell startCell, TCell endCell, double maxCost = Double.PositiveInfinity)
        {
            _startCell = startCell;
            _endCell = endCell;
            _gValues = new (); 
            _predecessors = new (); 
            _dynamicObstacles = new();

            _gValues[startCell] = 0;

            return FindPath(maxCost);
        }

        public override LinkedList<TCell> UpdatePathWithDynamicObstacle(TCell currentStartCell, List<TCell> foundDynamicObstacles, double maxCost = Double.PositiveInfinity)
        {
            return FindPath(maxCost);
        }

        public override Dictionary<TCell, double> FindAllCellsSmallerThanCost(TCell currentStartCell, double maxCost = Double.PositiveInfinity)
        {
            _startCell = currentStartCell;
            _gValues = new (); 
            _predecessors = new (); 
            _dynamicObstacles = new();
            _gValues[_startCell] = 0;
            
            Priority_Queue.SimplePriorityQueue<TCell, double> openSet = new Priority_Queue.SimplePriorityQueue<TCell, double>();
            HashSet<TCell> visitedSet = new HashSet<TCell>();

            openSet.Enqueue(currentStartCell, currentStartCell.FCost);

            Dictionary<TCell, double> reachableCells = new();

            reachableCells[currentStartCell] = 0;
            
            while (openSet.Count > 0)
            {
                TCell currentCell = openSet.Dequeue();
                
                if ( GetGValue(currentCell) > maxCost)
                    continue;
                
                visitedSet.Add(currentCell);
                reachableCells[currentCell] = GetGValue(currentCell);
                
                foreach (TCell adjacentCell in currentCell.InDegreeCells)
                {
                    //if (visitedSet.Contains(adjacentCell))
                    //    continue;

                    if (!_adjacentCellSelectionFunction.CheckMovableCell(currentCell, adjacentCell))
                        continue;

                    double newGCost = GetGValue(currentCell) + GetDistanceCost(currentCell, adjacentCell);

                    if (newGCost < GetGValue(adjacentCell) || 
                        (!openSet.Contains(adjacentCell) && !visitedSet.Contains(adjacentCell)))
                    {
                        _gValues[adjacentCell] = newGCost;
                        _predecessors[adjacentCell] = currentCell;
                        
                        adjacentCell.GCost = newGCost;
                        adjacentCell.HCost = 0;
                        adjacentCell.FCost = newGCost;
                        adjacentCell.ParentXZCell2D = currentCell;
                        
                        if (!openSet.Contains(adjacentCell))
                        {
                            openSet.Enqueue(adjacentCell, newGCost);
                        }
                    }
                }
            }

            return reachableCells;
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns> the path between start and end</returns>
        private LinkedList<TCell> FindPath(double maxCost = Double.PositiveInfinity)
        {
            Priority_Queue.SimplePriorityQueue<TCell, double> openSet = new (); // to be travelled set
            HashSet<TCell> visitedSet = new(); // travelled set 
            
            openSet.Enqueue(_startCell, _startCell.FCost);
        
            while (openSet.Count > 0)
            {
                TCell currentMinFCostCell = openSet.Dequeue();
                
                if ( GetGValue(currentMinFCostCell) > maxCost)
                    continue;
                
                visitedSet.Add(currentMinFCostCell);
                
                if (currentMinFCostCell == _endCell)
                {
                    return RetracePath(_startCell, _endCell);
                }

                foreach (TCell adjacentCell in currentMinFCostCell.OutDegreeCells)
                {
                    //if (visitedSet.Contains(adjacentCell)) 
                    //    continue;  // skip for travelled cell
                    
                    if (!_adjacentCellSelectionFunction.CheckMovableCell(currentMinFCostCell, adjacentCell)) 
                        continue;
                    
                    
                    double newGCostToNeighbour = GetGValue(currentMinFCostCell) + GetDistanceCost(currentMinFCostCell, adjacentCell);
                    
                    if (newGCostToNeighbour < GetGValue(adjacentCell) || 
                        (!openSet.Contains(adjacentCell) && !visitedSet.Contains(adjacentCell)))
                    {
                        double hCost = GetDistanceCost(adjacentCell, _endCell);
                        double fCost = newGCostToNeighbour + hCost;
                        
                        _gValues[adjacentCell] = newGCostToNeighbour;
                        _predecessors[adjacentCell] = currentMinFCostCell;
                        
                        adjacentCell.GCost = newGCostToNeighbour;
                        adjacentCell.HCost = hCost;
                        adjacentCell.FCost = fCost;
                        adjacentCell.ParentXZCell2D = currentMinFCostCell;

                        if (!openSet.Contains(adjacentCell)) // Not in open set
                        {
                            openSet.Enqueue(adjacentCell, fCost);
                        }
                    }

                }
            }
            //Not found a path to the end
            return null;
        }

        /// <summary>
        /// Get a list of Cell that the pathfinding was found
        /// </summary>
        protected LinkedList<TCell> RetracePath(TCell start, TCell end)
        {
            LinkedList<TCell> path = new();
            TCell currentCell = end;
            while (currentCell != start && currentCell!= null) 
            {
                //Debug.Log("Path "+ currentCell.xIndex +" "+ currentCell.zIndex );
                path.AddFirst(currentCell);
                currentCell = _predecessors[currentCell];
            }
            path.AddFirst(start);
            return path;
        }

        protected virtual double GetDistanceCost(TCell start, TCell end)
        {
            var indexDifferenceAbsolute = Grid.GetIndexDifferenceAbsolute(start,end);

            return _distanceCostFunction.GetDistanceCost(indexDifferenceAbsolute.x, indexDifferenceAbsolute.y) + start.GetAdditionalOutDegreeAdjacentCellCost(end);
        }
        
        private double GetGValue(TCell cell)
        {
            return _gValues.TryGetValue(cell, out double value) ? value : 0;
        }
    
    }
}

