using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArenaOfTimeDemo1
{
    /// <summary>
    /// main game class
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private VikingSprite vikingSprite1;
        private VikingSprite vikingSprite2; 
        private Texture2D HealthBarShell;
        private Texture2D HealthBar;
        private Texture2D background;
        private SpriteFont font;
        
        /// <summary>
        /// base game class that sets up base settings
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
        }
        /// <summary>
        /// initalizes player 1 and 2's sprites 
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            vikingSprite1 = new VikingSprite(1);
            vikingSprite2 = new VikingSprite(2);
            base.Initialize();
        }

        /// <summary>
        /// loads content for both players sprites, the background, the text font, and the players health bars
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            vikingSprite1.LoadContent(Content);
            vikingSprite2.LoadContent(Content);
            HealthBarShell = Content.Load<Texture2D>("emptyHealthBar");
            HealthBar = Content.Load<Texture2D>("HealthBarCenter");
            background = Content.Load<Texture2D>("image without mist");
            font = Content.Load<SpriteFont>("RetroGaming");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// calls the update methods of the users sprites and checks for collisions 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            vikingSprite1.Update(gameTime);
            vikingSprite2.Update(gameTime);

            if(vikingSprite1.HealthPercent == 0 || vikingSprite2.HealthPercent == 0)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    vikingSprite1 = new VikingSprite(1);
                    vikingSprite2 = new VikingSprite(2);
                    vikingSprite1.LoadContent(Content);
                    vikingSprite2.LoadContent(Content);
                }
            }
            //check collisions

            if (vikingSprite1.Hurtbox.Bounds.CollidesWith(vikingSprite2.Hurtbox.Bounds))
            {
                vikingSprite1.CollidingRight = true;
                vikingSprite2.CollidingLeft = true;
            }
            else
            {
                vikingSprite1.CollidingRight = false;
                vikingSprite2.CollidingLeft = false;
            }

            if(vikingSprite1.Hurtbox.Bounds.Left < 20 || vikingSprite1.Hurtbox.Bounds.Right > 620)
            {
                vikingSprite1.CollidingLeft = true;
            }
            else
            {
                vikingSprite1.CollidingLeft = false;
            }

            if (vikingSprite2.Hurtbox.Bounds.Left < 20 || vikingSprite2.Hurtbox.Bounds.Right > 620)
            {
                vikingSprite2.CollidingRight = true;
            }
            else
            {
                vikingSprite2.CollidingRight = false;
            }

            if (vikingSprite1.Hurtbox.Active && vikingSprite2.Attack1Hitbox.Active && vikingSprite1.Hurtbox.Bounds.CollidesWith(vikingSprite2.Attack1Hitbox.Bounds))
            {
                vikingSprite1.Hit();
            }
            if (vikingSprite2.Hurtbox.Active && vikingSprite1.Attack1Hitbox.Active && vikingSprite2.Hurtbox.Bounds.CollidesWith(vikingSprite1.Attack1Hitbox.Bounds))
            {
                vikingSprite2.Hit();
            } 
            base.Update(gameTime);
        }

        /// <summary>
        /// calls both players sprites draw functions draws the health bars for both players and displays text instructing how to restart the game if one player wins 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront);
            vikingSprite1.Draw(gameTime, spriteBatch);
            vikingSprite2.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(HealthBarShell, new Vector2(20, 5) ,null , Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(HealthBar, new Vector2(23, 9), new Rectangle(0, 0, (int)(274 * vikingSprite1.HealthPercent) , 27), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(HealthBarShell, new Vector2(340, 5), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(HealthBar, new Vector2(343, 9), new Rectangle(0, 0, (int)(274 * vikingSprite2.HealthPercent) , 27), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(background, new Vector2(0, 30), null, Color.White, 0, Vector2.Zero, 1.15f, SpriteEffects.None, 1f);
            if(vikingSprite1.HealthPercent == 0)
            {
                spriteBatch.DrawString(font, "Player 2 wins!", new Vector2(190, 200), Color.Red);
                spriteBatch.DrawString(font, "press r or start to play again", new Vector2(20, 250), Color.Red);
            }
            if (vikingSprite2.HealthPercent == 0)
            {
                spriteBatch.DrawString(font, "Player 1 wins!" , new Vector2(190, 200), Color.Red);
                spriteBatch.DrawString(font, "press r or start to play again", new Vector2(20, 250), Color.Red);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
