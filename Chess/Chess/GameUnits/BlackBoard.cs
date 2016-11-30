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

namespace Chess.GameUnits
{
    class ChessBoard
    {

        #region Fields

        Cell[,] Board { get; set; } = new Cell[GameConstants.BoardSize, GameConstants.BoardSize];

        #region Actions

        // True if user already select one cell
        bool IsCellSelect { get; set; } = false;

        #endregion


        #endregion

        public ChessBoard()
        {
            for (int rowID = 0; rowID < GameConstants.BoardSize; ++rowID)
            {
                int currentColor = (rowID % 2 == 0) ? (int)CellType.WHITE : (int)CellType.BLACK;

                for (int columnID = 0; columnID < GameConstants.BoardSize; ++columnID)
                {
                    Rectangle currentPosition = new Rectangle
                        (
                            GameConstants.IndentLeft + rowID * Cell.Width,
                            GameConstants.IndentTop + columnID * Cell.Height,
                            Cell.Width,
                            Cell.Height
                        );

                    Board[rowID, columnID] = new Cell(currentColor, currentPosition, new KeyValuePair<int, int>(rowID, columnID));


                    currentColor = (currentColor == (int)CellType.BLACK) ? (int)CellType.WHITE : (int)CellType.BLACK;
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

        /// <summary>
        /// Checks whether the mouse click hit the chess board
        /// </summary>
        /// <param name="curMouseState"> Current Mouse State </param>
        /// <returns> True if mouse click hits the chess board, false otherwise </returns>
        bool IsInCheesBoard(MouseState curMouseState)
        {
            return (curMouseState.Position.X >= GameConstants.IndentLeft && curMouseState.Position.X <= GameConstants.IndentRight
                && curMouseState.Y >= GameConstants.IndentTop && curMouseState.Y <= GameConstants.IndentBottom) ? true : false;
        }

        /// <summary>
        /// Calculate index (Name) of current clicked cell by it's own position
        /// </summary>
        /// <param name="curMouseState"> Current Mouse State </param>
        /// <returns> Pair: Key - row index, Value - column index </returns>
        KeyValuePair<int, int> getSelectedCellName(MouseState curMouseState)
        {
            int rowID = (curMouseState.Position.X - GameConstants.IndentLeft) / Cell.Width;
            int columnID = (curMouseState.Position.Y - GameConstants.IndentTop) / Cell.Height;

            return new KeyValuePair<int, int>(rowID, columnID);
        }

        public void Update()
        {
            MouseState curMouseState = Mouse.GetState();

            if (IsInCheesBoard(curMouseState))
            {
                if (curMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (!IsCellSelect)
                    {
                        KeyValuePair<int, int> selectedCellName = getSelectedCellName(curMouseState);
                        Board[selectedCellName.Key, selectedCellName.Value].Update();

                        IsCellSelect = true;
                    }
                }
                if (curMouseState.RightButton == ButtonState.Pressed)
                {
                    if (IsCellSelect)
                    {
                        KeyValuePair<int, int> selectedCellName = getSelectedCellName(curMouseState);

                        if (Board[selectedCellName.Key, selectedCellName.Value].IsSelect)
                        {
                            Board[selectedCellName.Key, selectedCellName.Value].Update();
                            IsCellSelect = false;
                        }
                    }

                }
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
