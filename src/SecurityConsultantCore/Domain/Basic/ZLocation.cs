namespace SecurityConsultantCore.Domain.Basic
{
    public class ZLocation<T>
    {
        public ZLocation(int z, T obj)
        {
            Z = z;
            Obj = obj;
        }

        public int Z { get; }
        public T Obj { get; }
    }
}