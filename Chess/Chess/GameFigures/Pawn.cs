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
    /// Имплементация класса пешки
    /// </summary>
    class Pawn
    {
        #region Construcor

        public Pawn(int indexY, int indexX)
        {
            this.IndexY = indexY;
            this.IndexX = indexX;
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            PawnTexture = Content.Load<Texture2D>(@"figures/pawn");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawPos = new Rectangle(GC.IndentLeft + IndexX * GC.CellHeight, GC.IndentTop + IndexY * GC.CellWidth, GC.CellWidth, GC.CellHeight);
            spriteBatch.Draw(PawnTexture, drawPos, Color.White);
        }

        #endregion

        #region Properties

        // Index on the pawn on the chessboard
        int IndexX { get; set; }
        int IndexY { get; set; }

        bool IsChoosen { get; set; } = false;


        // Drawing fields
        Texture2D PawnTexture { get; set; }
        
        #endregion
    }
}
