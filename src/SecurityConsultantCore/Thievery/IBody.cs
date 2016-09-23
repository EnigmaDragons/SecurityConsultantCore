using System;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Pathfinding;

namespace SecurityConsultantCore.Thievery
{
    public interface IBody
    {
        void BeginMove(Path path, Action callBack);
        void Exit();
        void StealAt(SpatialValuable valuable);
    }
}
