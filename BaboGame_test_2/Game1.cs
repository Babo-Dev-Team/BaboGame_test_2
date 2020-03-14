using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaboGame_test_2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
 
        private float pointerScale;
        private Vector2 pointerOrigin;
        private Texture2D pointerTexture;//Albert
        private Vector2 pointerPosition;//Albert2
        private Texture2D characterTexture;
        private Vector2 characterPosition;//Albert2

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
            pointerScale = 0.1f; // scale factor for the pointer
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
            // origin point in the center of the object 
            pointerTexture = Content.Load<Texture2D>("Babo down0 s0");//Albert3  
            pointerOrigin = new Vector2(pointerTexture.Width / 2, pointerTexture.Height / 2);
            pointerPosition = new Vector2(0, 0);//Albert4
            characterTexture = Content.Load<Texture2D>("Babo down0 s0");
            characterPosition = new Vector2(0, 0);//Albert4
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            // TODO: Add your update logic here

            /* if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                 Exit();
                 */
            if (Keyboard.GetState().IsKeyDown(Keys.W)) //Albert8
             {
                 characterPosition.Y -= 3;
             }

             if (Keyboard.GetState().IsKeyDown(Keys.S)) //Albert9
             {
                 characterPosition.Y += 3;
             }

             if (Keyboard.GetState().IsKeyDown(Keys.A)) //Albert10
             {
                 characterPosition.X -= 3;
             }

             if (Keyboard.GetState().IsKeyDown(Keys.D)) //Albert11
             {
                 characterPosition.X += 3;
             }

            pointerPosition = new Vector2 (Mouse.GetState().Position.X,
                                    Mouse.GetState().Position.Y); 
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

            // draws the pointer at position with scale pointerScale applied from pointerOrigin
            spriteBatch.Draw(pointerTexture, pointerPosition, null, Color.White, 0.0f,
            pointerOrigin, pointerScale, SpriteEffects.None, 0);

            //spriteBatch.Draw(_pointerTexture, _pointerPosition);
            spriteBatch.Draw(characterTexture, characterPosition);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
