using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        bool cursorInCell()
        {
            return (Mouse.GetState().Y > Position.Top && Mouse.GetState().Y < Position.Bottom && Mouse.GetState().X > Position.Left && Mouse.GetState().Y < Position.Right) ? true : false;
        }

        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if(Mouse.GetState().Y > Position.Top)
                    if(Mouse.GetState().Y < Position.Bottom)
                        if(Mouse.GetState().X > Position.Left)
                            if(Mouse.GetState().Y < Position.Right)
                CurrentState = (int)CellState.SELECT;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(StatesArray[CurrentState], Position, Color.White);
        }

    }
}
