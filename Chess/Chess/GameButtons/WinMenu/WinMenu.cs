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
        public WinMenu()
        {
            WMenu[0] = new WMBtnQuit(GC.WMIconLevelY, 100);
            WMenu[1] = new WMBtnRefresh(GC.WMIconLevelY, 300);
        }


        public void Update(MouseState curMouseState, Game1 game)
        {
            if (game.Winner == (int) GameWinner.WHITE)
                IsWhiteWin = true;
            else if (game.Winner == (int)GameWinner.BLACK)
                IsBlackWin = true;

            foreach (Icon icon in WMenu)
                icon.Update(curMouseState, game);
        }

        public void LoadContent(ContentManager Content)
        {
            winBackground = Content.Load<Texture2D>(@"WinMenu/WinBackgroung");

            trophyBlack = Content.Load<Texture2D>(@"WinMenu/Trophy_Black");
            trophyWhite = Content.Load<Texture2D>(@"WinMenu/Trophy_White");

            foreach (Icon icon in WMenu)
                icon.LoadContent(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle fullScreen = new Rectangle(0, 0, GC.WindowWidth, GC.WindowHeight);
            spriteBatch.Draw(winBackground, fullScreen, Color.White);

            Rectangle trophyDest = new Rectangle(GC.WindowHeight / 3, GC.WindowWidth / 2 - GC.WMTrophyWidth / 2, GC.WMTrophyWidth, GC.WMTrophyHeight);

            if (IsWhiteWin)
            {
                spriteBatch.Draw(trophyWhite, trophyDest, Color.White);
                IsWhiteWin = false;
            }
            else if (IsBlackWin)
            {
                spriteBatch.Draw(trophyBlack, trophyDest, Color.White);
                IsBlackWin = false;
            }

            foreach (Icon icon in WMenu)
                icon.Draw(spriteBatch);

        }

        bool IsWhiteWin = false;
        bool IsBlackWin = false;

        Texture2D trophyBlack;
        Texture2D trophyWhite;

        Texture2D winBackground;

        Icon[] WMenu = new Icon[GC.WMCount];

    }
}
