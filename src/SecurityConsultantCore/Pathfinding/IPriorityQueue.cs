﻿namespace SecurityConsultantCore.Pathfinding
{
    public interface IPriorityQueue<T>
    {
        int Push(T item);
        T Pop();
        T Peek();
        void Update(int i);
    }
}