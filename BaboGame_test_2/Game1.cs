using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BaboGame_test_2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Sprite> _sprites;
        
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

            var slugTexture = Content.Load<Texture2D>("Babo down0 s0");
            var sightTexture = Content.Load<Texture2D>("Sight_off");

            _sprites = new List<Sprite>()
            {
                new Character(slugTexture)
                {
                    Position = new Vector2(100,100),
                    Salt = new SaltWeapon(Content.Load<Texture2D>("Babo down hit")),
                    Scale = 0.25f,
                    HitBoxScale = 0.6f,
                    Input = new Input()
                    {
                        Left = Keys.A,
                        Right = Keys.D,
                        Up = Keys.W,
                        Down = Keys.S,
                    }
                },

                 new Character(slugTexture)
                {
                    Position = new Vector2(300,300),
                    Salt = new SaltWeapon(Content.Load<Texture2D>("Babo down hit")),
                    Scale = 0.25f,
                    HitBoxScale = 0.6f,
                    Input = new Input()
                    {
                        Left = Keys.Left,
                        Right = Keys.Right,
                        Up = Keys.Up,
                        Down = Keys.Down,
                    },
                    Effect = SpriteEffects.FlipHorizontally,
                    _color = Color.Silver,
                },

                new SightWeapon(sightTexture)
                {
                    Position = new Vector2(100,100),
                    Scale = 0.2f,
                    SolidObject = false,
                    Layer = 1f,
                },
            };
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
           if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
               Exit();

            // TODO: Add your update logic here
            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);

            PostUpdate();

            base.Update(gameTime);
        }

        //Funció per definir la mort dels objectes
        private void PostUpdate()
        {
             for (int i=0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
