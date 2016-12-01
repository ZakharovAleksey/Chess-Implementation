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
                    if (rowID == GC.BoardSize - 2)
                        FigureBoard[rowID, columnID] = new Pawn(rowID, columnID);
                    else
                        FigureBoard[rowID, columnID] = new EmptyCell(rowID, columnID);
                }
            }
        }

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            foreach (Cell cell in Board)
                cell.LoadContent(Content);

            // Load pawn content
            foreach (Figure fig in FigureBoard)
                fig.LoadContent(Content);

        }

        #region Update 



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


        void SetCellSelect(MouseState curMouseState)
        {
            // Переменная хранит выбранную на данный момент пользователем позицию
            IndexPair selectPos = GetChosenCellIndex(curMouseState);

            // Находим тип выбранной, для начала движения клетки чтобы проверить не выбрана ли клетка в которой нет фигуры.
            object emptyCellType = typeof(EmptyCell);
            object selectedCellType = FigureBoard[selectPos.IndexY, selectPos.IndexX].GetType();

            // Если выбранная клетка с фигурой то подсвечиваем ее красным цветом
            if (emptyCellType != selectedCellType)
            {
                StartMoveIndexY = selectPos.IndexY;
                StartMoveIndexX = selectPos.IndexX;

                Board[StartMoveIndexY, StartMoveIndexX].Update();

                // Говорим что пользователь действительно выбрал клетку с фигурой [чтобы сделать этой фигурой шаг]
                IsPlayerSelectCell = true;
                IsFigureSelect = true;
            }
            else
            {
                StartMoveIndexY = -1;
                StartMoveIndexX = -1;
            }
        }

        void SetCellUnselect(MouseState curMouseState)
        {
            //StartMoveIndexX = (curMouseState.Position.X - GC.IndentLeft) / GC.CellWidth;
            //StartMoveIndexY = (curMouseState.Position.Y - GC.IndentTop) / GC.CellHeight;

            if (Board[StartMoveIndexY, StartMoveIndexX].IsSelect)
            {
                Board[StartMoveIndexY, StartMoveIndexX].Update();
                IsPlayerSelectCell = !IsPlayerSelectCell;
            }
        }



        void ClickOnLeftButtonActions(MouseState curMouseState)
        {
            if (curMouseState.LeftButton == ButtonState.Pressed)
            {
                if (!IsPlayerSelectCell)
                {
                    // Переменная хранит выбранную на данный момент пользователем позицию
                    IndexPair selectPos = GetChosenCellIndex(curMouseState);

                    if (IsCellEmpty(selectPos.IndexY, selectPos.IndexX))
                    {
                        StartMoveIndexY = -1;
                        StartMoveIndexX = -1;
                    }
                    else
                    {
                        StartMoveIndexY = selectPos.IndexY;
                        StartMoveIndexX = selectPos.IndexX;

                        Board[StartMoveIndexY, StartMoveIndexX].Update();

                        // Говорим что пользователь действительно выбрал клетку с фигурой [чтобы сделать этой фигурой шаг]
                        IsPlayerSelectCell = true;
                        IsFigureSelect = true;
                    }

                    //SetCellSelect(curMouseState);
                }
                else if (IsPlayerSelectCell && IsFigureSelect)
                {
                    // Находим все доступные для хода позиции
                    List<IndexPair> possibleSteps = new List<IndexPair>();
                    FigureBoard[StartMoveIndexY, StartMoveIndexX].GetPossiblePositions(possibleSteps);

                    // Индексы клетки куда нажал пользователь
                    int X = (curMouseState.Position.X - GC.IndentLeft) / GC.CellWidth;
                    int Y = (curMouseState.Position.Y - GC.IndentTop) / GC.CellHeight;
                    IndexPair curStep = new IndexPair(Y, X);
                    // Если пользователь нажат на ячейку в которую можно сходить данной фигурой
                    if (possibleSteps.Contains(curStep))
                    {
                        FigureBoard[StartMoveIndexY, StartMoveIndexX].Update(Y, X);
                        FigureBoard[Y, X].Update(StartMoveIndexY, StartMoveIndexX);
                    }
                }
            }
        }

        void ClickOnRightButtonActions(MouseState curMouseState)
        {
            if (curMouseState.RightButton == ButtonState.Pressed)
            {
                if (IsPlayerSelectCell)
                    SetCellUnselect(curMouseState);
            }
        }



        public void Update()
        {
            MouseState curMouseState = Mouse.GetState();

            if (IsClickInChessboard(curMouseState))
            {
                ClickOnLeftButtonActions(curMouseState);
                ClickOnRightButtonActions(curMouseState);
            }

        }

        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Cell cell in Board)
                cell.Draw(spriteBatch);

            foreach (Figure fig in FigureBoard)
                fig.Draw(spriteBatch);
        }

        #endregion

        #region Fields

        Cell[,] Board { get; set; } = new Cell[GC.BoardSize, GC.BoardSize];
        
        // Индексы ячейки откуда начнется движение выбранной фигуры
        int StartMoveIndexX { get; set; } = -1;
        int StartMoveIndexY { get; set; } = -1;

        // Индексы ячейки в которой закончится движение выбранной фигуры
        int EndMoveIndexX { get; set; } = -1;
        int EndMoveIndexY { get; set; } = -1;

        // Показывает выбранна ли какая-либо из фигур
        bool IsFigureSelect { get; set; } = false;

        // True if user already select one cell
        bool IsPlayerSelectCell { get; set; } = false;


        #region Chess figures

        Figure[,] FigureBoard { get; set; } = new Figure[GC.BoardSize, GC.BoardSize];

        #endregion


        #endregion

    }
}
