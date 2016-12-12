using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GC = Chess.GameParameters.GameConstants;

namespace Chess.GameButtons.WinMenu
{
    class WinMenu
    {
        enum WinMenuID
        {
            REFRESH = 0,
            WATCH = 1,
            QUIT = 2
        }


        public WinMenu()
        {
            WMenu[(int)WinMenuID.REFRESH] = new WMBtnRefresh(GC.WMIconLevelY, GC.WindowWidth / 2 - 3 * GC.GMIconWidth);
            WMenu[(int)WinMenuID.WATCH] = new WMBtnWatch(GC.WMIconLevelY, GC.WindowWidth / 2 - GC.GMIconWidth / 2);
            WMenu[(int)WinMenuID.QUIT] = new WMBtnQuit(GC.WMIconLevelY, GC.WindowWidth / 2 + 2 * GC.GMIconWidth);
        }


        public void Update(MouseState curMouseState, Game1 game, int winnerColor)
        {
            WinPlayerColor = winnerColor;

            foreach (Icon icon in WMenu)
            {
                icon.Update(curMouseState, game);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            backgroung = Content.Load<Texture2D>(@"WinMenu/WinBackgroung");
            trophy = Content.Load<Texture2D>(@"WinMenu/Trophy_White");

            foreach (Icon icon in WMenu)
                icon.LoadContent(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Отрисовка фона
            spriteBatch.Draw(backgroung, new Rectangle(0, 0, GC.WindowWidth, GC.WindowHeight), Color.White);

            if (!IsWatchBtnClicked)
            {
                // Рисует кубок цвета соответствующего победителю
                Rectangle trophyRect = new Rectangle(GC.WindowWidth / 2 - GC.WMTrophyWidth / 2, GC.WindowHeight / 3, GC.WMTrophyWidth, GC.WMTrophyHeight);
                if (WinPlayerColor == (int)GameWinner.WHITE)
                    spriteBatch.Draw(trophy, trophyRect, Color.White);
                else
                    spriteBatch.Draw(trophy, trophyRect, Color.Black);
            }

            // Отоборажение всех иконок
            foreach (Icon icon in WMenu)
                icon.Draw(spriteBatch);
        }

        #region Fields

        // Показывает нажата ли кнопка посмотреть результаты игры
        public static bool IsWatchBtnClicked { get; set; } = false;
        // Показывает нажата ли кнопка начать новую игру
        public static bool IsNewGameBtnCliced { get; set; } = false;

        // Цвет победителя
        int WinPlayerColor { get; set; }

        // Тексетуры с внешним фоном и кубком победителя
        Texture2D backgroung;
        Texture2D trophy;

        Icon[] WMenu = new Icon[GC.WMCount];

        #endregion

    }
}
