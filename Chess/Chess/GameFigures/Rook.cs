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

        public Rook(int indexY, int indexX) : base(indexY, indexX) { }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(@"figures/Rook_White");
        }

        // Вычисляет позиции куда может пойти ладья
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {

            // Проверем возможные движения вдоль оси X до первой попавшейся фигуры
            int curX = IndexX - 1;
            while (curX >= 0 && IsCellEmpty(board, IndexY, curX))
            {
                possibleSteps.Add(new IndexPair(IndexY, curX--));
            }

            curX = IndexX + 1;
            while (curX < GC.BoardSize && IsCellEmpty(board, IndexY, curX))
            {
                possibleSteps.Add(new IndexPair(IndexY, curX++));
            }

            // Проверем возможные движения вдоль оси Y до первой попавшейся фигуры
            int curY = IndexY - 1;
            while (curY >= 0 && IsCellEmpty(board, curY, IndexX))
            {
                possibleSteps.Add(new IndexPair(curY--, IndexX));
            }

            curY = IndexY + 1;
            while (curY < GC.BoardSize && IsCellEmpty(board, curY, IndexX))
            {
                possibleSteps.Add(new IndexPair(curY++, IndexX));
            }
        }

        #endregion
    }
}
