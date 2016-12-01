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

        void SetCellSelect(MouseState curMouseState)
        {
            ChosenIndexY = (curMouseState.Position.Y - GC.IndentTop) / GC.CellHeight;
            ChosenIndexX = (curMouseState.Position.X - GC.IndentLeft) / GC.CellWidth;

            // Берем тип Пустой клетки и тип нажатой клетки
            object EmptySellType = typeof(EmptyCell);
            object SelectedCellType = FigureBoard[ChosenIndexY, ChosenIndexX].GetType();

            // Если нажали не на пустую клетку то подсвечиваем клетку
            if (EmptySellType != SelectedCellType)
            {
                Board[ChosenIndexY, ChosenIndexX].Update();
                IsPlayerSelectCell = true;
                // Говорим что игрок выбрал именно фигуру а не пустую клетку
                IsFigureSelect = true;
            }
        }

        void SetCellUnselect(MouseState curMouseState)
        {
            ChosenIndexX = (curMouseState.Position.X - GC.IndentLeft) / GC.CellWidth;
            ChosenIndexY = (curMouseState.Position.Y - GC.IndentTop) / GC.CellHeight;

            if (Board[ChosenIndexY, ChosenIndexX].IsSelect)
            {
                Board[ChosenIndexY, ChosenIndexX].Update();
                IsPlayerSelectCell = !IsPlayerSelectCell;
            }
        }



        void ClickOnLeftButtonActions(MouseState curMouseState)
        {
            if (curMouseState.LeftButton == ButtonState.Pressed)
            {
                if (!IsPlayerSelectCell)
                    SetCellSelect(curMouseState);
                else if (IsPlayerSelectCell && IsFigureSelect)
                {
                    // Находим все доступные для хода позиции
                    List<IndexPair> possibleSteps = new List<IndexPair>();
                    FigureBoard[ChosenIndexY, ChosenIndexX].GetPossiblePositions(possibleSteps);

                    // Индексы клетки куда нажал пользователь
                    int X = (curMouseState.Position.X - GC.IndentLeft) / GC.CellWidth;
                    int Y = (curMouseState.Position.Y - GC.IndentTop) / GC.CellHeight;
                    IndexPair curStep = new IndexPair(Y, X);
                    // Если пользователь нажат на ячейку в которую можно сходить данной фигурой
                    if (possibleSteps.Contains(curStep))
                    {
                        FigureBoard[ChosenIndexY, ChosenIndexX].Update(Y, X);
                        FigureBoard[Y, X].Update(ChosenIndexY, ChosenIndexX);
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

        // Индекс выбранной играком ячейки в данный момент (-1 - знеачит не выбрана)
        int ChosenIndexX { get; set; } = -1;
        int ChosenIndexY { get; set; } = -1;
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
