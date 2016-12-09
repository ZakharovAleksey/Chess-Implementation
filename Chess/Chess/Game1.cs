using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using Chess.GameParameters;
using Chess.GameUnits;
using Chess.GameFigures;

using Chess.GameButtons.GameMenu;

using Chess.GameButtons.PauseMenu;

using Chess.GameButtons.WinMenu;

using Chess.GameButtons;



namespace Chess
{
    enum GameState
    {
        MAIN_MENU = 0,
        EXECUTION = 1,
        PAUSE     = 2,
        WIN       = 3
    }

    enum GameWinner
    {
        WHITE = 0,
        BLACK = 1
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public ChessBoard board;
        MainMenu mainMenu;

        GameMenu gameMenu;
        PauseMenu pauseMenu;

        WinMenu winMenu;

        public int Winner { get; set; }

        public int CurGameState { get; set; } = (int) GameState.MAIN_MENU;
        public int PrevGameState { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = GameConstants.WindowWidth;
            graphics.PreferredBackBufferHeight = GameConstants.WindowHeight;

            board = new ChessBoard();
            gameMenu = new GameMenu();

            pauseMenu = new PauseMenu();

            winMenu = new WinMenu();

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
            winMenu.LoadContent(Content);
            pauseMenu.LoadContent(Content);
            mainMenu.LoadContent(Content);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            MouseState curMouseState = Mouse.GetState();

            PrevGameState = CurGameState;
            switch (CurGameState)
            {
                case (int)GameState.MAIN_MENU:
                    mainMenu.Update(curMouseState, this);
                    break;
                case (int)GameState.EXECUTION:
                    if (board.IsCheckMate)
                    {
                        // Определяем цвет победителя и переходим в соответствующий раздел игрового меню
                        Winner = (board.IsBlackMove) ? (int)GameWinner.WHITE : (int)GameWinner.BLACK;
                        CurGameState = (int)GameState.WIN;
                        break;
                    }
                    board.Update(gameTime);
                    gameMenu.Update(curMouseState, this);
                    break;
                case (int)GameState.PAUSE:
                    pauseMenu.Update(curMouseState, this);
                    break;
                case (int)GameState.WIN:
                    winMenu.Update(curMouseState, this);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            switch (CurGameState)
            {
                case (int)GameState.MAIN_MENU:
                    mainMenu.Draw(spriteBatch);
                    break;
                case (int)GameState.EXECUTION:
                    // Это чтобы отрисовыволось после победы - так ка не успевали загружать контент
                    if (PrevGameState != (int)GameState.WIN)
                    {
                        board.Draw(spriteBatch, Content);
                        gameMenu.Draw(spriteBatch);
                    }
                    break;
                case (int)GameState.PAUSE:
                    board.Draw(spriteBatch, Content);
                    pauseMenu.Draw(spriteBatch);
                    break;
                case (int)GameState.WIN:
                    board.Draw(spriteBatch, Content);
                    winMenu.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
