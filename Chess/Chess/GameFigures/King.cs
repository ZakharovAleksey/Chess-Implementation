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

        // Вычисляет позиции куда может пойти король
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {
            int Y, X;

            // Ход влево
            Y = IndexY;
            X = IndexX - 1;
            if (X >= 0)
            {
                if(IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y, X));
            }

            // Ход вправо
            Y = IndexY;
            X = IndexX + 1;
            if (X < GC.BoardSize)
            {
                if (IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y, X));
            }

            // Ход вверх
            Y = IndexY - 1;
            X = IndexX;
            if (Y >= 0)
            {
                if (IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y, X));
            }

            // Ход вниз
            Y = IndexY + 1;
            X = IndexX;
            if (Y < GC.BoardSize)
            {
                if (IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y, X));
            }
        }

        #endregion
    }
}
