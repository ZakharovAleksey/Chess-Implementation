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

        // Вычисляет позиции куда может пойти пешка
        public override void GetPossiblePositions(List<IndexPair> possibleSteps)
        {
            IndexPair posStep = new IndexPair();
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
