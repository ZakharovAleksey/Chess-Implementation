using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GC = Chess.GameParameters.GameConstants;

namespace Chess.GameButtons
{
    abstract class Icon : Button
    {
        public Icon(int posY, int posX) : base(posY, posX)
        {
            ScreenRectangle = new Rectangle(PositionX, PositionY, GC.GMIconWidth, GC.GMIconHeight);
        }


        public override void OnButtonClick(Game1 game) { }

        public override void LoadContent(ContentManager Content) { }
    }
}
