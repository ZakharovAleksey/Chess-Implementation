using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameButtons.PauseMenu
{
    class PMBtnOpen : Icon
    {
        public PMBtnOpen(int posY, int posX) : base(posY, posX) { }


        public override void OnButtonClick(Game1 game)
        {
            PauseMenu.IsLoadBtmClicked = true;

        }

        public override void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(@"PauseMenu/Open");
        }
    }
}
