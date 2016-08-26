using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Factories
{
    public abstract class Container<T>
    {
        private Dictionary<string, T> _objects;
        private Dictionary<string, T> Objects => _objects ?? (_objects = GetObjects());

        public T Create(string id)
        {
            var key = GetKey(id);
            if (string.IsNullOrEmpty(key) || !Objects.ContainsKey(key))
                throw new KeyNotFoundException("Unknown object type: \"" + key + "\"");
            return Objects[key];
        }

        public List<string> GetConstructables()
        {
            return Objects.Select(x => x.Key).ToList();
        }

        protected abstract string GetKey(string id);
        protected abstract Dictionary<string, T> GetObjects();
    }
}