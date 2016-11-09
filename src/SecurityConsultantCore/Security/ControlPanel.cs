using System.Collections.Generic;

namespace SecurityConsultantCore.Security
{
    public class ControlPanel
    {
        private List<IArmable> _wiredComponents = new List<IArmable>();

        public void WireComponent(IArmable component)
        {
            _wiredComponents.Add(component);
        }

        public void RemoveComponent(IArmable component)
        {
            _wiredComponents.Remove(component);
        }

        public void ArmComponents()
        {
            foreach (var c in _wiredComponents)
            {
                c.Arm();
            }
        }

        public void DisarmComponents()
        {
            foreach (var c in _wiredComponents)
            {
                c.Disarm();
            }
        }
    }
}
