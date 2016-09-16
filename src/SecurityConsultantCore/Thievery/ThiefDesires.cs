using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Thievery
{
    public class ThiefDesires : IDesires
    {
        private readonly Predicate<IValuable> _criteria;
        private readonly Preference _preference;
        private readonly IEnumerable<LocatedValuable> _valuables;

        public ThiefDesires(IEnumerable<LocatedValuable> valuables)
            : this(valuables, x => true, new PreferenceNone()) { }

        public ThiefDesires(IEnumerable<LocatedValuable> valuables, Preference preference) 
            : this(valuables, x => true, preference) { }

        public ThiefDesires(IEnumerable<LocatedValuable> valuables, Predicate<IValuable> criteria) 
            : this(valuables, criteria, new PreferenceNone())  { }

        public ThiefDesires(IEnumerable<LocatedValuable> valuables, Predicate<IValuable> criteria, Preference preference)
        {
            _criteria = criteria;
            _valuables = valuables;
            _preference = preference;
        }

        public IEnumerable<LocatedValuable> Get()
        {
            return _valuables.Where(x => _criteria.Invoke(x.Obj)).OrderByDescending(x => x.Obj, _preference);
        }
    }
}
