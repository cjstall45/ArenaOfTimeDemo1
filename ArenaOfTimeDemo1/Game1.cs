using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArenaOfTimeDemo1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private VikingSprite vikingSprite1;
        private VikingSprite vikingSprite2;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            vikingSprite1 = new VikingSprite(1);
            vikingSprite2 = new VikingSprite(2);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            vikingSprite1.LoadContent(Content);
            vikingSprite2.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            vikingSprite1.Update(gameTime);
            vikingSprite2.Update(gameTime);
            
            if (vikingSprite1.Hurtbox.CollidesWith(vikingSprite2.Hurtbox))
            {
                vikingSprite1.colliding = true;
                vikingSprite2.colliding = true;
            }
            else
            {
                vikingSprite1.colliding = false;
                vikingSprite2.colliding = false;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            vikingSprite1.Draw(gameTime, spriteBatch);
            vikingSprite2.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
