using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameFigures
{
    class EmptyCell : Figure
    {
        public EmptyCell(int indexY, int indexX) : base(indexY, indexX) { }

        public override object Clone()
        {
            EmptyCell res = new EmptyCell(IndexX, IndexY);          
            return res;
        }

    }
}
