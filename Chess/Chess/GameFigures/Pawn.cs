using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Chess.GameParameters;
using Microsoft.Xna.Framework.Input;

namespace Chess.GameFigures
{
    /// <summary>
    /// Имплементация класса пешки
    /// </summary>
    class Pawn
    {

        #region Construcor

        public Pawn(Vector2 position)
        {
            this.CurPosition = new Rectangle((int) position.X, (int) position.Y, GameConstants.CellWidth, GameConstants.CellHeight);
        }

        public Pawn(Vector2 position, ContentManager Content) : this(position)
        {
            texture = Content.Load<Texture2D>(@"figure/pawn");
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(@"figures/pawn");
        }

        bool IsChecked(MouseState curMouseState)
        {
            return (curMouseState.Y > CurPosition.Top && curMouseState.Y < CurPosition.Bottom
                && curMouseState.X > CurPosition.Left && curMouseState.X < CurPosition.Right) ? true : false;
        }

        public void Update()
        {
            MouseState curState = Mouse.GetState();

            if (curState.LeftButton == ButtonState.Pressed && IsChecked(curState))
            {
                IsSelect = true;
            }
            if (curState.RightButton == ButtonState.Pressed && IsChecked(curState) && IsSelect)
            {
                IsSelect = false;
            }
            if(curState.LeftButton == ButtonState.Pressed && IsChecked(curState))

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, CurPosition, Color.White);
        }

        #endregion

        #region Properties

        Rectangle CurPosition { get; set; }
         
        Texture2D Texture
        {
            get { return texture; }
            // Постораться убрать
            set { texture = value; }
        }

        bool IsSelect { get; set; } = false;

        #endregion

        #region Fields

        Texture2D texture;
        
        #endregion
    }
}
