using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.GameButtons.PauseMenu;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chess.GameButtons.WinMenu
{
    class WMBtnQuit : Icon
    {
        public WMBtnQuit(int posY, int posX) : base(posY, posX) { }


        public override void OnButtonClick(Game1 game)
        {
            if (IsClicked)
            {
                game.Exit();
            }
        }

        public override void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(@"WinMenu/Quit");
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (!WinMenu.IsWatchBtnClicked)
                base.Draw(spritebatch);
            else
            {
                int a = 10;
            }
        }
    }
}
