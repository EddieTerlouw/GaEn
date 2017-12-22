using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GaEn
{
    public sealed class Engine : DrawableGameComponent
    {
        #region Singleton
        private static volatile Engine instance;
        private static object sync = new object();
        public static Engine Instance(Game game)
        {
            if (instance == null)
            {
                lock (sync)
                {
                    if (instance == null)
                        instance = new Engine(game);
                }
            }
            return instance;
        }
        #endregion

        private Game game;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        #region Properties
        public Input.InputManager InputManager { get { return Input.InputManager.Instance; } }
        #endregion

        private Engine(Game game) : base(game)
        {
            this.game = game;
            this.game.Components.Add(this);

            Setup();
        }

        private void Setup()
        {
            graphics = new GraphicsDeviceManager(game);
            game.Content.RootDirectory = "Content";

            var action = new Input.InputAction("KILLGAME") { Type = Input.InputAction.InputActionTypes.All };
            action.Add(Keys.LeftAlt);
            action.Add(Keys.Q);
            InputManager.AddAction(action);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);

            if (InputManager["KILLGAME"].IsDown())
                game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
        }
    }
}
