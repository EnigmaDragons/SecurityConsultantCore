using System.Linq;

namespace SecurityConsultantCore.Spatial
{
    public class Volume
    {
        private bool[,] _occupiedSpaces;

        public Volume(bool[,] occupiedSpaces)
        {
            _occupiedSpaces = occupiedSpaces;
        }

        public bool this[int x, int y] => _occupiedSpaces[x, y];

        public override bool Equals(object obj)
        {
            var other = obj as Volume;
            if (other == null) return false;
            return Equals(other);
        }

        public bool Equals(Volume other)
        {
            return _occupiedSpaces.Rank == other._occupiedSpaces.Rank &&
                Enumerable.Range(0, _occupiedSpaces.Rank).All(dimension => _occupiedSpaces.GetLength(dimension) == other._occupiedSpaces.GetLength(dimension)) &&
                _occupiedSpaces.Cast<bool>().SequenceEqual(other._occupiedSpaces.Cast<bool>());
        }
    }
}
