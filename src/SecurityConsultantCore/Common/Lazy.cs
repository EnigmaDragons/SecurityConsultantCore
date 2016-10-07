using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Common
{
    public class Lazy<T>
    {
        private readonly Func<T> _evaluationFunc;
        private readonly List<T> _objectContainer;

        public Lazy(Func<T> evaluationFunc)
        {
            _evaluationFunc = evaluationFunc;
            _objectContainer = new List<T>(1);
        }

        public T Get()
        {
            if (!_objectContainer.Any())
                _objectContainer.Add(_evaluationFunc.Invoke());

            return _objectContainer.First();
        }
    }
}