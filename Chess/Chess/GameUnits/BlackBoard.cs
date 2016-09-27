using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Chess.GameParameters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chess.GameUnits
{
    class BlackBoard
    {

        #region Fields

        Cell[,] Board { get; set; } = new Cell[GameConstants.BlackBoardSize, GameConstants.BlackBoardSize];

        #endregion


        public BlackBoard()
        {
            for (int columnID = 0; columnID < GameConstants.BlackBoardSize; ++columnID)
            {
                int currentColor = (columnID % 2 == 0) ? (int)CellType.WHITE : (int)CellType.BLACK;

                for (int rowID = 0; rowID < GameConstants.BlackBoardSize; ++rowID)
                {
                    Rectangle currentPosition = new Rectangle(rowID * Cell.Width, columnID * Cell.Height, Cell.Width, Cell.Height);
                    Board[columnID, rowID] = new Cell(currentColor, currentPosition);

                    currentColor = (currentColor == (int)CellType.BLACK) ? (int)CellType.WHITE : (int)CellType.BLACK;
                }
            }
        }

        public void LoadContent(ContentManager Content)
        {
            foreach (Cell cell in Board)
                cell.LoadContent(Content);
        }

        public void Update()
        {
            foreach (Cell cell in Board)
                cell.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Cell cell in Board)
                cell.Draw(spriteBatch);
        }


    }
}
