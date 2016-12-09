using SecurityConsultantCore.Pathfinding;
using SecurityConsultantCore.PlayerCommands;

namespace SecurityConsultantCore.Test._TestDoubles
{
    public class FakeEngineer : IEngineer
    {
        public PatrolRoute CurrentGuardRoute { get; private set; }

        public void MyPatrolRouteIs(PatrolRoute patrolRoute)
        {
            CurrentGuardRoute = patrolRoute;
        }
    }
}
