using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework;


namespace Chess.GameButtons
{
    class BtnExitGame : Button
    {
        public BtnExitGame(int posY, int posX) : base(posY, posX) { }


        public override void OnButtonClick(Game1 game)
        {
            if(IsCkicked)
                game.Exit();
        }

        public override void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(@"MainMenu/Exit");
        }


    }
}
