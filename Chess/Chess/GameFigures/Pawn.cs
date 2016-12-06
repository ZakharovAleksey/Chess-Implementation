using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Chess.GameParameters;
using Microsoft.Xna.Framework.Input;

using GC = Chess.GameParameters.GameConstants;

namespace Chess.GameFigures
{
    /// <summary>
    /// Pawn loginc implementation.
    /// </summary>
    class Pawn : Figure
    {
        #region Construcor

        public Pawn(int indexY, int indexX, int color) : base(indexY, indexX, color) { }

        #endregion

        #region Methods

        public override void  LoadContent(ContentManager Content)
        {
            if(Color == (int)FigureColor.WHITE)
                Texture = Content.Load<Texture2D>(@"figures/pawn");
            else
                Texture = Content.Load<Texture2D>(@"figures/Pawn_Black");
        }

        // Вычисляет позиции куда может пойти пешка
        public override void GetPossiblePositions(List<IndexPair> possibleSteps, Figure[,] board)
        {
            if (Color == (int)FigureColor.WHITE)
            {
                if (IndexY > 0 && IsCellEmpty(board, IndexY - 1, IndexX))
                    possibleSteps.Add(new IndexPair(IndexY - 1, IndexX));
            }
            else
            {
                if (IndexY < GC.BoardSize && IsCellEmpty(board, IndexY + 1, IndexX))
                    possibleSteps.Add(new IndexPair(IndexY + 1, IndexX));
            }

        }

        #endregion

    }
}
