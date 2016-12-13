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

using System.IO;
using System.Xml.Serialization;

using System.Runtime.Serialization;


using Chess.GameParameters;

using GC = Chess.GameParameters.GameConstants;

namespace Chess.GameUnits
{
    // Возможные цвета клеток
    enum CellType
    {
        WHITE = 0,
        BLACK = 1,
    }

    // Возможные состояния клеток
    enum CellState
    {
        IDLE = 0,       // Не выбрана пользователем
        SELECT = 1,     // Выбрана пользователем
        POSSIBLE = 2    // Возможна для хода пользователем (НЕ РЕАЛИЗОВАНО)
    }

    // Клетка - отображается как клетка доски (белая черная или красная если выбрана пользователем для хода)
    [DataContract]
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(StatesArray[CurrentState], ScreenPos, Color.White);
        }


        // Ставит клетку в состояние - не выбрана
        public void SetStateIDLE()
        {
            CurrentState = (int)CellState.IDLE;
            IsSelect = false;
        }

        // Ставит клетку в состояние - выбрана
        public void SetStateSELECT()
        {
            CurrentState = (int)CellState.SELECT;
            IsSelect = true;
        }

        #endregion

        #region Fields

        // Количество состояний в которых может находиться клетка
        static int StatesCount { get; } = 3;
        // Текущее состояние клетки с шахматной фигурой (или без нее)
        [DataMember]
        int CurrentState { get; set; } = 0;
        // Тип клетки - белая или черная или красная
        int Type { get; set; }

        // Индексы клетки 
        int IndexX { get; set; }
        int IndexY { get; set; }

        // Показывает выбрана ли клетка на данный момент пользователем
        public bool IsSelect { get; set; } = false;


        // Массив содержит контент для всех возможных состояний клетки
        Texture2D[] StatesArray { get; set; } = new Texture2D[StatesCount];
        // Координаты клетки на экране
        Rectangle ScreenPos { get; set; }

        #endregion

    }
}
