using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    public struct PathFinderNode
    {
        public int F; // f = gone + heuristic
        public int G;
        public int H;
        public int X;
        public int Y;
        public int PX; // Parent
        public int PY;
    }

    public enum PathFinderNodeType
    {
        Start = 1,
        End = 2,
        Open = 4,
        Close = 8,
        Current = 16,
        Path = 32
    }

    public class TwoDPathFinder : I2DPathFinder
    {
        private readonly ExpandedPathfindingGrid _grid;
        private bool _shouldStop;
        private readonly List<PathFinderNode> mClose = new List<PathFinderNode>();
        private readonly PriorityQueueB<PathFinderNode> mOpen = new PriorityQueueB<PathFinderNode>(new ComparePFNode());

        public TwoDPathFinder(ExpandedPathfindingGrid grid)
        {
            if (grid == null)
                throw new Exception("Grid cannot be null");
            _grid = grid;
        }

        public void CancelSearch()
        {
            _shouldStop = true;
        }

        public List<PathFinderNode> BeginPathSearch(XY start, XY end)
        {
            var found = false;
            var gridX = _grid.GetWidth();
            var gridY = _grid.GetHeight();

            _shouldStop = false;
            mOpen.Clear();
            mClose.Clear();

            var direction = new sbyte[,] {{0, -1}, {1, 0}, {0, 1}, {-1, 0}};

            PathFinderNode parentNode;
            parentNode.G = 0;
            parentNode.H = 2;
            parentNode.F = parentNode.G + parentNode.H;
            parentNode.X = start.XInt;
            parentNode.Y = start.YInt;
            parentNode.PX = parentNode.X;
            parentNode.PY = parentNode.Y;
            mOpen.Push(parentNode);
            while ((mOpen.Count > 0) && !_shouldStop)
            {
                parentNode = mOpen.Pop();

                if ((parentNode.X == end.X) && (parentNode.Y == end.Y))
                {
                    mClose.Add(parentNode);
                    found = true;
                    break;
                }

                //Lets calculate each successors
                for (var i = 0; i < 4; i++)
                {
                    PathFinderNode newNode;
                    newNode.X = parentNode.X + direction[i, 0];
                    newNode.Y = parentNode.Y + direction[i, 1];

                    if ((newNode.X < 0) || (newNode.Y < 0) || (newNode.X >= gridX) || (newNode.Y >= gridY))
                        continue;

                    var newG = parentNode.G + _grid[newNode.X, newNode.Y];

                    if (newG == parentNode.G)
                        continue;

                    var foundInOpenIndex = -1;
                    for (var j = 0; j < mOpen.Count; j++)
                        if ((mOpen[j].X == newNode.X) && (mOpen[j].Y == newNode.Y))
                        {
                            foundInOpenIndex = j;
                            break;
                        }
                    if ((foundInOpenIndex != -1) && (mOpen[foundInOpenIndex].G <= newG))
                        continue;

                    var foundInCloseIndex = -1;
                    for (var j = 0; j < mClose.Count; j++)
                        if ((mClose[j].X == newNode.X) && (mClose[j].Y == newNode.Y))
                        {
                            foundInCloseIndex = j;
                            break;
                        }
                    if (foundInCloseIndex != -1)
                        continue;

                    newNode.PX = parentNode.X;
                    newNode.PY = parentNode.Y;
                    newNode.G = newG;
                    newNode.H = 2*(Math.Abs(newNode.X - end.XInt) + Math.Abs(newNode.Y - end.YInt));
                    newNode.F = newNode.G + newNode.H;

                    mOpen.Push(newNode);
                }

                mClose.Add(parentNode);
            }

            if (found)
            {
                var fNode = mClose[mClose.Count - 1];
                for (var i = mClose.Count - 1; i >= 0; i--)
                    if (((fNode.PX == mClose[i].X) && (fNode.PY == mClose[i].Y)) || (i == mClose.Count - 1))
                        fNode = mClose[i];
                    else
                        mClose.RemoveAt(i);
                return mClose;
            }
            return null;
        }

        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern unsafe bool ZeroMemory(byte* destination, int length);

        internal class ComparePFNode : IComparer<PathFinderNode>
        {
            public int Compare(PathFinderNode x, PathFinderNode y)
            {
                if (x.F > y.F)
                    return 1;
                if (x.F < y.F)
                    return -1;
                return 0;
            }
        }
    }
}