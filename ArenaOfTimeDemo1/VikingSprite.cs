using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaOfTimeDemo1
{
    public class VikingSprite
    {
        private AnimationState animationState;

        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;
        private float animationSpeed = 0.15f;

        private Texture2D[] idleTextures = new Texture2D[6];
        private Texture2D[] walkingTextures = new Texture2D[6];
        private Texture2D[] attackTextures = new Texture2D[6];

        private double animationTimer;

        private int animationFrame;
        private int playerNumber;
        public Vector2 Position;

        public VikingSprite(int player)
        {
            if (player == 1)
            {
                playerNumber = 1;
                Position = new Vector2(100, 250);
            }
            else
            {
                playerNumber = 2;
                Position = new Vector2(300, 250);
            }
        }

        public void LoadContent(ContentManager content)
        {
            idleTextures[0] = content.Load<Texture2D>("ready_1");
            idleTextures[1] = content.Load<Texture2D>("ready_2");
            idleTextures[2] = content.Load<Texture2D>("ready_3");
            idleTextures[3] = content.Load<Texture2D>("ready_4");
            idleTextures[4] = content.Load<Texture2D>("ready_5");
            idleTextures[5] = content.Load<Texture2D>("ready_6");
            walkingTextures[0] = content.Load<Texture2D>("walk_1");
            walkingTextures[1] = content.Load<Texture2D>("walk_2");
            walkingTextures[2] = content.Load<Texture2D>("walk_3");
            walkingTextures[3] = content.Load<Texture2D>("walk_4");
            walkingTextures[4] = content.Load<Texture2D>("walk_5");
            walkingTextures[5] = content.Load<Texture2D>("walk_6");
            attackTextures[0] = content.Load<Texture2D>("attack1_1");
            attackTextures[1] = content.Load<Texture2D>("attack1_2");
            attackTextures[2] = content.Load<Texture2D>("attack1_3");
            attackTextures[3] = content.Load<Texture2D>("attack1_4");
            attackTextures[4] = content.Load<Texture2D>("attack1_5");
            attackTextures[5] = content.Load<Texture2D>("attack1_6");
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (playerNumber == 1 && keyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
            {
                animationState = AnimationState.attack1;
                animationFrame = 0;
                animationSpeed = .125f;
            }
            if (playerNumber == 1 && (int) animationState < 4 && keyboardState.IsKeyDown(Keys.A))
            {
                Position += new Vector2((float) -1.5, 0);
                animationState = AnimationState.backingup;
            }
            else if (playerNumber == 1 && (int)animationState < 4 &&  keyboardState.IsKeyDown(Keys.D))
            {
                Position += new Vector2((float) 1.5, 0);
                animationState = AnimationState.walking;
            }
            else if (playerNumber != 1 && keyboardState.IsKeyDown(Keys.NumPad0) && previousKeyboardState.IsKeyUp(Keys.NumPad0))
            {
                animationState = AnimationState.attack1;
                animationFrame = 0;
                animationSpeed = .125f;
            }
            else if (playerNumber!= 1 && (int)animationState < 4 && keyboardState.IsKeyDown(Keys.Left))
            {
                Position += new Vector2((float)-1.5, 0);
                animationState = AnimationState.backingup;
            }
            else if (playerNumber != 1 && (int)animationState < 4 && keyboardState.IsKeyDown(Keys.Right))
            {
                Position += new Vector2((float)1.5, 0);
                animationState = AnimationState.walking;
            }
            else
            {
                if ((int)animationState < 4)
                {
                    animationState = AnimationState.idle;
                }

            }
            previousKeyboardState = keyboardState;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > animationSpeed)
            {
                animationFrame++;
                switch (animationState)
                {
                    case AnimationState.idle:
                        if (animationFrame > 5) animationFrame = 0;
                        break;
                    case AnimationState.walking:
                        if (animationFrame > 5) animationFrame = 0;
                        break;
                    case AnimationState.backingup:
                        if (animationFrame > 5) animationFrame = 0;
                        break;
                    case AnimationState.attack1:
                        if (animationFrame > 5)
                        { 
                            animationFrame = 0;
                            animationState = AnimationState.idle;
                        }
                        break;
                }
                animationTimer -= animationSpeed;
            }
            switch (animationState)
            {
                case AnimationState.idle:
                    if (playerNumber == 1)
                    {
                        spriteBatch.Draw(idleTextures[animationFrame], Position, null, Color.White, 0, new Vector2(0, 0), 3.8f, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(idleTextures[animationFrame], Position, null, Color.White, 0, new Vector2(0, 0), 3.8f, SpriteEffects.FlipHorizontally, 0);
                    }
                    break;
                case AnimationState.walking:
                    if (playerNumber == 1)
                    {
                        spriteBatch.Draw(walkingTextures[animationFrame], Position, null, Color.White, 0, new Vector2(0, 0), 3.8f, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(walkingTextures[5 - animationFrame], Position, null, Color.White, 0, new Vector2(0, 0), 3.8f, SpriteEffects.FlipHorizontally, 0);
                    }
                    break;
                case AnimationState.backingup:
                    if (playerNumber == 1)
                    {
                        spriteBatch.Draw(walkingTextures[5 - animationFrame], Position, null, Color.White, 0, new Vector2(0, 0), 3.8f, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(walkingTextures[animationFrame], Position, null, Color.White, 0, new Vector2(0, 0), 3.8f, SpriteEffects.FlipHorizontally, 0);
                    }
                    
                    break;
                case AnimationState.attack1:
                    if(playerNumber == 1)
                    {
                        spriteBatch.Draw(attackTextures[animationFrame], Position, null, Color.White, 0, new Vector2(15, 17), 3.8f, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(attackTextures[animationFrame], Position, null, Color.White, 0, new Vector2(35, 17), 3.8f, SpriteEffects.FlipHorizontally, 0);
                    }
                    break;
            }
        }
    }
}
