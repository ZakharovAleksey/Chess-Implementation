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

using GC = Chess.GameParameters.GameConstants;

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

        public Cell(int indexY, int indexX, int cellType)
        {
            this.IndexY = indexY;
            this.IndexX = indexX;

            Type = cellType;
            ScreenPos = new Rectangle(GC.IndentLeft + IndexX * GC.CellHeight, GC.IndentTop + IndexY * GC.CellWidth, GC.CellWidth, GC.CellHeight);
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

        bool IsInCell(MouseState curMouseState)
        {
            return (curMouseState.Y> ScreenPos.Top && curMouseState.Y < ScreenPos.Bottom  && curMouseState.X > ScreenPos.Left && curMouseState.X < ScreenPos.Right) ? true : false;
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
            spriteBatch.Draw(StatesArray[CurrentState], ScreenPos, Color.White);
        }

        #endregion

        public void SetStateIDLE()
        {
            CurrentState = (int)CellState.IDLE;
        }

        public void SetStateSelect()
        {
            CurrentState = (int)CellState.SELECT;
        }


        #region Fields


        int CurrentState { get; set; } = 0;
        static int StatesCount { get; } = 3;

        int Type { get; set; }


        Texture2D[] StatesArray { get; set; } = new Texture2D[StatesCount];
        Rectangle ScreenPos { get; set; }
        

        int IndexX { get; set; }
        int IndexY { get; set; }

        #region Actions

        // True if cell is selected, false otherwise [Means player click on it with left button]
        public bool IsSelect { get; set; } = false;

        #endregion

        #endregion

    }
}
