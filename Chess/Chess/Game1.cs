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
using System.Xml.Serialization;
using System.IO;

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
        #region Fields

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Класс Реализующий игровую доску
        ChessBoard chessBoard;
        // Цвет победившего игрока
        public static int WinnerColor { get; set; }

        // Текущее и предыдущее состояния меню игр
        public int CurGameState { get; set; } = (int)GameState.MAIN_MENU;
        public int PrevGameState { get; set; }

        // Классы различных меню игры
        MainMenu mainMenu;
        GameMenu gameMenu;
        PauseMenu pauseMenu;
        WinMenu winMenu;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = GameConstants.WindowWidth;
            graphics.PreferredBackBufferHeight = GameConstants.WindowHeight;

            chessBoard = new ChessBoard();

            mainMenu = new MainMenu();
            gameMenu = new GameMenu();
            pauseMenu = new PauseMenu();
            winMenu = new WinMenu();
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
            chessBoard.LoadContent(Content);
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

            // В зависимости от текущего состояния игры выбираем ту или иную ветвь
            switch (CurGameState)
            {
                case (int)GameState.MAIN_MENU:
                    mainMenu.Update(curMouseState, this);
                    break;
                case (int)GameState.EXECUTION:
                    if (chessBoard.IsCheckMate)
                    {
                        // Определяем цвет победителя и переходим в соответствующий раздел игрового меню
                        WinnerColor = (chessBoard.IsBlackMove) ? (int)GameWinner.WHITE : (int)GameWinner.BLACK;
                        CurGameState = (int)GameState.WIN;
                        break;
                    }
                    chessBoard.Update(gameTime);
                    gameMenu.Update(curMouseState, this);
                    break;
                case (int)GameState.PAUSE:
                    pauseMenu.Update(curMouseState, this);
                    if (PauseMenu.IsSaveBtnClicked)
                    {
                        // Попытка сохранить текущее состояние игры
                        ChessBoard.SaveInXML(ref chessBoard, "saveGame");
                        // Переходим к игре
                        CurGameState = (int)GameState.EXECUTION;
                        PauseMenu.IsSaveBtnClicked = false;
                    }
                    else if (PauseMenu.IsLoadBtmClicked)
                    {
                        // Попытка сохранить текущее состояние игры
                        ChessBoard.LoadFromXML(ref chessBoard, "saveGame");
                        // Переходим к игре
                        CurGameState = (int)GameState.EXECUTION;
                        PauseMenu.IsLoadBtmClicked = false;
                    }
                    else if (PauseMenu.IsNewGameCliced)
                    {
                        // Обновляем доску и параметры начала игры
                        chessBoard.CreateNew();
                        chessBoard.SetLogicParamToInitalState();
                        // Переходим в режим игры
                        CurGameState = (int)GameState.EXECUTION;
                    }
                    break;
                case (int)GameState.WIN:
                    winMenu.Update(curMouseState, this, WinnerColor);
                    if (WinMenu.IsNewGameBtnCliced)
                    {
                        // Обновляем доску и параметры начала игры
                        chessBoard.CreateNew();
                        chessBoard.SetLogicParamToInitalState();
                        // Переходим в режим игры
                        CurGameState = (int)GameState.EXECUTION;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // В зависимости от текущего состояния игры выбираем ту или иную ветвь
            switch (CurGameState)
            {
                case (int)GameState.MAIN_MENU:
                    mainMenu.Draw(spriteBatch);
                    break;
                case (int)GameState.EXECUTION:
                    // Это чтобы отрисовыволось после победы - так ка не успевали загружать контент
                    if (PrevGameState != (int)GameState.WIN)
                    {
                        chessBoard.Draw(spriteBatch, Content);
                        gameMenu.Draw(spriteBatch);
                    }
                    break;
                case (int)GameState.PAUSE:
                    chessBoard.Draw(spriteBatch, Content);
                    pauseMenu.Draw(spriteBatch);

                    // Делаем это тут так как в противном случае не подгружается контент
                    if (PauseMenu.IsNewGameCliced)
                        PauseMenu.IsNewGameCliced = false;

                    break;
                case (int)GameState.WIN:
                    chessBoard.Draw(spriteBatch, Content);
                    winMenu.Draw(spriteBatch);

                    // Делаем это тут так как в противном случае не подгружается контент
                    if (WinMenu.IsNewGameBtnCliced)
                        WinMenu.IsNewGameBtnCliced = false;

                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
