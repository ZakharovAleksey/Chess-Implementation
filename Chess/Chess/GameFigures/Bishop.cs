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
    class Bishop : Figure
    {
        #region Construcor

        public Bishop(int indexY, int indexX) : base(indexY, indexX) { }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(@"figures/Bishop_White");
        }

        // Вычисляет позиции куда может пойти пешка
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {
            int Y = IndexY;
            int X = IndexX;
            IndexPair posStep = new IndexPair();

            for (int i = 1; i < GC.BoardSize; ++i)
            {
                posStep.IndexY = IndexY - i;
                posStep.IndexX = IndexX - i;
                if (posStep.IndexX >= 0 && posStep.IndexX < GC.BoardSize && posStep.IndexY >= 0 && posStep.IndexY < GC.BoardSize)
                    possibleSteps.Add(posStep);

                posStep.IndexY = IndexY + i;
                posStep.IndexX = IndexX + i;
                if (posStep.IndexX >= 0 && posStep.IndexX < GC.BoardSize && posStep.IndexY >= 0 && posStep.IndexY < GC.BoardSize)
                    possibleSteps.Add(posStep);

                posStep.IndexY = IndexY + i;
                posStep.IndexX = IndexX - i;
                if (posStep.IndexX >= 0 && posStep.IndexX < GC.BoardSize && posStep.IndexY >= 0 && posStep.IndexY < GC.BoardSize)
                    possibleSteps.Add(posStep);

                posStep.IndexY = IndexY - i;
                posStep.IndexX = IndexX + i;
                if (posStep.IndexX >= 0 && posStep.IndexX < GC.BoardSize && posStep.IndexY >= 0 && posStep.IndexY < GC.BoardSize)
                    possibleSteps.Add(posStep);
            }
        }

        #endregion
    }
}
