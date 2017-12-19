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

        Game game;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Engine(Game game) : base(game)
        {
            this.game = game;
            this.game.Components.Add(this);

            graphics = new GraphicsDeviceManager(this.game);
            this.game.Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.F4))
                game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
        }
    }
}
