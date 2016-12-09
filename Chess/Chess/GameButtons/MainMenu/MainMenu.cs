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
    /*
     В игровом меню:
     - Save

     
     * В меню Пауза:
     - Иконка Play button
     - Иконка Save
     - Иконка Pause
     - Иконка Load
     - Иконка Options

      В основном меню:
      - Иконка
      
     */


    class MainMenu
    {
        public MainMenu()
        {
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
            foreach (Button btn in Body)
                btn.LoadContent(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button btn in Body)
                btn.Draw(spriteBatch);
        }

        #region Fields

        Button[] Body { get; set; } = new Button[GC.BtnCountInMainMenu];

        #endregion
    }
}
