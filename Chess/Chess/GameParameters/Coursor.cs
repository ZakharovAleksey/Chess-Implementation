using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.GameParameters
{
    enum CoursorType
    {
        IDLE = 0,
        PICK = 1,
        WAIT = 2,
    }

    class Coursor
    {
        #region Fields

        Texture2D[] statesArray;
        Vector2 currentPosition = new Vector2(GameConstants.WindowWidth / 2, GameConstants.WindowHeight / 2);


        int StatesCount { get; set; } = 0;
        int CurrentState { get; set; } = (int)CoursorType.IDLE;

        #endregion

        #region Constructor

        public Coursor(int statesCount)
        {
            StatesCount = statesCount;
            statesArray = new Texture2D[StatesCount];
        }

        #endregion

        #region Methods

        void LoadContent(ContentManager Content)
        {
            statesArray[(int)CoursorType.IDLE] = Content.Load<Texture2D>(@"coursor/coursorIDLE");
            statesArray[(int)CoursorType.PICK] = Content.Load<Texture2D>(@"coursor/coursorPICK");
            statesArray[(int)CoursorType.WAIT] = Content.Load<Texture2D>(@"coursor/coursorWAIT");
        }


        void Draw(SpriteBatch spriteBatch)
        {

        }

        #endregion


    }
}
