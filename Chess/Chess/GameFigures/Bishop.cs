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

        public Bishop(int indexY, int indexX, int color) : base(indexY, indexX, color) { }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            if (Color == (int)FigureColor.WHITE)
                Texture = Content.Load<Texture2D>(@"figures/Bishop_White");
            else
                Texture = Content.Load<Texture2D>(@"figures/Bishop_Black");
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
                else if (IsCellOtherColor(board, Y, X, this.Color))
                {
                    possibleSteps.Add(new IndexPair(Y, X));
                    break;
                }
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
                else if (IsCellOtherColor(board, Y, X, this.Color))
                {
                    possibleSteps.Add(new IndexPair(Y, X));
                    break;
                }
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
                else if (IsCellOtherColor(board, Y, X, this.Color))
                {
                    possibleSteps.Add(new IndexPair(Y, X));
                    break;
                }
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
                else if (IsCellOtherColor(board, Y, X, this.Color))
                {
                    possibleSteps.Add(new IndexPair(Y, X));
                    break;
                }
                else
                    break;
            }
        }

        #endregion
    }
}
