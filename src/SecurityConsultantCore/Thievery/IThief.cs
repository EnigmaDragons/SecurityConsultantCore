using System;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Pathfinding;

namespace SecurityConsultantCore.Thievery
{
    public interface IThief
    {
        void BeginTraverse(Path path, Action callBack);
        void Exit();
        void Steal(XYZObjectLayer valuableLocation);
    }
}
