using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Chess.GameParameters;

namespace Chess.GameUnits
{
    enum CellType
    {
        WHITE = 0,
        BLACK = 1,
    } 

    enum CellState
    {
        IDLE = 0, 
        SELECT = 1,
        POSSIBLE = 2
    }

    class Cell
    {
        #region Fields

        int CurrentState { get; set; }
        static int StatesCount { get; } = 3;
        int Type { get; set; }
        

        Texture2D[] StatesArray { get; set; } = new Texture2D[StatesCount];
        Rectangle Position { get; set; }


        public static int Width { get; } = 100;
        public static int Height { get; } = 100;

        #endregion

        public Cell(int cellType, Rectangle position)
        {
            Type = cellType;
            Position = position;
        }

        #region Properties

        #endregion

        public void LoadContent(ContentManager Content)
        {
            if(Type == (int)CellType.BLACK)
                StatesArray[(int)CellState.IDLE] = Content.Load<Texture2D>(@"cell/black");
            else
                StatesArray[(int)CellState.IDLE] = Content.Load<Texture2D>(@"cell/white");

            StatesArray[(int)CellState.SELECT] = Content.Load<Texture2D>(@"cell/checked");
            StatesArray[(int)CellState.POSSIBLE] = Content.Load<Texture2D>(@"cell/possible");
        }

        bool isInCell(MouseState currentMouseState)
        {
            return (currentMouseState.Y> Position.Top && currentMouseState.Y < Position.Bottom && currentMouseState.X > Position.Left && currentMouseState.X < Position.Right) ? true : false;
        }

        bool isInScreen(MouseState currentMouseState)
        {
            return (currentMouseState.X >= 0 && currentMouseState.X <= GameConstants.WindowWidth && currentMouseState.Y >= 0 && currentMouseState.Y <= GameConstants.WindowHeight) ? true : false;
        }

        public void Update()
        {
            MouseState currentMouseState = Mouse.GetState();

            if (isInScreen(currentMouseState) && isInCell(currentMouseState))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    CurrentState = (int)CellState.SELECT;
                else if (currentMouseState.RightButton == ButtonState.Pressed)
                    CurrentState = (int)CellState.IDLE;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(StatesArray[CurrentState], Position, Color.White);
        }

    }
}
