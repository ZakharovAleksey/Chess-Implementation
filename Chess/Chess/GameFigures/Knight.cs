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
    class Knight : Figure
    {

        public Knight(int indexY, int indexX) : base(indexY, indexX) { }


        #region Methods

        public override void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(@"figures/Knight_White");
        }

        // Вычисляет позиции куда может пойти пешка
        public override void GetPossiblePositions(List<IndexPair> possibleSteps)
        {
            IndexPair curStep = new IndexPair();
            for (int curStepId = 0; curStepId < 8; ++curStepId)
            {
                curStep.IndexY = IndexY + stepsY[curStepId];
                curStep.IndexX = IndexX + stepsX[curStepId];

                if (curStep.IndexX >= 0 && curStep.IndexX < GC.BoardSize && curStep.IndexY >= 0 && curStep.IndexY < GC.BoardSize)
                {
                    possibleSteps.Add(curStep);
                }
            }
        }

        #endregion

        // Массивы соответствующие индесксы которых задают возможные положения для хода коня
        readonly int[] stepsX = { -2, -1, 1, 2, 2, 1, -1, -2 };
        readonly int[] stepsY = { -1, -2, -2, -1, 1, 2, 2, 1 };
    }
}
