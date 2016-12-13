using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameButtons
{
    class BtnNewGame : Button
    {
        public BtnNewGame(int posY, int posX) : base(posY, posX) { }


        public override void OnButtonClick(Game1 game)
        {
            if (IsClicked)
            {
                game.CurGameState = (int)GameState.EXECUTION;
            }

        }

        public override void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(@"MainMenu/NewGame");
        }
    }
}
