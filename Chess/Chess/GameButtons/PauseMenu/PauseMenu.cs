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
        // Определяет номера иконок в меню паузы
        enum PauseMenuID
        {
            RESUME = 0,
            NEW_GAME = 1,
            OPEN = 2, 
            SAVE = 3,
            SETTINGS = 4,
            QUIT = 5,
            
        }


        public PauseMenu()
        {
            PMenu[(int) PauseMenuID.RESUME] = new PMBtnResume(GC.WindowHeight / 2 - GC.PMIconPauseHeight /4 * 3 , GC.WindowWidth / 2 - GC.PMIconPauseWidth / 2);

            int Indent = GC.WindowWidth / 2 - GC.GMIconWidth / 2 - 2 * GC.GMIconWidth - GC.WindowWidth / 15;

            PMenu[(int)PauseMenuID.NEW_GAME] = new PMBtnNewGame(GC.PMIconLevelY, Indent);
            Indent += GC.GMIconWidth + GC.WindowWidth / 30;
            PMenu[(int)PauseMenuID.OPEN] = new PMBtnOpen(GC.PMIconLevelY, Indent);
            Indent += GC.GMIconWidth + GC.WindowWidth / 30;
            PMenu[(int)PauseMenuID.SAVE] = new PMBtnSave(GC.PMIconLevelY, Indent);
            Indent += GC.GMIconWidth + GC.WindowWidth / 30;
            PMenu[(int)PauseMenuID.SETTINGS] = new PMBtnSettings(GC.PMIconLevelY, Indent);
            Indent += GC.GMIconWidth + GC.WindowWidth / 30;
            PMenu[(int)PauseMenuID.QUIT] = new PMBtnQuit(GC.PMIconLevelY, Indent);
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
        public static bool IsLoadBtmClicked { get; set; } = false;
        public static bool IsNewGameCliced { get; set; } = false;


        // Текстуры для заднего фона и иконок меню
        Texture2D background;
        Icon[] PMenu = new Icon[GC.PMCount];

        #endregion

    }
}
