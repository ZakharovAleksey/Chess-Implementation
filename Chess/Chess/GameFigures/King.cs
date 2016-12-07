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

        public King(int indexY, int indexX, int color) : base(indexY, indexX, color) { }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            if (Color == (int)FigureColor.WHITE)
                Texture = Content.Load<Texture2D>(@"figures/King_White");
            else
                Texture = Content.Load<Texture2D>(@"figures/King_Black");
        }

        // Для Короля данного цвета считает все позиции которые бьются фигурами другого цвета
        void CalculateAllBittenPositions(List<IndexPair> bittenPos, Figure[,] board)
        {
            // Получаем другой цвет
            int anotherColor = GetAnotherColor();

            // Создаем список который хранит все позиции которые бьются каждой из выбранных фигур
            foreach (Figure figure in board)
            {
                if (figure.Color == anotherColor && figure.GetType() != typeof(King))
                {
                    figure.GetPossiblePositions(bittenPos, board);
                }
            }
        }

        // Вычисляет позиции куда может пойти король
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {
            // Находим все позиции которые бьются фигурами другого цвета
            List<IndexPair> bittenPos = new List<IndexPair>();
            CalculateAllBittenPositions(bittenPos, board);

            int Y, X;

            // Ход влево
            Y = IndexY;
            X = IndexX - 1;
            if (X >= 0)
            {
                if((IsCellEmpty(board, Y, X) || IsCellOtherColor(board, Y,X, this.Color)) && !bittenPos.Contains(new IndexPair(Y,X)) )
                    possibleSteps.Add(new IndexPair(Y, X));
            }

            // Ход вправо
            Y = IndexY;
            X = IndexX + 1;
            if (X < GC.BoardSize)
            {
                if ((IsCellEmpty(board, Y, X) || IsCellOtherColor(board, Y, X, this.Color)) && !bittenPos.Contains(new IndexPair(Y, X)) )
                    possibleSteps.Add(new IndexPair(Y, X));
            }

            // Ход вверх
            Y = IndexY - 1;
            X = IndexX;
            if (Y >= 0)
            {
                if ((IsCellEmpty(board, Y, X) || IsCellOtherColor(board, Y, X, this.Color)) && !bittenPos.Contains(new IndexPair(Y, X)))
                    possibleSteps.Add(new IndexPair(Y, X));
            }

            // Ход вниз
            Y = IndexY + 1;
            X = IndexX;
            if (Y < GC.BoardSize)
            {
                if ((IsCellEmpty(board, Y, X) || IsCellOtherColor(board, Y, X, this.Color)) && !bittenPos.Contains(new IndexPair(Y, X)))
                    possibleSteps.Add(new IndexPair(Y, X));
            }
        }

        #endregion
    }
}
