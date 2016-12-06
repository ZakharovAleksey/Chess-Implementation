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
    class Bishop : Figure
    {
        #region Construcor

        public Bishop(int indexY, int indexX) : base(indexY, indexX) { }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(@"figures/Bishop_White");
        }

        // Вычисляет позиции куда может пойти слон
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {
            int Y, X;

            // Проверяем позиции по направлению в лево вверх
            Y = IndexY - 1;
            X = IndexX - 1;
            while (Y >= 0 && X >= 0)
            {
                if (IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y--, X--));
                else
                    break;
            }

            // Проверяем позиции по направлению в лево вниз
            Y = IndexY + 1;
            X = IndexX - 1;
            while (Y <GC.BoardSize && X >= 0)
            {
                if(IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y++, X--));
                else
                    break;
            }

            // Проверяем позиции по направлению в вправо вниз
            Y = IndexY - 1;
            X = IndexX + 1;
            while (Y >= 0 && X < GC.BoardSize)
            {
                if(IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y--, X++));
                else
                    break;
            }

            // Проверяем позиции по направлению в право вверх
            Y = IndexY + 1;
            X = IndexX + 1;
            while (Y < GC.BoardSize && X < GC.BoardSize)
            {
                if(IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y++, X++));
                else
                    break;
            }
        }

        #endregion
    }
}
