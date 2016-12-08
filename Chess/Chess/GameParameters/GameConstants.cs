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

        public const int WindowWidth = 800;
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
        public const int IndentRight = WindowWidth  - CellWidth;
        public const int IndentLeft = WindowWidth - (CellWidth * (BoardSize + 1));

        #endregion

        #endregion

        #region Button parameters

        public const int BtnWidth = WindowWidth / 5;
        public const int BtnHeight = WindowHeight / 10;

        public const int BtnIndentTop = WindowHeight / 2 - BtnDistance - BtnDistance / 2;
        public const int BtnIndentLeft = WindowWidth / 2 - BtnWidth / 2;
        public const int BtnDistance = BtnHeight + 30;

        // Показывает на сколько изменяется глубина цвета кнопки, когда на нее наводишь мышь
        public const int BtnColorDepthInc = 5;

        public const int BtnCountInMainMenu = 3;

        #region Game Menu parameters

        public const int GMIconWidth = 40;
        public const int GMIconHeight = 40;

        public const int GMIconIndentTop = WindowHeight / 10;
        public const int GMIconIndentLeft = 100;

        public const int GMIconDistance = BtnHeight + 30;


        public const int GMCount = 4;

        #endregion


        #endregion

        public const double TimerDelay = 75;
    }
}
