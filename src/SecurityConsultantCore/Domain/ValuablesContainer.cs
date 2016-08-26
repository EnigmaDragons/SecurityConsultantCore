using System.Collections.Generic;

namespace SecurityConsultantCore.Domain
{
    public class ValuablesContainer : FacilityObject, IValuablesContainer
    {
        private readonly List<IValuable> _valuables = new List<IValuable>();

        public IEnumerable<IValuable> Valuables => _valuables;

        public void Remove(IValuable valuable)
        {
            _valuables.Remove(valuable);
        }

        public void Put(IValuable valuable)
        {
            _valuables.Add(valuable);
        }
    }
}