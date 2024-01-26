using UnityEngine;

namespace Shun_Grid_System
{
    public enum PathFindingCostFunction
    {
        Manhattan,
        Euclidean, // Note that when grid is too large, it degrades into Greedy Best-First-Search
        Octile,
        Chebyshev
    }

    public interface IPathFindingDistanceCost
    {
        double GetDistanceCost(int xDifference, int yDifference);
    }

    public class ManhattanDistanceCost : IPathFindingDistanceCost
    {
        public double GetDistanceCost(int xDifference, int yDifference)
        {
            return xDifference + yDifference;
        }
    }
    
    public class EuclideanDistanceCost : IPathFindingDistanceCost
    {
        public double GetDistanceCost(int xDifference, int yDifference)
        {
            return Mathf.Sqrt(Mathf.Pow(xDifference, 2) + Mathf.Pow(yDifference, 2));
        }
    }
    
    public class OctileDistanceCost : IPathFindingDistanceCost
    {
        public double GetDistanceCost(int xDifference, int yDifference)
        {
            return xDifference > yDifference ? 1.4*yDifference+ 1.0*(xDifference-yDifference) : 1.4*xDifference + 1.0*(yDifference-xDifference);
        }
    }
    
    public class ChebyshevDistanceCost : IPathFindingDistanceCost
    {
        public double GetDistanceCost(int xDifference, int yDifference)
        {
            return xDifference > yDifference ? 1.0*yDifference+ 1.0*(xDifference-yDifference) : 1.0*xDifference + 1.0*(yDifference-xDifference);
        }
    }

}