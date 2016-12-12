using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GC = Chess.GameParameters.GameConstants;

namespace Chess.GameButtons.PauseMenu
{
    class PauseMenu
    {

        public PauseMenu()
        {
            PMenu[0] = new PMBtnResume(100, GC.WindowWidth / 2 - GC.PMIconPauseWidth / 2);
            PMenu[1] = new PMBtnOpen(GC.PMIconLevelY, 100);
            PMenu[2] = new PMBtnSave(GC.PMIconLevelY, 200);
            PMenu[3] = new PMBtnSettings(GC.PMIconLevelY, 300);
            PMenu[4] = new PMBtnQuit(GC.PMIconLevelY, 400);
        }


        public void Update(MouseState curMouseState, Game1 game)
        {
            foreach (Icon icon in PMenu)
                icon.Update(curMouseState, game);
        }

        public void LoadContent(ContentManager Content)
        {
            background = Content.Load<Texture2D>(@"PauseMenu/PauseBackgroung");

            foreach (Icon icon in PMenu)
                icon.LoadContent(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle fullScreen = new Rectangle(0, 0, GC.WindowWidth, GC.WindowHeight);
            spriteBatch.Draw(background, fullScreen, Color.White);

            foreach (Icon icon in PMenu)
                icon.Draw(spriteBatch);
        }

        #region Fields

        public static bool IsSaveBtnClicked { get; set; } = false;



        Texture2D background;
        Icon[] PMenu = new Icon[GC.PMCount];

        #endregion

    }
}
