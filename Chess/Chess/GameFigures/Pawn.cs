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

        public Pawn(int indexY, int indexX) : base(indexY, indexX) { }

        #endregion

        #region Methods


        public override void  LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(@"figures/pawn");
        }

        #endregion

    }
}
