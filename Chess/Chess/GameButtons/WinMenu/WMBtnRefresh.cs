using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameButtons.WinMenu
{
    class WMBtnRefresh : Icon
    {
        public WMBtnRefresh(int posY, int posX) : base(posY, posX) { }


        public override void OnButtonClick(Game1 game)
        {
            if (IsCkicked)
            {
                game.CurGameState = (int)GameState.EXECUTION;
                game.board.SetStateIDLE();
            }
        }

        public override void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(@"WinMenu/Refresh");
        }
    }
}
