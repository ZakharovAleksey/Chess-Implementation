using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using Chess.GameParameters;
using Chess.GameUnits;
using Chess.GameFigures;

using Chess.GameButtons.GameMenu;


using Chess.GameButtons;

namespace Chess
{
    enum GameState
    {
        MAIN_MENU = 0,
        EXECUTION = 1
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ChessBoard board;
        MainMenu mainMenu;

        GameMenu gameMenu;


        public int CurGameState { get; set; } = (int) GameState.MAIN_MENU;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = GameConstants.WindowWidth;
            graphics.PreferredBackBufferHeight = GameConstants.WindowHeight;

            board = new ChessBoard();
            gameMenu = new GameMenu();


            mainMenu = new MainMenu();
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            board.LoadContent(Content);
            gameMenu.LoadContent(Content);


            mainMenu.LoadContent(Content);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            MouseState curMouseState = Mouse.GetState();

            switch (CurGameState)
            {
                case (int)GameState.MAIN_MENU:
                    mainMenu.Update(curMouseState, this);
                    break;
                case (int)GameState.EXECUTION:
                    board.Update(gameTime);
                    gameMenu.Update(curMouseState, this);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            switch (CurGameState)
            {
                case (int)GameState.MAIN_MENU:
                    mainMenu.Draw(spriteBatch);
                    break;
                case (int)GameState.EXECUTION:
                    board.Draw(spriteBatch, Content);
                    gameMenu.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
