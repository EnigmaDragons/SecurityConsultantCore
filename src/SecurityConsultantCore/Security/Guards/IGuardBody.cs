using System;
using SecurityConsultantCore.Pathfinding;

namespace SecurityConsultantCore.Security.Guards
{
    public interface IGuardBody
    {
        void BeginMoving(Path path, Action callBack);
    }
}
