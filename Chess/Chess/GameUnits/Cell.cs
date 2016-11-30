using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

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

    enum CellNameLetter
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4,
        F = 5,
        G = 6,
        H = 7
    }

    class Cell
    {

        public Cell(int cellType, Rectangle position, KeyValuePair<int, int> Name)
        {
            Type = cellType;
            Position = position;

            this.Name = Name;
        }

        #region Methods 

        public void LoadContent(ContentManager Content)
        {
            if(Type == (int)CellType.BLACK)
                StatesArray[(int)CellState.IDLE] = Content.Load<Texture2D>(@"cell/black");
            else
                StatesArray[(int)CellState.IDLE] = Content.Load<Texture2D>(@"cell/white");

            StatesArray[(int)CellState.SELECT] = Content.Load<Texture2D>(@"cell/checked");
            StatesArray[(int)CellState.POSSIBLE] = Content.Load<Texture2D>(@"cell/possible");
        }

        bool IsInCell(MouseState currentMouseState)
        {
            return (currentMouseState.Y> Position.Top && currentMouseState.Y < Position.Bottom 
                && currentMouseState.X > Position.Left && currentMouseState.X < Position.Right) ? true : false;
        }

        public void Update()
        {
            MouseState currentMouseState = Mouse.GetState();

            if (IsInCell(currentMouseState))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (!IsSelect)
                    {
                        CurrentState = (int)CellState.SELECT;
                        IsSelect = true;
                    }
                }
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    if (IsSelect)
                    {
                        CurrentState = (int)CellState.IDLE;
                        IsSelect = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(StatesArray[CurrentState], Position, Color.White);
        }

        #endregion


        #region Fields

        int CurrentState { get; set; } = 0;
        static int StatesCount { get; } = 3;
        int Type { get; set; }


        Texture2D[] StatesArray { get; set; } = new Texture2D[StatesCount];
        Rectangle Position { get; set; }

        // Name of cell - for example: A1, F8
        KeyValuePair<int, int> Name = new KeyValuePair<int, int>();

        // Width and Height of cell
        public static int Width { get; } = GameConstants.CellWidth;
        public static int Height { get; } = GameConstants.CellHeight;

        #region Actions

        // True if cell is selected, false otherwise [Means player click on it with left button]
        public bool IsSelect { get; set; } = false;

        public bool IsFree { get; set; } = true;

        #endregion

        #endregion

    }
}
