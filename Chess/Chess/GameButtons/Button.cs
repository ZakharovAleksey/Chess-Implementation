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
    enum ColorBorder
    {
        UP = 255,
        DOWN = 0
    }
    
    // Класс являющийся базовым для всех кнопок и иконок
    abstract class Button
    {
        public Button(int posY, int posX)
        {
            this.PositionY = posY;
            this.PositionX = posX;

            OnScreenPos = new Rectangle(PositionX, PositionY, GC.MMBtnWidth, GC.MMBtnHeight);
        }

        #region Methods

        // Метод отрабатывает нажатие на кнопку и изменение ее цвета при наведении на мышь кнопкой
        public void Update(MouseState curMouseState, Game1 game)
        {
            Rectangle mouseRect = new Rectangle(curMouseState.X, curMouseState.Y, 1, 1);

            // Если мышь пересекает область кнопки то делаем ее обновление
            if (mouseRect.Intersects(OnScreenPos))
            {
                // Определяем как будет изменяться глубина цвета - увеличиваться или уменьшаться
                if (btnCurColor.A >= (int)ColorBorder.UP)
                    IsBtnColorReduce = true;
                else if (btnCurColor.A <= (int)ColorBorder.DOWN)
                    IsBtnColorReduce = false;

                // Уменьшаем или увеличиваем соответственно цвет кнопки
                if (IsBtnColorReduce)
                    btnCurColor.A -= GC.BtnColorDepthInc;
                else
                    btnCurColor.A += GC.BtnColorDepthInc;

                // Если нажали на кнопку
                if (curMouseState.LeftButton == ButtonState.Pressed)
                {
                    IsClicked = true;
                    OnButtonClick(game);
                }
            }
            // Когда убрали мышь цвет должен вернуться в исходное состояние
            else if (btnCurColor.A < (int)ColorBorder.UP)
            {
                btnCurColor.A += GC.BtnColorDepthInc;
                IsClicked = false;
            }
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, OnScreenPos, btnCurColor);
        }

        // Назначает кнопке новое положение
        public virtual void SetPosition(int positionY, int positionX)
        {
            PositionY = positionY;
            PositionX = positionX;

            OnScreenPos = new Rectangle(PositionX, PositionY, GC.MMBtnWidth, GC.MMBtnHeight);
        }

        public virtual void LoadContent(ContentManager Content) { }

        // Опрделеляет действие по нажатию на данную кнопку
        public virtual void OnButtonClick(Game1 game) { }

        #endregion

        #region Fields

        // Координаты положения кнопки на экране
        protected int PositionY { get; set; }
        protected int PositionX { get; set; }

        // Квадрат который определяет положение кнопки на экране
        protected Rectangle OnScreenPos { get; set; }

        // Определяет нажата ли кнопка
        protected bool IsClicked { get; set; } = false;
        // Показывает убывает или возрастает глубина цвета для текущей кнопки
        protected bool IsBtnColorReduce { get; set; } = true;
        // Текущий цвет кнопки
        protected Color btnCurColor = new Color(255, 255, 255, 255);
        // Текстура с надписью кнопки
        protected Texture2D Texture { get; set; } = null;

        #endregion
    }
}
