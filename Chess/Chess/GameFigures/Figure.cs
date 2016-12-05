using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GC = Chess.GameParameters.GameConstants;

namespace Chess.GameFigures
{
    // Базовый абстрактный класс для всех фигур на шахматной доске.
    abstract class Figure : IFigure
    {
        // Задает начальное положение для фигуры
        public Figure(int indexY, int indexX)
        {
            this.IndexY = indexY;
            this.IndexX = indexX;
        }

        public virtual void LoadContent(ContentManager Content) { }

        // Отрисовывает фигуру в зависимости от ее положения на доске
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                Rectangle drawPos = new Rectangle(GC.IndentLeft + IndexX * GC.CellHeight, GC.IndentTop + IndexY * GC.CellWidth, GC.CellWidth, GC.CellHeight);
                spriteBatch.Draw(Texture, drawPos, Color.White);
            }
        }

        // Вычисляет все возможные позикии для хода
        public virtual void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board) { }

        protected bool IsCellEmpty(Figure[,] board, int IndexY, int IndexX)
        {
            return (board[IndexY, IndexX].GetType() == typeof(EmptyCell)) ? true : false;
        }

        #region Properties

        // y и x координата положения фигуры на доске соответственно
        protected int IndexY { get; set; }
        protected int IndexX { get; set; }

        protected bool IsChoosen { get; set; } = false;

        protected Texture2D Texture { get; set; } = null;

        #endregion
    }
}
