using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Chess.GameParameters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Chess.GameFigures;

using GC = Chess.GameParameters.GameConstants;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Chess.GameUnits
{
    class ChessBoard
    {
        /*
         * Надо добавить:
         * IsChoosen - показывает выбрана ли какая либо фигура
         * ChosenIndexX
         * ChosenIndexY
         * -> когда пытаемся нажать левой кнопкой второй раз на выбранную позицию.
         *  - list = board[chy,chX].return list of possible values.
         *  - пробегаемся по выбраным id и смотрим заняты ли они!
         *  - получаем окончательный вид list
         *  - если выбрана возможная ячейка то тогда идем в нее, в противном случае нет!
         * */

        public ChessBoard()
        {
            // Board declaration
            for (int rowID = 0; rowID < GC.BoardSize; ++rowID)
            {
                int curColor = (rowID % 2 == 0) ? (int)CellType.WHITE : (int)CellType.BLACK;
                for (int columnID = 0; columnID < GC.BoardSize; ++columnID)
                {
                    Board[rowID, columnID] = new Cell(rowID, columnID, curColor);
                    curColor = (curColor == (int)CellType.BLACK) ? (int)CellType.WHITE : (int)CellType.BLACK;

                    // Load chess content
                    // Подгружаем пешки
                    if (rowID == GC.BoardSize - 2)
                        FigureBoard[rowID, columnID] = new Pawn(rowID, columnID, (int) FigureColor.WHITE);
                    else if (rowID == 1)
                        FigureBoard[rowID, columnID] = new Pawn(rowID, columnID, (int)FigureColor.BLACK);
                    // Подгружаем коней
                    else if (rowID == GC.BoardSize - 1 && ( columnID == 1 || columnID == GC.BoardSize - 2) )
                        FigureBoard[rowID, columnID] = new Knight(rowID, columnID, (int)FigureColor.WHITE);
                    else if (rowID == 0 && (columnID == 1 || columnID == GC.BoardSize - 2))
                        FigureBoard[rowID, columnID] = new Knight(rowID, columnID, (int)FigureColor.BLACK);
                    // Подгружаем ладей
                    else if (rowID == GC.BoardSize - 1 && (columnID == 0 || columnID == GC.BoardSize - 1))
                        FigureBoard[rowID, columnID] = new Rook(rowID, columnID, (int)FigureColor.WHITE);
                    else if (rowID == 0 && (columnID == 0 || columnID == GC.BoardSize - 1))
                        FigureBoard[rowID, columnID] = new Rook(rowID, columnID, (int)FigureColor.BLACK);
                    // Подгружаем слонов
                    else if (rowID == GC.BoardSize - 1 && (columnID == 2 || columnID == GC.BoardSize - 3))
                        FigureBoard[rowID, columnID] = new Bishop(rowID, columnID, (int)FigureColor.WHITE);
                    else if (rowID == 0 && (columnID == 2 || columnID == GC.BoardSize - 3))
                        FigureBoard[rowID, columnID] = new Bishop(rowID, columnID, (int)FigureColor.BLACK);
                    // Подгружаем ферзей
                    else if (rowID == GC.BoardSize - 1 && columnID == 4)
                        FigureBoard[rowID, columnID] = new Queen(rowID, columnID, (int)FigureColor.WHITE);
                    else if (rowID == 0 && columnID == 4)
                        FigureBoard[rowID, columnID] = new Queen(rowID, columnID, (int)FigureColor.BLACK);
                    // Подгружаем королей
                    else if (rowID == GC.BoardSize - 1 && columnID == 3)
                        FigureBoard[rowID, columnID] = new King(rowID, columnID, (int)FigureColor.WHITE);
                    else if (rowID == 0 && columnID == 3)
                        FigureBoard[rowID, columnID] = new King(rowID, columnID, (int)FigureColor.BLACK);
                    else
                        FigureBoard[rowID, columnID] = new EmptyCell(rowID, columnID);
                }
            }
        }

        #region Methods

        #region Update 
        
        // Проверяет попадает ли клик пользователя в область шахматной доски
        bool IsClickInChessboard(MouseState curMouseState)
        {
            return (curMouseState.Position.X >= GC.IndentLeft && curMouseState.Position.X <= GC.IndentRight
                && curMouseState.Y >= GC.IndentTop && curMouseState.Y <= GC.IndentBottom) ? true : false;
        }

        // По нажатию на кнопку мыши берет индекс позиции
        IndexPair GetChosenCellIndex(MouseState curMouseState)
        {
            int Y = (curMouseState.Position.Y - GC.IndentTop) / GC.CellHeight;
            int X = (curMouseState.Position.X - GC.IndentLeft) / GC.CellWidth;

            return new IndexPair(Y, X);
        }

        // Проверяет является ли ячейка с переданными индексами пустой
        bool IsCellEmpty(int indexY, int indexX)
        {
            object emptyCellType = typeof(EmptyCell);
            object selectedCellType = FigureBoard[indexY, indexX].GetType();

            return (selectedCellType == emptyCellType) ? true : false;
        }

        #region On left button click

        #region First click on left button actions

        // Если пользователь выбрал первым щелчком ячейку без шахматной фигуры то как будло щелчка не было
        void SkipSelectedCell()
        {
            // Указываем что стартовая позиция не определена
            StartMoveIndexX = -1;
            StartMoveIndexY = -1;

            // Пользователь нажал на пустую клетку - следовательно не выбрал фигуру для своего хода
            IsFigureChosenForStep = false;
        }

        void SetStartMoveCell(IndexPair selectPos)
        {
            // Присваиваем стартовой позиции индексы, соответствующие клику игрока
            StartMoveIndexY = selectPos.IndexY;
            StartMoveIndexX = selectPos.IndexX;

            Board[StartMoveIndexY, StartMoveIndexX].SetStateSELECT();

            // Говорим что пользователь действительно выбрал клетку с фигурой [чтобы сделать этой фигурой шаг]
            IsFigureChosenForStep = true;
        }

        // Обеспечивает логику первого клика на левую кнопку мыши
        void FirstClickOnLeftButtonActions(MouseState curMouseState, int selFigureColor)
        {
            // Переменная хранит выбранную на данный момент пользователем позицию
            IndexPair selectPos = GetChosenCellIndex(curMouseState);

            if ((IsWhiteMove && FigureBoard[selectPos.IndexY, selectPos.IndexX].Color == selFigureColor) || (IsBlackMove && FigureBoard[selectPos.IndexY, selectPos.IndexX].Color == selFigureColor))
            {
                if (IsCellEmpty(selectPos.IndexY, selectPos.IndexX))
                    SkipSelectedCell();
                else
                {
                    SetStartMoveCell(selectPos);
                    prevMouseState = curMouseState;
                }
            }
        }

        #endregion

        #region Second click on left button actions

        void SecondClickOnLeftButtonActions(MouseState curMouseState, int figureColor)
        {
            // Находим все доступные для выбранной фигуры позиции для хода
            List<IndexPair> figPosMoves = new List<IndexPair>();
            FigureBoard[StartMoveIndexY, StartMoveIndexX].GetPossiblePositions(figPosMoves, this.FigureBoard);

            // Индексы клетки куда пользователь хочет сделать ход
            IndexPair selectPos = GetChosenCellIndex(curMouseState);

            // Если пользователь нажал на ячейку в которую можно сходить данной фигурой
            if (figPosMoves.Contains(selectPos))
            {
                // Задаем конечную позицию для хода
                EndMoveIndexY = selectPos.IndexY;
                EndMoveIndexX = selectPos.IndexX;

                // Выполняем ход 
                // Определяем тип выбранной фигуры и ее цвет
                object selFigureType = FigureBoard[StartMoveIndexY, StartMoveIndexX].GetType();
                int selFigureColor = FigureBoard[StartMoveIndexY, StartMoveIndexX].Color;

                // Клетка в которую мы сходим теперь будет содержать фигуру, которой сделан ход
                if (selFigureType == typeof(Pawn))
                    FigureBoard[EndMoveIndexY, EndMoveIndexX] = new Pawn(EndMoveIndexY, EndMoveIndexX, selFigureColor);
                else if (selFigureType == typeof(Knight))
                    FigureBoard[EndMoveIndexY, EndMoveIndexX] = new Knight(EndMoveIndexY, EndMoveIndexX, selFigureColor);
                else if (selFigureType == typeof(Rook))
                    FigureBoard[EndMoveIndexY, EndMoveIndexX] = new Rook(EndMoveIndexY, EndMoveIndexX, selFigureColor);
                else if (selFigureType == typeof(Bishop))
                    FigureBoard[EndMoveIndexY, EndMoveIndexX] = new Bishop(EndMoveIndexY, EndMoveIndexX, selFigureColor);
                else if (selFigureType == typeof(Queen))
                    FigureBoard[EndMoveIndexY, EndMoveIndexX] = new Queen(EndMoveIndexY, EndMoveIndexX, selFigureColor);
                else if (selFigureType == typeof(King))
                    FigureBoard[EndMoveIndexY, EndMoveIndexX] = new King(EndMoveIndexY, EndMoveIndexX, selFigureColor);

                // Клетка из которой мы ходили теперь пустая
                FigureBoard[StartMoveIndexY, StartMoveIndexX] = new EmptyCell(StartMoveIndexY, StartMoveIndexX);

                // Говорим что пользователь сделал ход (Нужно будет для отрисовки)
                IsFigureMadeStep = true;

                prevMouseState = curMouseState;

                // Если нам удалось сходить одним из двух игроков, то тогда делаем доступным ход второго игрока
                if (figureColor == (int)FigureColor.WHITE)
                {
                    IsWhiteMove = false;
                    IsBlackMove = true;
                }
                else if (figureColor == (int)FigureColor.BLACK)
                {
                    IsWhiteMove = true;
                    IsBlackMove = false;
                }
            }
        }

        #endregion

        void ClickOnLeftButtonActions(MouseState curMouseState, GameTime gameTime, int figureColor)
        {
            if (curMouseState.LeftButton == ButtonState.Pressed)
            {
                if (!IsFigureChosenForStep)
                {
                    // Сделано чтобы при нажатии два раза мыши по одной позиции она не загаралась как выделенная
                    if (prevMouseState != curMouseState)
                        FirstClickOnLeftButtonActions(curMouseState, figureColor);
                }
                else if (IsFigureChosenForStep)
                {
                    SecondClickOnLeftButtonActions(curMouseState, figureColor);
                }
            }
        }

        #endregion

        #region On right button click

        void SetCellUnselect()
        {
            if (Board[StartMoveIndexY, StartMoveIndexX].IsSelect)
            {
                Board[StartMoveIndexY, StartMoveIndexX].SetStateIDLE();
                IsFigureChosenForStep = false;
            }
        }

        void ClickOnRightButtonActions(MouseState curMouseState)
        {
            if (curMouseState.RightButton == ButtonState.Pressed)
            {
                // Если уже выбрана фигура для хода то тогда отменяем ее выбор
                if (IsFigureChosenForStep)
                {
                    SetCellUnselect();
                    prevMouseState = curMouseState;
                }
            }
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            MouseState curMouseState = Mouse.GetState();

            TimeBetweenLeftBTNClick += gameTime.ElapsedGameTime.Milliseconds;

            if (IsClickInChessboard(curMouseState))
            {
                if (IsWhiteMove)
                {
                    ClickOnLeftButtonActions(curMouseState, gameTime, (int)FigureColor.WHITE);
                    ClickOnRightButtonActions(curMouseState);
                }
                else if (IsBlackMove)
                {
                    ClickOnLeftButtonActions(curMouseState, gameTime, (int)FigureColor.BLACK);
                    ClickOnRightButtonActions(curMouseState);
                }

                // Таймер установлен так как мышь делает двойное нажатие (почему то!!) И поэтому после хода сразу выполняется функция выбрать ячейку
                //ClickOnLeftButtonActions(curMouseState, gameTime, (int)FigureColor.WHITE);
                //ClickOnRightButtonActions(curMouseState);
            }

        }

        #endregion

        public void LoadContent(ContentManager Content)
        {
            foreach (Cell cell in Board)
                cell.LoadContent(Content);

            // Load pawn content
            foreach (Figure fig in FigureBoard)
                fig.LoadContent(Content);

        }

        public void Draw(SpriteBatch spriteBatch, ContentManager Content)
        {
            if (IsFigureMadeStep)
            {
                FigureBoard[StartMoveIndexY, StartMoveIndexX].LoadContent(Content);
                FigureBoard[EndMoveIndexY, EndMoveIndexX].LoadContent(Content);

                // Пользователь сделал ход - обнуляем все параметры
                IsFigureChosenForStep = false;
                IsFigureMadeStep = false;

                //Говорим что стартовая клетка и конечная теперь пустые
                Board[StartMoveIndexY, StartMoveIndexX].SetStateIDLE();
                Board[EndMoveIndexY, EndMoveIndexX].SetStateIDLE();
            }

            foreach (Cell cell in Board)
                cell.Draw(spriteBatch);

            foreach (Figure fig in FigureBoard)
                fig.Draw(spriteBatch);
        }

        #endregion

        #region Fields

        // Матрица показывающая выбрана ли клетка или нет
        Cell[,] Board { get; set; } = new Cell[GC.BoardSize, GC.BoardSize];

        // Матрица хранящая фигуры тип шахматной фигуры для данной клетки [если нет фигуры - EmptyCell]
        Figure[,] FigureBoard { get; set; } = new Figure[GC.BoardSize, GC.BoardSize];

        // Индексы ячейки откуда начнется движение выбранной фигуры
        int StartMoveIndexX { get; set; } = -1;
        int StartMoveIndexY { get; set; } = -1;

        // Индексы ячейки в которой закончится движение выбранной фигуры
        int EndMoveIndexX { get; set; } = -1;
        int EndMoveIndexY { get; set; } = -1;

        // Показывает выбранна ли какая-либо из фигур
        bool IsFigureChosenForStep { get; set; } = false;

        // True если сделан ход! False в противном случае 
        bool IsFigureMadeStep { get; set; } = false;

        // Таймер нужен для того, чтобы от (якобы) двойнова щелчка фигуры которой сходили сразу же не выбиралась
        double TimeBetweenLeftBTNClick { get; set; } = 0;

        // Хранит предыдущее состояние мыши! Нужно чтобы не нажималось два раза.
        MouseState prevMouseState = new MouseState();

        bool IsWhiteMove { get; set; } = true;

        bool IsBlackMove { get; set; } = false;

        #endregion

    }
}
