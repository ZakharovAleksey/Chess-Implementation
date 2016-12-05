using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GC = Chess.GameParameters.GameConstants;

namespace Chess.GameFigures
{
    class King : Figure
    {
        #region Construcor

        public King(int indexY, int indexX) : base(indexY, indexX) { }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(@"figures/King_White");
        }

        // Вычисляет позиции куда может пойти пешка
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {
            if (IndexY > 0)
                possibleSteps.Add(new IndexPair(IndexY - 1, IndexX));
            if(IndexY < GC.BoardSize)
                possibleSteps.Add(new IndexPair(IndexY + 1, IndexX));

            if (IndexX > 0)
                possibleSteps.Add(new IndexPair(IndexY, IndexX - 1));
            if (IndexY < GC.BoardSize)
                possibleSteps.Add(new IndexPair(IndexY, IndexX + 1));
        }

        #endregion
    }
}
