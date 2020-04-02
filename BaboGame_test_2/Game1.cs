using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;

namespace BaboGame_test_2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont _font;
        Debugger debugger;




        private List<Character> characterSprites;           // Personatges (inclòs el jugador)
        private List<Projectile> projectileSprites;         // Projectils, creats per projectileEngine
        private List<Sprite> overlaySprites;                // Sprites de la UI, de moment només la mira

        ProjectileEngine projectileEngine;
        InputManager inputManager = new InputManager(Keys.W, Keys.S, Keys.A, Keys.D); // El passem ja inicialitzat als objectes

        Character playerChar;                               // Punter cap al character controlat pel jugador
        Texture2D projectileTexture;                        // Textura per instanciar projectils

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
            base.Initialize();
            projectileSprites = new List<Projectile>();
            projectileEngine = new ProjectileEngine(projectileSprites);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Dictionary<string, Animation> slugAnimations = new Dictionary<string, Animation>()
            {
                {"Babo down0", new Animation(Content.Load<Texture2D>("Babo/Babo down0"), 6) },
                {"Babo up0", new Animation(Content.Load<Texture2D>("Babo/Babo up0"), 6) },
                {"Babo down0 bck", new Animation(Content.Load<Texture2D>("Babo/Babo down0 s0"), 1) },
                {"Babo up0 bck", new Animation(Content.Load<Texture2D>("Babo/Babo up0 s0"), 1) },
                {"Babo right0", new Animation(Content.Load<Texture2D>("Babo/Babo right0"), 6) },
                {"Babo left0", new Animation(Content.Load<Texture2D>("Babo/Babo left0"), 6) },
                {"Babo down22_5", new Animation(Content.Load<Texture2D>("Babo/Babo down22_5"), 6) },
                {"Babo up22_5", new Animation(Content.Load<Texture2D>("Babo/Babo up22_5"), 6) },
                {"Babo right22_5", new Animation(Content.Load<Texture2D>("Babo/Babo right22_5"), 6) },
                {"Babo left22_5", new Animation(Content.Load<Texture2D>("Babo/Babo left22_5"), 6) },
                {"Babo down45", new Animation(Content.Load<Texture2D>("Babo/Babo down45"), 6) },
                {"Babo up45", new Animation(Content.Load<Texture2D>("Babo/Babo up45"), 6) },
                {"Babo right45", new Animation(Content.Load<Texture2D>("Babo/Babo right45"), 6) },
                {"Babo left45", new Animation(Content.Load<Texture2D>("Babo/Babo left45"), 6) },
                {"Babo down-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo down-22_5"), 6) },
                {"Babo up-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo up-22_5"), 6) },
                {"Babo right-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo right-22_5"), 6) },
                {"Babo left-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo left-22_5"), 6) },
                {"Babo down-45", new Animation(Content.Load<Texture2D>("Babo/Babo down-45"), 6) },
                {"Babo up-45", new Animation(Content.Load<Texture2D>("Babo/Babo up-45"), 6) },
                {"Babo right-45", new Animation(Content.Load<Texture2D>("Babo/Babo right-45"), 6) },
                {"Babo left-45", new Animation(Content.Load<Texture2D>("Babo/Babo left-45"), 6) },
                {"Babo up hit", new Animation(Content.Load<Texture2D>("Babo/Babo up hit"), 1) },
                {"Babo down hit", new Animation(Content.Load<Texture2D>("Babo/Babo down hit"), 1) },

            };

            var sightAnimation = new Dictionary<string, Animation>()
            {
                {"ON", new Animation(Content.Load<Texture2D>("Sight/Sight_on"), 1) },
                {"OFF", new Animation(Content.Load<Texture2D>("Sight/Sight_off"), 1) },
            };

            var slugTexture = Content.Load<Texture2D>("Babo/Babo down0 s0");
            var sightTexture = Content.Load<Texture2D>("Sight/Sight_off");

            projectileTexture = Content.Load<Texture2D>("Babo/Babo down hit");

            characterSprites = new List<Character>()
            {
                new Character(slugAnimations)
                {
                    Position = new Vector2(100,100),
                    Scale = 0.25f,
                    HitBoxScale = 0.6f,
                    IDcharacter = 1,
                },

                 new Character(slugAnimations)
                {
                    Position = new Vector2(300,300),
                    Scale = 0.25f,
                    HitBoxScale = 0.6f,
                    _color = Color.Silver,
                    IDcharacter = 2,
                },
             };

            // La mira necessita que li passem inputManager per obtenir la posició del ratolí
            overlaySprites = new List<Sprite>()
            {
                new SightWeapon(sightAnimation, inputManager)
                {
                    Position = new Vector2(100,100),
                    Scale = 0.2f,
                    SolidObject = false,
                    Layer = 1f,
                },
            };
                
            // punter que apunta al personatge controlat pel jugador
            playerChar = characterSprites.ToArray()[0];
            _font = Content.Load<SpriteFont>("Font");
            debugger = new Debugger(characterSprites,projectileSprites,overlaySprites,_font);


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

            // Detectem inputs al teclat
            inputManager.detectKeysPressed();

            // Actualitzem direcció i moviment del playerChar segons els inputs
            playerChar.Direction = VectorOps.UnitVector(inputManager.GetMousePosition() - playerChar.Position);
           
            if (inputManager.RightCtrlActive())
            {
                playerChar.MoveRight();
            }
            if (inputManager.LeftCtrlActive())
            {
                playerChar.MoveLeft();
            }
            if (inputManager.UpCtrlActive())
            {
                playerChar.MoveUp();
            }
            if (inputManager.DownCtrlActive())
            {
                playerChar.MoveDown();
            }

            // llançem projectils segons els inputs del jugador
            inputManager.DetectMouseClicks();
            if (inputManager.LeftMouseClick())
            {
                Vector2 projOrigin = playerChar.Position;
                Vector2 projTarget = inputManager.GetMousePosition();
                int shooterID = 1; // caldrà gestionar els ID's des del server
                projectileEngine.AddProjectile(projOrigin, projTarget, projectileTexture, shooterID);
            }

            // Això hauria de moure els projectils, calcular les colisions i notificar als characters si hi ha hagut dany.
            projectileEngine.UpdateProjectiles(gameTime, characterSprites);

            foreach (var character in characterSprites.ToArray())
            {
                character.Update(gameTime, characterSprites);
            }

            foreach (var overlay in this.overlaySprites)
            {
                overlay.Update(gameTime, overlaySprites);
            }

            PostUpdate();
            base.Update(gameTime);
        }

        //Funció per definir la mort dels objectes
        private void PostUpdate()
        {
             for (int i = 0; i < characterSprites.Count; i++)
            {
                if (characterSprites[i].IsRemoved)
                {
                    characterSprites.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < projectileSprites.Count; i++)
            {
                if (projectileSprites[i].IsRemoved)
                {
                    projectileSprites.RemoveAt(i);
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
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.LinearWrap);
            //spriteBatch.Begin(SpriteSortMode.FrontToBack);

            debugger.DrawText(spriteBatch);
            foreach (var sprite in characterSprites)
            {
                sprite.Draw(spriteBatch);
            }

            foreach (var sprite in projectileSprites)
            {
                sprite.Draw(spriteBatch);
            }

            foreach (var overlay in overlaySprites)
            {
                overlay.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
