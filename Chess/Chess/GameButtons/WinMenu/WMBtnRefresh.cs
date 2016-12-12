using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GC = Chess.GameParameters.GameConstants;

namespace Chess.GameButtons.WinMenu
{
    class WMBtnRefresh : Icon
    {
        public WMBtnRefresh(int posY, int posX) : base(posY, posX) { }

        public override void OnButtonClick(Game1 game)
        {
            if (IsClicked)
            {
                WinMenu.IsNewGameBtnCliced = true;
            }
        }

        public override void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(@"WinMenu/Refresh");
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (WinMenu.IsWatchBtnClicked)
                this.SetPosition(GC.GMIconIndentTop, GC.GMIconIndentLeft);
            base.Draw(spritebatch);
        }
    }
}
