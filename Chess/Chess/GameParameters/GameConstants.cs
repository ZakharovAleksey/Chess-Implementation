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

        public const int WindowWidth = 500;
        public const int WindowHeight = 500;

        #endregion

        #region Chess Board parameters

        // Number of cells on the chees board
        public const int BoardSize = 8;

        // Число клеток доски которое является отступом от края области отрисовки сверху
        public const int CellBoundCount = 2;

        #region Cell parameters

        public const int CellHeight = WindowHeight /(BoardSize + CellBoundCount * 2);
        public const int CellWidth = CellHeight;

        #endregion

        // Отступы от соответствующей границы
        #region Indents 
        
        public const int IndentTop = CellBoundCount * CellHeight;
        public const int IndentBottom = WindowHeight - CellBoundCount * CellHeight;
        public const int IndentRight = WindowWidth / 2 + BoardSize / 2 * CellWidth;
        public const int IndentLeft = IndentRight - BoardSize * CellWidth;

        #endregion

        #endregion

        #region Clock parameters

        public const int ClockWidth = CellWidth;
        public const int ClockHeight = ClockWidth;

        public const int ClockIndentRight = IndentRight + 2 * ClockWidth;

        #endregion

        #region Button parameters

        public const int MMBtnWidth = WindowWidth / 5;
        public const int MMBtnHeight = WindowHeight / 10;

        public const int MMBtnIndentTop = WindowHeight / 2 - MMBtnDistance - MMBtnDistance / 2;
        public const int MMBtnIndentLeft = WindowWidth / 2 - MMBtnWidth / 2;
        public const int MMBtnDistance = MMBtnHeight + 30;

        // Показывает на сколько изменяется глубина цвета кнопки, когда на нее наводишь мышь
        public const int BtnColorDepthInc = 5;

        // Количество кнопок в Главном меню
        public const int BtnCountInMainMenu = 3;

        #endregion


        #region Game Menu parameters

        public const int GMIconWidth = WindowWidth / 10;
        public const int GMIconHeight = GMIconWidth;

        public const int GMIconIndentTop = WindowWidth / 30;
        public const int GMIconIndentLeft = GMIconIndentTop;

        public const int GMIconDistance = MMBtnHeight + 10;

        public const int GMCount = 1;

        #endregion


        #region Pause Menu parameters

        public const int PMIconPauseWidth = WindowWidth / 3;
        public const int PMIconPauseHeight = PMIconPauseWidth;


        public const int PMIconLevelY = 350;

        public const int PMCount = 5;

        #endregion


        public const double TimerDelay = 75;
    }
}
