using System.Collections.Generic;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.FacilityObjects;

namespace SecurityConsultantCore.Domain
{
    public class ValuablesContainer : FacilityObject
    {
        private readonly List<IValuable> _valuables = new List<IValuable>();

        public IEnumerable<IValuable> Valuables => _valuables;
        public IEnumerable<Orientation> AccessibleFrom { get; private set; }

        public ValuablesContainer() : this(new List<Orientation> { Orientation.Up, Orientation.Right, Orientation.Left, Orientation.Down }) {}

        public ValuablesContainer(IEnumerable<Orientation> accessibleFrom)
        {
            AccessibleFrom = accessibleFrom;
        }

        public void Put(IValuable valuable)
        {
            _valuables.Add(valuable);
        }

        public void Remove(IValuable valuable)
        {
            _valuables.Remove(valuable);
        }
    }
}
