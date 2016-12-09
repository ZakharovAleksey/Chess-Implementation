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
        public MainMenu()
        {
            // Инициализируем каждую из кнопока главного меню
            Body[0] = new BtnNewGame(GC.MMBtnIndentTop, GC.MMBtnIndentLeft);
            Body[1] = new BtnLoadGame(GC.MMBtnIndentTop + GC.MMBtnDistance, GC.MMBtnIndentLeft);
            Body[2] = new BtnExitGame(GC.MMBtnIndentTop + 2 *  GC.MMBtnDistance, GC.MMBtnIndentLeft);
        }

        public void Update(MouseState curMouseState, Game1 game)
        {
            foreach (Button btn in Body)
                btn.Update(curMouseState, game);
        }

        public void LoadContent(ContentManager Content)
        {
            BackGround = Content.Load<Texture2D>("MainMenu/MenuBackGround");

            foreach (Button btn in Body)
                btn.LoadContent(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackGround, new Rectangle(0, 0, GC.WindowWidth, GC.WindowHeight), Color.White);

            foreach (Button btn in Body)
                btn.Draw(spriteBatch);
        }

        #region Fields

        // Массив который хранит все кнопки в главном меню
        Button[] Body { get; set; } = new Button[GC.BtnCountInMainMenu];

        // Фон Главного меню
        Texture2D BackGround { get; set; }

        #endregion
    }
}
