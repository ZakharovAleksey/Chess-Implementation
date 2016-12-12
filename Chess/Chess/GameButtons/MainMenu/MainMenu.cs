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

namespace Chess.GameButtons
{
    class MainMenu
    {
        enum MainMenuID
        {
            NEW_GAME = 0,
            LOAD_GAME = 1,
            EXIT_GAME = 2
        }

        public MainMenu()
        {
            MMenu[(int)MainMenuID.NEW_GAME] = new BtnNewGame(GC.MMBtnIndentTop, GC.MMBtnIndentLeft);
            MMenu[(int)MainMenuID.LOAD_GAME] = new BtnLoadGame(GC.MMBtnIndentTop + GC.MMBtnDistance, GC.MMBtnIndentLeft);
            MMenu[(int)MainMenuID.EXIT_GAME] = new BtnExitGame(GC.MMBtnIndentTop + 2 *  GC.MMBtnDistance, GC.MMBtnIndentLeft);
        }

        public void Update(MouseState curMouseState, Game1 game)
        {
            foreach (Button btn in MMenu)
                btn.Update(curMouseState, game);
        }

        public void LoadContent(ContentManager Content)
        {
            background = Content.Load<Texture2D>("MainMenu/MenuBackGround");

            foreach (Button btn in MMenu)
                btn.LoadContent(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GC.WindowWidth, GC.WindowHeight), Color.White);

            foreach (Button btn in MMenu)
                btn.Draw(spriteBatch);
        }

        #region Fields

        // Задний фон
        Texture2D background;
        // Массив который хранит все кнопки в главном меню
        Button[] MMenu { get; set; } = new Button[GC.BtnCountInMainMenu];

        #endregion
    }
}
