using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameButtons.WinMenu
{
    class WMBtnWatch : Icon
    {
        public WMBtnWatch(int posY, int posX) : base(posY, posX) { }


        public override void OnButtonClick(Game1 game)
        {
            if (IsClicked)
            {
                WinMenu.IsWatchBtnClicked = true;
            }
        }

        public override void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(@"WinMenu/Watch");
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (!WinMenu.IsWatchBtnClicked)
                base.Draw(spritebatch);
        }

    }
}
