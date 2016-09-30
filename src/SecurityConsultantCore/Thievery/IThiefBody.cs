using System;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Pathfinding;

namespace SecurityConsultantCore.Thievery
{
    public interface IThiefBody
    {
        void BeginShowMoving(Path path, Action callBack);
        void ShowLeavingMap();
        void ShowTakingValuable(XYZObjectLayer valuableLocation);
    }
}
