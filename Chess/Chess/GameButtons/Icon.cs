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
            OnScreenPos = new Rectangle(PositionX, PositionY, GC.GMIconWidth, GC.GMIconHeight);
        }

        #region Methods

        public override void SetPosition(int positionY, int positionX)
        {
            PositionY = positionY;
            PositionX = positionX;

            OnScreenPos = new Rectangle(PositionX, PositionY, GC.GMIconWidth, GC.GMIconHeight);
        }

        public override void OnButtonClick(Game1 game) { }

        public override void LoadContent(ContentManager Content) { }

        #endregion
    }
}
