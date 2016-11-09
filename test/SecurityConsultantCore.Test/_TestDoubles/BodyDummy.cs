using System;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Pathfinding;
using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Test._TestDoubles
{
    public class BodyDummy : IBody
    {
        public void BeginMove(Path path, Action callBack)
        {
            callBack();
        }

        public void Exit()
        {
        }

        public void StealAt(SpatialValuable valuable)
        {
        }
    }
}