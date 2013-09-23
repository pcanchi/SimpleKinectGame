using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame4
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
 
        Texture2D ball;
        Vector2 ballPos = Vector2.Zero;
        Vector2 ballSpeed = new Vector2(300, 300);

        Texture2D paddle;
        Vector2 paddlePos;

        int count;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            count = 0;
            paddlePos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - paddle.Width/2,
	        graphics.GraphicsDevice.Viewport.Height - paddle.Height); 
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball = Content.Load<Texture2D>("ball");
            paddle = Content.Load<Texture2D>("index");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            ballPos += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            int Xmax = GraphicsDevice.Viewport.Width - ball.Width;
            int Ymax = GraphicsDevice.Viewport.Height - ball.Height;

            if(ballPos.X > Xmax || ballPos.X < 0)
                ballSpeed.X *= -1;
           
            if (ballPos.Y < 0)
                ballSpeed.Y *= -1;
            
            else if (ballPos.Y > Ymax)
            {
                count = 0;
                ballPos.Y = 0;
                ballSpeed.X = 150;
                ballSpeed.Y = 150;
            }

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Right))
                paddlePos.X += 10;
            
            else if (state.IsKeyDown(Keys.Left))
                paddlePos.X -= 10;



            Rectangle ballRect = new Rectangle((int)ballPos.X, (int)ballPos.Y, paddle.Width, paddle.Height);
            Rectangle paddleRect = new Rectangle((int)paddlePos.X, (int)paddlePos.Y, paddle.Width, paddle.Width);

            if (ballRect.Intersects(paddleRect))
            {
                ballSpeed.Y += 20;
                count++;
                if (ballSpeed.X < 0)
                    ballSpeed.X -= 20;
                
                else
                    ballSpeed.X += 20;
                
                ballSpeed.Y *= -1;
            }
         
        base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            spriteBatch.Draw(ball, ballPos, Color.Yellow);
            spriteBatch.Draw(paddle, paddlePos, Color.CornflowerBlue);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
