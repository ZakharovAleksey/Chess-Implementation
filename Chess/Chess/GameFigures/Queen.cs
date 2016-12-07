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
            while (Y < GC.BoardSize && X >= 0)
            {
                if (IsCellEmpty(board, Y, X))
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
                if (IsCellEmpty(board, Y, X))
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
                if (IsCellEmpty(board, Y, X))
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

        // Вычисляет все возможные позиции для хода королевы как ладьей
        void RookSteps(List<IndexPair> possibleSteps, Figure[,] board)
        {
            // Проверем возможные движения вдоль оси X в право до первой попавшейся фигуры 
            int curX = IndexX - 1;
            while (curX >= 0 && IsCellEmpty(board, IndexY, curX))
            {
                possibleSteps.Add(new IndexPair(IndexY, curX--));
            }

            // Если первая попавшаяся фигура при движении вправо вдоль оси X вправо другого цвета то мы можем ее бить
            if (curX >= 0)
                if (IsCellOtherColor(board, IndexY, curX, this.Color))
                    possibleSteps.Add(new IndexPair(IndexY, curX));

            // Проверем возможные движения вдоль оси X в лево до первой попавшейся фигуры 
            curX = IndexX + 1;
            while (curX < GC.BoardSize && IsCellEmpty(board, IndexY, curX))
            {
                possibleSteps.Add(new IndexPair(IndexY, curX++));
            }

            // Если первая попавшаяся фигура при движении влево  вдоль оси X другого цвета то мы можем ее бить
            if (curX < GC.BoardSize - 1)
                if (IsCellOtherColor(board, IndexY, curX, this.Color))
                    possibleSteps.Add(new IndexPair(IndexY, curX));


            // Проверем возможные движения вдоль оси Y вверх до первой попавшейся фигуры
            int curY = IndexY - 1;
            while (curY >= 0 && IsCellEmpty(board, curY, IndexX))
            {
                possibleSteps.Add(new IndexPair(curY--, IndexX));
            }

            // Если первая попавшаяся фигура при движении влево  вдоль оси X другого цвета то мы можем ее бить
            if (curY >= 0)
                if (IsCellOtherColor(board, curY, IndexX, this.Color))
                    possibleSteps.Add(new IndexPair(curY, IndexX));

            // Проверем возможные движения вдоль оси Y вниз до первой попавшейся фигуры
            curY = IndexY + 1;
            while (curY < GC.BoardSize && IsCellEmpty(board, curY, IndexX))
            {
                possibleSteps.Add(new IndexPair(curY++, IndexX));
            }
            // Если первая попавшаяся фигура при движении влево  вдоль оси X другого цвета то мы можем ее бить
            if (curY < GC.BoardSize - 1)
                if (IsCellOtherColor(board, curY, IndexX, this.Color))
                    possibleSteps.Add(new IndexPair(curY, IndexX));
        }


        // Вычисляет позиции куда может пойти ферзь
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {
            // Находим возможные позиции ходов как для слона
            BishopSteps(possibleSteps, board);
            // Находим возможные позиции ходов как для ладьи
            RookSteps(possibleSteps, board);

        }


        public override object Clone()
        {
            Queen clone = new Queen(this.IndexY, this.IndexX, this.Color);
            clone.IsChoosen = this.IsChoosen;

            return clone;
        }

        #endregion
    }

}
