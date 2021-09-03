using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CurvePainter
{
    public class CurvePainter : Game
    {
        private SpriteBatch spriteBatch;
        private readonly GraphicsDeviceManager graphics;
        public static Texture2D solid;
        public static SpriteFont font;

        private CurveDrawArea area;

        public CurvePainter()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";

            // enable AA
            // doesn't work :(
            /*graphics.PreferMultiSampling = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreparingDeviceSettings += Graphics_PreparingDeviceSettings;
            graphics.ApplyChanges();*/
        }

        /*private void Graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            graphics.PreferMultiSampling = true;
            e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 8;
        }*/

        protected override void Initialize()
        {
            //GraphicsDevice.PresentationParameters.MultiSampleCount = 8;
            //graphics.ApplyChanges();

            Utils.MaximizeWindow(Window, graphics);
            base.Initialize();

            area = new CurveDrawArea();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            solid = Content.Load<Texture2D>("solid");
            font = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            area.Update(gameTime);
            Input.UpdateLastVariables();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(new Color(33, 33, 33));

            spriteBatch.Begin();

            area.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
