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

namespace Chess.GameButtons
{
    abstract class Button
    {
        public Button(int posY, int posX)
        {
            this.PositionY = posY;
            this.PositionX = posX;

            ScreenRectangle = new Rectangle(PositionX, PositionY, GC.BtnWidth, GC.BtnHeight);
        }

        public void Update(MouseState curMouseState, Game1 game)
        {
            Rectangle mouseRect = new Rectangle(curMouseState.X, curMouseState.Y, 1, 1);

            // Если мышь пересекает область кнопки то делаем ее обновление
            if (mouseRect.Intersects(ScreenRectangle))
            {
                // Определяем как будет изменяться глубина цвета - увеличиваться или уменьшаться
                if (color.A == 255)
                    IsColorDown = true;
                else if (color.A == 0)
                    IsColorDown = false;

                // Уменьшаем или увеличиваем соответственно цвет кнопки
                if (IsColorDown)
                    color.A -= GC.BtnColorDepthInc;
                else
                    color.A += GC.BtnColorDepthInc;

                // Если нажали на кнопку
                if (curMouseState.LeftButton == ButtonState.Pressed)
                {
                    IsCkicked = true;
                    OnButtonClick(game);
                }
            }
            // Когда убрали мышь цвет должен вернуться в искодное состояние
            else if (color.A < 255)
            {
                color.A += GC.BtnColorDepthInc;
                IsCkicked = false;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, ScreenRectangle, color);
        }


        public virtual void LoadContent(ContentManager Content) { }

        public virtual void OnButtonClick(Game1 game) { }

        #region Fields

        // Координаты положения кнопки на экране
        protected int PositionY { get; set; }
        protected int PositionX { get; set; }

        // Квадрат который определяет положение кнопки на экране
        protected Rectangle ScreenRectangle { get; set; }

        // Определяет нажата ли кнопка
        protected bool IsCkicked { get; set; } = false;
        // Показывает убывает или возрастает глубина цвета для текущей кнопки
        protected bool IsColorDown { get; set; } = true;
        // Текущий цвет кнопки
        protected Color color = new Color(255, 255, 255, 255);
        // Текстура с надписью кнопки
        protected Texture2D Texture { get; set; } = null;

        #endregion
    }
}
