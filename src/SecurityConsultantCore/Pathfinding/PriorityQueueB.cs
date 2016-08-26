using System.Collections.Generic;

namespace SecurityConsultantCore.Pathfinding
{
    public class PriorityQueueB<T> : IPriorityQueue<T>
    {
        protected List<T> InnerList = new List<T>();
        protected IComparer<T> mComparer;

        public PriorityQueueB()
        {
            mComparer = Comparer<T>.Default;
        }

        public PriorityQueueB(IComparer<T> comparer)
        {
            mComparer = comparer;
        }

        public int Count
        {
            get { return InnerList.Count; }
        }

        public T this[int index]
        {
            get { return InnerList[index]; }
            set
            {
                InnerList[index] = value;
                Update(index);
            }
        }

        /// <summary>
        ///     Push an object onto the PQ
        /// </summary>
        /// <param name="O">The new object</param>
        /// <returns>
        ///     The index in the list where the object is _now_. This will change when objects are taken from or put onto the
        ///     PQ.
        /// </returns>
        public int Push(T item)
        {
            int p = InnerList.Count, p2;
            InnerList.Add(item); // E[p] = O
            do
            {
                if (p == 0)
                    break;
                p2 = (p - 1)/2;
                if (OnCompare(p, p2) < 0)
                {
                    SwitchElements(p, p2);
                    p = p2;
                }
                else
                    break;
            } while (true);
            return p;
        }

        /// <summary>
        ///     Get the smallest object and remove it.
        /// </summary>
        /// <returns>The smallest object</returns>
        public T Pop()
        {
            var result = InnerList[0];
            int p = 0, p1, p2, pn;
            InnerList[0] = InnerList[InnerList.Count - 1];
            InnerList.RemoveAt(InnerList.Count - 1);
            do
            {
                pn = p;
                p1 = 2*p + 1;
                p2 = 2*p + 2;
                if ((InnerList.Count > p1) && (OnCompare(p, p1) > 0)) // links kleiner
                    p = p1;
                if ((InnerList.Count > p2) && (OnCompare(p, p2) > 0)) // rechts noch kleiner
                    p = p2;

                if (p == pn)
                    break;
                SwitchElements(p, pn);
            } while (true);

            return result;
        }

        /// <summary>
        ///     Notify the PQ that the object at position i has changed
        ///     and the PQ needs to restore order.
        ///     Since you dont have access to any indexes (except by using the
        ///     explicit IList.this) you should not call this function without knowing exactly
        ///     what you do.
        /// </summary>
        /// <param name="i">The index of the changed object.</param>
        public void Update(int i)
        {
            int p = i, pn;
            int p1, p2;
            do // aufsteigen
            {
                if (p == 0)
                    break;
                p2 = (p - 1)/2;
                if (OnCompare(p, p2) < 0)
                {
                    SwitchElements(p, p2);
                    p = p2;
                }
                else
                    break;
            } while (true);
            if (p < i)
                return;
            do // absteigen
            {
                pn = p;
                p1 = 2*p + 1;
                p2 = 2*p + 2;
                if ((InnerList.Count > p1) && (OnCompare(p, p1) > 0)) // links kleiner
                    p = p1;
                if ((InnerList.Count > p2) && (OnCompare(p, p2) > 0)) // rechts noch kleiner
                    p = p2;

                if (p == pn)
                    break;
                SwitchElements(p, pn);
            } while (true);
        }

        /// <summary>
        ///     Get the smallest object without removing it.
        /// </summary>
        /// <returns>The smallest object</returns>
        public T Peek()
        {
            if (InnerList.Count > 0)
                return InnerList[0];
            return default(T);
        }

        protected void SwitchElements(int i, int j)
        {
            var h = InnerList[i];
            InnerList[i] = InnerList[j];
            InnerList[j] = h;
        }

        protected virtual int OnCompare(int i, int j)
        {
            return mComparer.Compare(InnerList[i], InnerList[j]);
        }

        public void Clear()
        {
            InnerList.Clear();
        }

        public void RemoveLocation(T item)
        {
            var index = -1;
            for (var i = 0; i < InnerList.Count; i++)
                if (mComparer.Compare(InnerList[i], item) == 0)
                    index = i;
            if (index != -1)
                InnerList.RemoveAt(index);
        }
    }
}