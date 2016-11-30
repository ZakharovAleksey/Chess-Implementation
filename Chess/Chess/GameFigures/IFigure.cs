using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameFigures
{
    // Интерфейс для всех фигур на доске.
    interface IFigure
    {
        void LoadContent(ContentManager Content);

        void Draw(SpriteBatch spriteBatch);
    }
}
