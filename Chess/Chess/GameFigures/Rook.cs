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
    class Rook : Figure
    {
        #region Construcor

        public Rook(int indexY, int indexX, int color) : base(indexY, indexX, color) { }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            if (Color == (int)FigureColor.WHITE)
                Texture = Content.Load<Texture2D>(@"figures/Rook_White");
            else
                Texture = Content.Load<Texture2D>(@"figures/Rook_Black");
        }

        // Вычисляет позиции куда может пойти ладья
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {

            // Проверем возможные движения вдоль оси X в право до первой попавшейся фигуры 
            int curX = IndexX - 1;
            while (curX >= 0 && IsCellEmpty(board, IndexY, curX))
            {
                possibleSteps.Add(new IndexPair(IndexY, curX--));
            }

            // Если первая попавшаяся фигура при движении вправо вдоль оси X вправо другого цвета то мы можем ее бить
            if(curX >= 0)
                if(IsCellOtherColor(board, IndexY, curX, this.Color))
                    possibleSteps.Add(new IndexPair(IndexY, curX));

            // Проверем возможные движения вдоль оси X в лево до первой попавшейся фигуры 
            curX = IndexX + 1;
            while (curX < GC.BoardSize && IsCellEmpty(board, IndexY, curX))
            {
                possibleSteps.Add(new IndexPair(IndexY, curX++));
            }

            // Если первая попавшаяся фигура при движении влево  вдоль оси X другого цвета то мы можем ее бить
            if(curX < GC.BoardSize - 1)
                if (IsCellOtherColor(board, IndexY, curX, this.Color))
                    possibleSteps.Add(new IndexPair(IndexY, curX));


            // Проверем возможные движения вдоль оси Y вверх до первой попавшейся фигуры
            int curY = IndexY - 1;
            while (curY >= 0 && IsCellEmpty(board, curY, IndexX))
            {
                possibleSteps.Add(new IndexPair(curY--, IndexX));
            }

            // Если первая попавшаяся фигура при движении влево  вдоль оси X другого цвета то мы можем ее бить
            if(curY >= 0)
                if (IsCellOtherColor(board, curY, IndexX, this.Color))
                    possibleSteps.Add(new IndexPair(curY, IndexX));

            // Проверем возможные движения вдоль оси Y вниз до первой попавшейся фигуры
            curY = IndexY + 1;
            while (curY < GC.BoardSize && IsCellEmpty(board, curY, IndexX))
            {
                possibleSteps.Add(new IndexPair(curY++, IndexX));
            }
            // Если первая попавшаяся фигура при движении влево  вдоль оси X другого цвета то мы можем ее бить
            if(curY < GC.BoardSize - 1)
                if (IsCellOtherColor(board, curY, IndexX, this.Color))
                    possibleSteps.Add(new IndexPair(curY, IndexX));
        }


        public override object Clone()
        {
            Rook clone = new Rook(this.IndexY, this.IndexX, this.Color);
            clone.IsChoosen = this.IsChoosen;

            return clone;
        }

        #endregion
    }
}
