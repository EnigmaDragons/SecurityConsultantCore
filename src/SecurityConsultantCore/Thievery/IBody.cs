using SecurityConsultantCore.FacilityObjects;
using SecurityConsultantCore.Pathfinding;
using System;

namespace SecurityConsultantCore.Thievery
{
    public interface IBody
    {
        void BeginMove(Path path, Action callBack);
        void Exit();
        void StealAt(SpatialValuable valuable);
    }
}
