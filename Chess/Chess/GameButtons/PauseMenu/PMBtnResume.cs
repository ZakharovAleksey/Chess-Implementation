using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GC = Chess.GameParameters.GameConstants;

namespace Chess.GameButtons.PauseMenu
{
    class PMBtnResume : Icon
    {
        public PMBtnResume(int posY, int posX) : base(posY, posX)
        {
            OnScreenPos = new Rectangle(PositionX, PositionY, GC.PMIconPauseWidth, GC.PMIconPauseHeight);
        }


        public override void OnButtonClick(Game1 game)
        {
            if (IsClicked)
                game.CurGameState = (int)GameState.EXECUTION;
                
        }

        public override void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(@"PauseMenu/Resume");
        }
    }
}
