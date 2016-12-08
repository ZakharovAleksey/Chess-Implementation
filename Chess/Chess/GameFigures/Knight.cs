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
    class Knight : Figure
    {
        public Knight(int indexY, int indexX, int color) : base(indexY, indexX, color) { }

        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            if (Color == (int)FigureColor.WHITE)
                Texture = Content.Load<Texture2D>(@"figures/Knight_White");
            else
                Texture = Content.Load<Texture2D>(@"figures/Knight_Black");
        }

        // Проверяет существует ли такой индекс для шахматной доски
        bool IsIndexInChessboard(int IndexY, int IndexX)
        {
            return (IndexX >= 0 && IndexX < GC.BoardSize && IndexY >= 0 && IndexY < GC.BoardSize) ? true : false;
        }

        // Вычисляет позиции куда может пойти конь
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {
            int Y, X;

            for (int curStepId = 0; curStepId < 8; ++curStepId)
            {
                Y = IndexY + stepsY[curStepId];
                X = IndexX + stepsX[curStepId];

                if (IsIndexInChessboard(Y, X) && ( IsCellOtherColor(board, Y,X,this.Color) || IsCellEmpty(board, Y, X)) )
                {
                    possibleSteps.Add(new IndexPair(Y,X));
                }
            }
        }

        public override object Clone()
        {
            Knight clone = new Knight(this.IndexY, this.IndexX, this.Color);
            clone.IsChoosen = this.IsChoosen;

            return clone;
        }

        #endregion

        // Массивы соответствующие индесксы которых задают возможные положения для хода коня
        readonly int[] stepsX = { -2, -1, 1, 2, 2, 1, -1, -2 };
        readonly int[] stepsY = { -1, -2, -2, -1, 1, 2, 2, 1 };
    }
}
