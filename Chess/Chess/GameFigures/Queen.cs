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
    class Queen : Figure
    {
        #region Construcor

        public Queen(int indexY, int indexX) : base(indexY, indexX) { }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(@"figures/Queen_White");
        }

        // Вычисляет позиции куда может пойти пешка
        public override void GetPossiblePositions(List<IndexPair> possibleSteps)
        {
            IndexPair posStep = new IndexPair();
            {
                int Y = IndexY;
                int X = IndexX;
                

                // Как для слона

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
            // Как для ладьи

            for (int X = 0; X < GC.BoardSize; ++X)
            {
                if (X != IndexX)
                {
                    posStep.IndexY = IndexY;
                    posStep.IndexX = X;

                    possibleSteps.Add(posStep);
                }
            }

            for (int Y = 0; Y < GC.BoardSize; ++Y)
            {
                if (Y != IndexY)
                {
                    posStep.IndexY = Y;
                    posStep.IndexX = IndexX;

                    possibleSteps.Add(posStep);
                }
            }
        }

        #endregion
    }

}
