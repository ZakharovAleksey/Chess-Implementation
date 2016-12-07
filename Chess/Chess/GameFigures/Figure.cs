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
    abstract class Figure : IFigure, ICloneable
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

        // Делает копию текущего положения шахматных фигур на доске
        Figure[,] CloneBoard(Figure[,] board)
        {
            Figure[,] boardClone = new Figure[GC.BoardSize, GC.BoardSize];

            for (int col = 0; col < GC.BoardSize; ++col)
                for (int row = 0; row < GC.BoardSize; ++row)
                {
                    object curType = board[row, col].GetType();
                    if(curType == typeof(EmptyCell))
                        boardClone[row, col] = (EmptyCell) board[row, col].Clone();
                    else if (curType == typeof(Pawn))
                        boardClone[row, col] = (Pawn)board[row, col].Clone();
                    else if (curType == typeof(Knight))
                        boardClone[row, col] = (Knight)board[row, col].Clone();
                    else if (curType == typeof(Rook))
                        boardClone[row, col] = (Rook)board[row, col].Clone();
                    else if (curType == typeof(Bishop))
                        boardClone[row, col] = (Bishop)board[row, col].Clone();
                    else if (curType == typeof(Queen))
                        boardClone[row, col] = (Queen)board[row, col].Clone();
                    else if (curType == typeof(King))
                        boardClone[row, col] = (King)board[row, col].Clone();
                }

            return boardClone;
        }

        bool IsMat(Figure[,] clone)
        {
            // Список всех возможных позийций для всех фигур цвета, что поставили Шах
            List<IndexPair> allPosPositons = new List<IndexPair>();
            int anotherColor = GetAnotherColor();

            int kingIndexX = 0;
            int kingIndexY = 0;
            
            foreach (Figure fig in clone)
            {
                // Записываем в список всех возможных позиций - все возможные позиции для текущей фигуры цвета, что поставили Шах
                if (fig.Color == anotherColor)
                    fig.GetPossiblePositions(allPosPositons, clone);
                // За одно еще ищем позицию короля цвета, которому поставили Шах
                if (fig.Color == Color && fig.GetType() == typeof(King))
                {
                    kingIndexY = fig.IndexY;
                    kingIndexX = fig.IndexX;
                }
            }

            if (allPosPositons.Contains(new IndexPair(kingIndexY, kingIndexX)))
                return true;
            else
                return false;
                



        }

        // Вычисляет все возможные позиции для хода конкретной фигурой в случае Шаха
        public virtual void GetPossiblePositionsInShehCase(List<IndexPair> resPosMoves, Figure[,] board, int selFigIndexY, int selFigIndexX, int ShehMadeFigIndexY, int ShehMadeFigIndexX)
        {
            // Получаем позицию короля
            IndexPair KingPosition = GetKingPosition(board); 

            // Тип фигуры поставившей Шах
            object ShehMadeFigureType = board[ShehMadeFigIndexY, ShehMadeFigIndexX].GetType();




            // Тип фигуры которой мы хотим предотвратить Шах
            object SelFigureType = board[selFigIndexY, selFigIndexX].GetType();
            int selColor = board[selFigIndexY, selFigIndexX].Color;


            List<IndexPair> posStepList = new List<IndexPair>();

            board[selFigIndexY, selFigIndexX].GetPossiblePositions(posStepList, board);

            foreach (IndexPair pos in posStepList)
            {
                Figure[,] clone = CloneBoard(board);

                if (SelFigureType == typeof(Pawn))
                    clone[pos.IndexY, pos.IndexX] = new Pawn(pos.IndexY, pos.IndexX, selColor);
                else if (SelFigureType == typeof(Knight))
                    clone[pos.IndexY, pos.IndexX] = new Knight(pos.IndexY, pos.IndexX, selColor);
                else if (SelFigureType == typeof(Rook))
                    clone[pos.IndexY, pos.IndexX] = new Rook(pos.IndexY, pos.IndexX, selColor);
                else if (SelFigureType == typeof(Bishop))
                    clone[pos.IndexY, pos.IndexX] = new Bishop(pos.IndexY, pos.IndexX, selColor);
                else if (SelFigureType == typeof(Queen))
                    clone[pos.IndexY, pos.IndexX] = new Queen(pos.IndexY, pos.IndexX, selColor);
                else if (SelFigureType == typeof(King))
                    clone[pos.IndexY, pos.IndexX] = new King(pos.IndexY, pos.IndexX, selColor);

                clone[selFigIndexY, selFigIndexX] = new EmptyCell(selFigIndexY, selFigIndexX);

                if (!IsMat(clone))
                    resPosMoves.Add(new IndexPair( pos.IndexY, pos.IndexX));
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

        public virtual object Clone() { return null; }

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
