using System;
using System.Collections;
using System.Collections.Generic;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XYRange : IEnumerable<XY>
    {
        private readonly XY _end;
        private readonly XY _start;

        public XYRange(XY start, XY end)
        {
            _start = start;
            _end = end;
        }

        public double MinX => Math.Min(_start.X, _end.X);
        public double MaxX => Math.Max(_start.X, _end.X);
        public double MinY => Math.Min(_start.Y, _end.Y);
        public double MaxY => Math.Max(_start.Y, _end.Y);

        public IEnumerator<XY> GetEnumerator()
        {
            for (var x = MinX; x < MaxX + 1; x++)
                for (var y = MinY; y < MaxY + 1; y++)
                    yield return new XY(x, y);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}