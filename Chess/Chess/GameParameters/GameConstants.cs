using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chess.GameParameters
{
    public static class GameConstants
    {
        #region Main window parameters

        public const int WindowWidth = 900;
        public const int WindowHeight = 500;

        #endregion

        #region Chess Board parameters

        // Number of cells on the chees board
        public const int BoardSize = 8;

        #region Cell parameters

        public const int CellHeight = WindowHeight / (BoardSize + 2);
        public const int CellWidth = CellHeight;


        #endregion

        // Chees board indents from apropriate border
        #region Indents 

        public const int IndentTop = CellHeight;
        public const int IndentBottom = WindowHeight - CellHeight;
        public const int IndentRight = WindowWidth - CellWidth;
        public const int IndentLeft = WindowWidth - (CellWidth * (BoardSize + 1));

        #endregion

        #endregion

        public const double TimerDelay = 500;
    }
}
