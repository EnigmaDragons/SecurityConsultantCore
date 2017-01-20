using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    public class TwoDPathFinderFast : I2DPathFinder
    {
        private readonly List<PathFinderNode> _closedNodes = new List<PathFinderNode>();

        // Heap variables are initializated to default, but I like to do it anyway
        private readonly ExpandedPathfindingGrid _grid;
        private readonly PriorityQueueB<int> _openNodes;
        private bool _shouldStop;
        private readonly PathFinderNodeFast[] mCalcGrid;
        private int mCloseNodeCounter;
        private byte mCloseNodeValue = 2;
        private readonly sbyte[,] mDirection = new sbyte[4, 2] {{0, -1}, {1, 0}, {0, 1}, {-1, 0}};
        private int mEndLocation;
        private bool mFound;
        private readonly ushort mGridX;
        private readonly ushort mGridXMinus1;
        private readonly ushort mGridY;
        private readonly ushort mGridYLog2;

        //Promoted local variables to member variables to avoid recreation between calls
        private int mH;
        private int mLocation;
        private ushort mLocationX;
        private ushort mLocationY;
        private int mNewG;
        private int mNewLocation;
        private ushort mNewLocationX;
        private ushort mNewLocationY;
        private byte mOpenNodeValue = 1;

        public TwoDPathFinderFast(ExpandedPathfindingGrid grid)
        {
            if (grid == null)
                throw new Exception("Grid cannot be null");

            _grid = grid;
            mGridX = (ushort) (_grid.GetWidth() + 1);
            mGridY = (ushort) (_grid.GetHeight() + 1);
            mGridXMinus1 = (ushort) (mGridX - 1);
            mGridYLog2 = (ushort) Math.Log(mGridY, 2);

            // This should be done at the constructor, for now we leave it here.
            if ((Math.Log(mGridX, 2) != (int) Math.Log(mGridX, 2)) ||
                (Math.Log(mGridY, 2) != (int) Math.Log(mGridY, 2)))
                throw new Exception("Invalid Grid, size in X and Y must be power of 2");

            if ((mCalcGrid == null) || (mCalcGrid.Length != mGridX*mGridY))
                mCalcGrid = new PathFinderNodeFast[mGridX*mGridY];

            _openNodes = new PriorityQueueB<int>(new ComparePFNodeMatrix(mCalcGrid));
        }

        public void CancelSearch()
        {
            _shouldStop = true;
        }

        public List<PathFinderNode> BeginPathSearch(XY start, XY end)
        {
            lock (this)
            {
                // Is faster if we don't clear the matrix, just assign different values for open and close and ignore the rest
                // I could have user Array.Clear() but using unsafe code is faster, no much but it is.
                //fixed (PathFinderNodeFast* pGrid = tmpGrid) 
                //    ZeroMemory((byte*) pGrid, sizeof(PathFinderNodeFast) * 1000000);

                mFound = false;
                _shouldStop = false;
                mCloseNodeCounter = 0;
                mOpenNodeValue += 2;
                mCloseNodeValue += 2;
                _openNodes.Clear();
                _closedNodes.Clear();

                mLocation = (start.Y << mGridYLog2) + start.X;
                mEndLocation = (end.Y << mGridYLog2) + end.X;
                mCalcGrid[mLocation].G = 0;
                mCalcGrid[mLocation].F = 2;
                mCalcGrid[mLocation].PX = (ushort) start.X;
                mCalcGrid[mLocation].PY = (ushort) start.Y;
                mCalcGrid[mLocation].Status = mOpenNodeValue;

                _openNodes.Push(mLocation);
                while ((_openNodes.Count > 0) && !_shouldStop)
                {
                    mLocation = _openNodes.Pop();

                    //Is it in closed list? means this node was already processed
                    if (mCalcGrid[mLocation].Status == mCloseNodeValue)
                        continue;

                    mLocationX = (ushort) (mLocation & mGridXMinus1);
                    mLocationY = (ushort) (mLocation >> mGridYLog2);

                    if (mLocation == mEndLocation)
                    {
                        mCalcGrid[mLocation].Status = mCloseNodeValue;
                        mFound = true;
                        break;
                    }

                    //Lets calculate each successors
                    for (var i = 0; i < 4; i++)
                    {
                        mNewLocationX = (ushort) (mLocationX + mDirection[i, 0]);
                        mNewLocationY = (ushort) (mLocationY + mDirection[i, 1]);
                        mNewLocation = (mNewLocationY << mGridYLog2) + mNewLocationX;

                        if ((mNewLocationX >= mGridX) || (mNewLocationY >= mGridY))
                            continue;

                        // Unbreakeable?
                        if (_grid[mNewLocationX, mNewLocationY] == 0)
                            continue;

                        mNewG = mCalcGrid[mLocation].G + _grid[mNewLocationX, mNewLocationY];

                        //Is it open or closed?
                        if ((mCalcGrid[mNewLocation].Status == mOpenNodeValue) ||
                            (mCalcGrid[mNewLocation].Status == mCloseNodeValue))
                            if (mCalcGrid[mNewLocation].G <= mNewG)
                                continue;

                        mCalcGrid[mNewLocation].PX = mLocationX;
                        mCalcGrid[mNewLocation].PY = mLocationY;
                        mCalcGrid[mNewLocation].G = mNewG;
                        mH = 2*(Math.Abs(mNewLocationX - end.X) + Math.Abs(mNewLocationY - end.Y));
                        mCalcGrid[mNewLocation].F = mNewG + mH;

                        _openNodes.Push(mNewLocation);

                        mCalcGrid[mNewLocation].Status = mOpenNodeValue;
                    }

                    mCloseNodeCounter++;
                    mCalcGrid[mLocation].Status = mCloseNodeValue;
                }

                if (mFound)
                {
                    _closedNodes.Clear();
                    var posX = end.X;
                    var posY = end.Y;

                    var fNodeTmp = mCalcGrid[(end.Y << mGridYLog2) + end.X];
                    PathFinderNode fNode;
                    fNode.F = fNodeTmp.F;
                    fNode.G = fNodeTmp.G;
                    fNode.H = 0;
                    fNode.PX = fNodeTmp.PX;
                    fNode.PY = fNodeTmp.PY;
                    fNode.X = (int)end.X;
                    fNode.Y = (int)end.Y;

                    while ((fNode.X != fNode.PX) || (fNode.Y != fNode.PY))
                    {
                        _closedNodes.Add(fNode);
                        posX = fNode.PX;
                        posY = fNode.PY;
                        fNodeTmp = mCalcGrid[(posY << mGridYLog2) + posX];
                        fNode.F = fNodeTmp.F;
                        fNode.G = fNodeTmp.G;
                        fNode.H = 0;
                        fNode.PX = fNodeTmp.PX;
                        fNode.PY = fNodeTmp.PY;
                        fNode.X = posX;
                        fNode.Y = posY;
                    }

                    _closedNodes.Add(fNode);

                    return _closedNodes;
                }
                return null;
            }
        }

        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern unsafe bool ZeroMemory(byte* destination, int length);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct PathFinderNodeFast
        {
            public int F; // f = gone + heuristic
            public int G;
            public ushort PX; // Parent
            public ushort PY;
            public byte Status;
        }

        internal class ComparePFNodeMatrix : IComparer<int>
        {
            private readonly PathFinderNodeFast[] mMatrix;

            public ComparePFNodeMatrix(PathFinderNodeFast[] matrix)
            {
                mMatrix = matrix;
            }

            public int Compare(int a, int b)
            {
                if (mMatrix[a].F > mMatrix[b].F)
                    return 1;
                if (mMatrix[a].F < mMatrix[b].F)
                    return -1;
                return 0;
            }
        }
    }
}