
namespace SecurityConsultantCore.Domain.Basic
{
    public class Oriented<T>
    {
        public Oriented(Orientation orientation, T obj)
        {
            Orientation = orientation;
            Obj = obj;
        }

        public T Obj { get; set; }
        public Orientation Orientation { get; set; }
    }
}
