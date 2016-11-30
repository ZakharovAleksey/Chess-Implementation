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

        #region Fields

        Cell[,] Board { get; set; } = new Cell[GC.BoardSize, GC.BoardSize];

        #region Actions

        // True if user already select one cell
        bool IsPlayerSelectCell { get; set; } = false;

        #endregion


        #endregion

        public ChessBoard()
        {
            for (int rowID = 0; rowID < GC.BoardSize; ++rowID)
            {
                int curColor = (rowID % 2 == 0) ? (int)CellType.WHITE : (int)CellType.BLACK;
                for (int columnID = 0; columnID < GC.BoardSize; ++columnID)
                {
                    Board[rowID, columnID] = new Cell(rowID, columnID, curColor);
                    curColor = (curColor == (int)CellType.BLACK) ? (int)CellType.WHITE : (int)CellType.BLACK;
                }
            }

        }

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            foreach (Cell cell in Board)
                cell.LoadContent(Content);
        }

        #region Update 

        bool IsClickInChessboard(MouseState curMouseState)
        {
            return (curMouseState.Position.X >= GC.IndentLeft && curMouseState.Position.X <= GC.IndentRight
                && curMouseState.Y >= GC.IndentTop && curMouseState.Y <= GC.IndentBottom) ? true : false;
        }

        void SetCellSelect(MouseState curMouseState)
        {
            int x = (curMouseState.Position.X - GC.IndentLeft) / Cell.Width;
            int y = (curMouseState.Position.Y - GC.IndentTop) / Cell.Height;

            Board[y, x].Update();

            IsPlayerSelectCell = !IsPlayerSelectCell;

        }

        void ClickOnLeftButtonActions(MouseState curMouseState)
        {
            if (curMouseState.LeftButton == ButtonState.Pressed)
            {
                if (!IsPlayerSelectCell)
                    SetCellSelect(curMouseState);
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

        void SetCellUnselect(MouseState curMouseState)
        {
            int x = (curMouseState.Position.X - GC.IndentLeft) / Cell.Width;
            int y = (curMouseState.Position.Y - GC.IndentTop) / Cell.Height;

            if (Board[y, x].IsSelect)
            {
                Board[y, x].Update();
                IsPlayerSelectCell = !IsPlayerSelectCell;
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
        }

        #endregion

    }
}
