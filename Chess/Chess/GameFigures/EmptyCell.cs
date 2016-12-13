using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Chess.GameFigures
{
    [DataContract]
    class EmptyCell : Figure
    {
        public EmptyCell(int indexY, int indexX) : base(indexY, indexX) { }

        public override object Clone()
        {
            EmptyCell clone = new EmptyCell(this.IndexY, this.IndexX);
            clone.IsChoosen = this.IsChoosen;

            return clone;
        }
    }
}
