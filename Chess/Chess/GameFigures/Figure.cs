using Microsoft.Xna.Framework;
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
    // Тип Текущей фигуры
    enum FigureColor
    {
        WHITE = 0,
        BLACK = 1,
    }


    // Базовый абстрактный класс для всех фигур на шахматной доске.
    abstract class Figure : IFigure
    {
        // Задает начальное положение для фигуры
        public Figure(int indexY, int indexX)
        {
            this.IndexY = indexY;
            this.IndexX = indexX;
        }

        public Figure(int indexY, int indexX, int color) : this(indexY, indexX)
        {
            this.Color = color;
        }

        public virtual void LoadContent(ContentManager Content) { }

        // Отрисовывает фигуру в зависимости от ее положения на доске
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                Rectangle drawPos = new Rectangle(GC.IndentLeft + IndexX * GC.CellHeight, GC.IndentTop + IndexY * GC.CellWidth, GC.CellWidth, GC.CellHeight);
                spriteBatch.Draw(Texture, drawPos, Microsoft.Xna.Framework.Color.White);
            }
        }

        // Вычисляет все возможные позикии для хода
        public virtual void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board) { }


        #region Sheh Case methods

        // Ищем позицию Короля того же цвета
        protected IndexPair GetKingPosition(Figure[,] board)
        {
            foreach (Figure figure in board)
            {
                object ft = figure.GetType();
                if (figure.Color == this.Color && figure.GetType() == typeof(King))
                {
                    return new IndexPair(figure.IndexY, figure.IndexX);
                }
            }

            return new IndexPair();
        }

        // Вычисляет все возможные позиции для хода конкретной фигурой в случае Шаха
        public virtual void GetPossiblePositionsInShehCase(List<IndexPair> resPosMoves, Figure[,] board, int selFigIndexY, int selFigIndexX, int ShehMadeFigIndexY, int ShehMadeFigIndexX)
        {
            // Получаем позицию короля
            IndexPair KingPosition = GetKingPosition(board);

            // 

            // Тип фигуры поставившей Шах
            object ShehMadeFigureType = board[ShehMadeFigIndexY, ShehMadeFigIndexX].GetType();
            // Тип фигуры которой мы хотим предотвратить Шах
            object SelFigureType = board[selFigIndexY, selFigIndexX].GetType();


            // Если мы поставили шаг Конем: то сходить пешкой мы можем только если съедим Коня
            if (ShehMadeFigureType == typeof(Knight) && SelFigureType == typeof(King))
            {
                board[KingPosition.IndexY, KingPosition.IndexX].GetPossiblePositions(resPosMoves, board);
            }
            else if (ShehMadeFigureType == typeof(Knight))
            {
                // Записываем возможные шаги 
                List<IndexPair> lol = new List<IndexPair>();
                board[selFigIndexY, selFigIndexX].GetPossiblePositions(lol, board);

                if (lol.Contains(new IndexPair(ShehMadeFigIndexY, ShehMadeFigIndexX)))
                    resPosMoves.Add(new IndexPair(ShehMadeFigIndexY, ShehMadeFigIndexX));
            }
            



        }

        #endregion

        // Проверяет пуста ли клетка с указанными индексами
        protected bool IsCellEmpty(Figure[,] board, int IndexY, int IndexX)
        {
            return (board[IndexY, IndexX].GetType() == typeof(EmptyCell)) ? true : false;
        }

        // Возвращает цвет противоположному у выбранной фигуры
        protected int GetAnotherColor()
        {
            return (this.Color == (int)FigureColor.WHITE) ? (int)FigureColor.BLACK : (int)FigureColor.WHITE;
        }

        // Проверяет противополжный ли цвет у фигуры с указанными индексами, переданному цвету
        protected bool IsCellOtherColor(Figure[,] board, int IndexY, int IndexX, int color)
        {
            int anotherColor = (color == (int)FigureColor.WHITE) ? (int)FigureColor.BLACK : (int)FigureColor.WHITE;

            return (!IsCellEmpty(board, IndexY, IndexX) && board[IndexY, IndexX].Color == anotherColor) ? true : false;
        }

        #region Properties

        // y и x координата положения фигуры на доске соответственно
        public int IndexY { get; set; }
        public int IndexX { get; set; }

        // Показывает выбрана ли данная фигура на текущий момент пользователем
        protected bool IsChoosen { get; set; } = false;
        // Текстура для фигуры
        protected Texture2D Texture { get; set; } = null;

        public int Color { get; set; }

        #endregion
    }
}
