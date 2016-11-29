using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.GameParameters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chess.GameFigures
{
    enum Player
    {
        FIRST = 0,
        SECOND = 1,
    }

    /// <summary>
    /// Тут будут заданы все фигуры для соответствующей команды:
    /// То есть все пешки, по два слона, по два ладьи, по два коня, по ферзю и по королю
    /// </summary>
    class Figures
    {
        #region Constructor

        public Figures()
        {
            for (int pawnID = 0; pawnID < pawnsNumber; ++pawnID)
                PawnsArray[pawnID] = new Pawn(new Vector2(GameConstants.IndentLeft + pawnID * GameConstants.CellWidth, GameConstants.IndentTop + (GameConstants.BoardSize - 2) * GameConstants.CellHeight));
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content)
        {
            foreach (Pawn curPawn in PawnsArray)
                curPawn.LoadContent(Content);
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Pawn curPawn in PawnsArray)
                curPawn.Draw(spriteBatch);
        }

        #endregion

        #region Properties

        const int pawnsNumber = GameConstants.BoardSize;

        Pawn[] PawnsArray { get; set; } = new Pawn[pawnsNumber];

        

        #endregion
    }
}
