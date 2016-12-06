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
    class Queen : Figure
    {
        #region Construcor

        public Queen(int indexY, int indexX, int color) : base(indexY, indexX, color) { }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            if (Color == (int)FigureColor.WHITE)
                Texture = Content.Load<Texture2D>(@"figures/Queen_White");
            else
                Texture = Content.Load<Texture2D>(@"figures/Queen_Black");
        }

        // Вычисляет все возможные позиции для хода королевы как слоном
        void BishopSteps(List<IndexPair> possibleSteps, Figure[,] board)
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
            while (Y < GC.BoardSize && X >= 0)
            {
                if (IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y++, X--));
                else
                    break;
            }

            // Проверяем позиции по направлению в вправо вниз
            Y = IndexY - 1;
            X = IndexX + 1;
            while (Y >= 0 && X < GC.BoardSize)
            {
                if (IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y--, X++));
                else
                    break;
            }

            // Проверяем позиции по направлению в право вверх
            Y = IndexY + 1;
            X = IndexX + 1;
            while (Y < GC.BoardSize && X < GC.BoardSize)
            {
                if (IsCellEmpty(board, Y, X))
                    possibleSteps.Add(new IndexPair(Y++, X++));
                else
                    break;
            }
        }

        // Вычисляет все возможные позиции для хода королевы как ладьей
        void RookSteps(List<IndexPair> possibleSteps, Figure[,] board)
        {
            // Проверем возможные движения вдоль оси X до первой попавшейся фигуры
            int curX = IndexX - 1;
            while (curX >= 0 && IsCellEmpty(board, IndexY, curX))
            {
                possibleSteps.Add(new IndexPair(IndexY, curX--));
            }

            curX = IndexX + 1;
            while (curX < GC.BoardSize && IsCellEmpty(board, IndexY, curX))
            {
                possibleSteps.Add(new IndexPair(IndexY, curX++));
            }

            // Проверем возможные движения вдоль оси Y до первой попавшейся фигуры
            int curY = IndexY - 1;
            while (curY >= 0 && IsCellEmpty(board, curY, IndexX))
            {
                possibleSteps.Add(new IndexPair(curY--, IndexX));
            }

            curY = IndexY + 1;
            while (curY < GC.BoardSize && IsCellEmpty(board, curY, IndexX))
            {
                possibleSteps.Add(new IndexPair(curY++, IndexX));
            }
        }


        // Вычисляет позиции куда может пойти ферзь
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {
            // Находим возможные позиции ходов как для слона
            BishopSteps(possibleSteps, board);
            // Находим возможные позиции ходов как для ладьи
            RookSteps(possibleSteps, board);

        }

        #endregion
    }

}
