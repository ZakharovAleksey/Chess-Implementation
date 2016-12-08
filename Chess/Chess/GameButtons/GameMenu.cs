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
    class GameMenu
    {
        public GameMenu()
        {
            Body[0] = new BtnNewGame(GC.GMBtnIndentTop, GC.GMBtnIndentLeft);
            Body[1] = new BtnLoadGame(GC.GMBtnIndentTop + GC.GMBtnDistance, GC.GMBtnIndentLeft);
            Body[2] = new BtnExitGame(GC.GMBtnIndentTop + 2 * GC.GMBtnDistance, GC.GMBtnIndentLeft);
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
