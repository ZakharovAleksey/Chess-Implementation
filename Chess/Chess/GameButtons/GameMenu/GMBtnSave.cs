using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameButtons.GameMenu
{
    class GMBtnSave : Icon
    {
        public GMBtnSave(int posY, int posX) : base(posY, posX) { }


        public override void OnButtonClick(Game1 game)
        {
        }

        public override void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(@"GameMenu/Save");
        }
    }
}
