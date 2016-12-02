using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Chess.GameFigures
{
    struct IndexPair
    {
        public int IndexX;
        public int IndexY;

        public IndexPair(int indexY, int indexX)
        {
            this.IndexY = indexY;
            this.IndexX = indexX;
        }

    }


    // Интерфейс для всех фигур на доске.
    interface IFigure
    {
        void LoadContent(ContentManager Content);

        void Draw(SpriteBatch spriteBatch);

        void GetPossiblePositions(List<IndexPair> possibleSteps);

        void Update(int NewIndexY, int newIndexX);
    }
}
