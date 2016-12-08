using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GC = Chess.GameParameters.GameConstants;

namespace Chess.GameButtons.GameMenu
{
    class GameMenu
    {

        public GameMenu()
        {
            GMMenu[0] = new GMBtnMenu(GC.GMIconIndentLeft, GC.GMIconIndentTop);
            GMMenu[1] = new GMBtnOpen(GC.GMIconIndentLeft + GC.GMIconDistance, GC.GMIconIndentTop);
            GMMenu[2] = new GMBtnSave(GC.GMIconIndentLeft + 2 * GC.GMIconDistance, GC.GMIconIndentTop);
            GMMenu[3] = new GMBtnSettings(GC.GMIconIndentLeft + 3 * GC.GMIconDistance, GC.GMIconIndentTop);
        }

        public void Update(MouseState curMouseState, Game1 game)
        {
            foreach (Icon icon in GMMenu)
                icon.Update(curMouseState, game);
        }

        public void LoadContent(ContentManager Content)
        {
            foreach (Icon icon in GMMenu)
                icon.LoadContent(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Icon icon in GMMenu)
                icon.Draw(spriteBatch);
        }


        Icon[] GMMenu { get; set; } = new Icon[GC.GMCount];
    }
}
