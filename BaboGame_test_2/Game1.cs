﻿using Microsoft.Xna.Framework;
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

        private List<Character> characterSprites;
        private List<Projectile> projectileSprites;
        private List<Sprite> overlaySprites;
        ProjectileEngine projectileEngine;
        InputManager inputManager = new InputManager(Keys.W, Keys.S, Keys.A, Keys.D); // El passem ja inicialitzat als objectes
        Character playerChar;
        Texture2D projectileTexture;

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
            projectileSprites = new List<Projectile>();
            projectileEngine = new ProjectileEngine(projectileSprites);
            //inputManager = new InputManager(Keys.W, Keys.S, Keys.A, Keys.D);
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
                {"Babo up0", new Animation(Content.Load<Texture2D>("Babo/Babo up0 s0"), 1) },
                {"Babo right0", new Animation(Content.Load<Texture2D>("Babo/Babo right0 s0"), 1) },
                {"Babo left0", new Animation(Content.Load<Texture2D>("Babo/Babo left0 s0"), 1) },
                {"Babo down22_5", new Animation(Content.Load<Texture2D>("Babo/Babo down22_5 s0"), 1) },
                {"Babo up22_5", new Animation(Content.Load<Texture2D>("Babo/Babo up22_5 s0"), 1) },
                {"Babo right22_5", new Animation(Content.Load<Texture2D>("Babo/Babo right22_5 s0"), 1) },
                {"Babo left22_5", new Animation(Content.Load<Texture2D>("Babo/Babo left22_5 s0"), 1) },
                {"Babo down45", new Animation(Content.Load<Texture2D>("Babo/Babo down45 s0"), 1) },
                {"Babo up45", new Animation(Content.Load<Texture2D>("Babo/Babo up45 s0"), 1) },
                {"Babo right45", new Animation(Content.Load<Texture2D>("Babo/Babo right45 s0"), 1) },
                {"Babo left45", new Animation(Content.Load<Texture2D>("Babo/Babo left45 s0"), 1) },
                {"Babo down-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo down-22_5 s0"), 1) },
                {"Babo up-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo up-22_5 s0"), 1) },
                {"Babo right-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo right-22_5 s0"), 1) },
                {"Babo left-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo left-22_5 s0"), 1) },
                {"Babo down-45", new Animation(Content.Load<Texture2D>("Babo/Babo down-45 s0"), 1) },
                {"Babo up-45", new Animation(Content.Load<Texture2D>("Babo/Babo up-45 s0"), 1) },
                {"Babo right-45", new Animation(Content.Load<Texture2D>("Babo/Babo right-45 s0"), 1) },
                {"Babo left-45", new Animation(Content.Load<Texture2D>("Babo/Babo left-45 s0"), 1) },
                {"Babo up hit", new Animation(Content.Load<Texture2D>("Babo/Babo up hit"), 1) },
                {"Babo down hit", new Animation(Content.Load<Texture2D>("Babo/Babo down hit"), 1) },

            };

            var sightAnimation = new Dictionary<string, Animation>()
            {
                {"ON", new Animation(Content.Load<Texture2D>("Sight/Sight_on"), 1) },
                {"OFF", new Animation(Content.Load<Texture2D>("Sight/Sight_off"), 1) },
            };

            // TODO: use this.Content to load your game content here

            var slugTexture = Content.Load<Texture2D>("Babo/Babo down0 s0");
            var sightTexture = Content.Load<Texture2D>("Sight/Sight_off");

            projectileTexture = Content.Load<Texture2D>("Babo/Babo down hit");

            characterSprites = new List<Character>()
            {
                new Character(slugAnimations)
                {
                    Position = new Vector2(100,100),
                    Salt = new SaltWeapon(Content.Load<Texture2D>("Babo/Babo down hit")),
                    Scale = 0.25f,
                    HitBoxScale = 0.6f,
                    IDcharacter = 1,
                },

                 new Character(slugAnimations)
                {
                    Position = new Vector2(300,300),
                    Salt = new SaltWeapon(Content.Load<Texture2D>("Babo/Babo down hit")),
                    Scale = 0.25f,
                    HitBoxScale = 0.6f,
                    _color = Color.Silver,
                    IDcharacter = 2,
                },
             };

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
                 //TODO: move this out of the list
                

            // variable global que apunta a la posicio del personatge controlat pel jugador
            playerChar = characterSprites.ToArray()[0];
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

            /*if ((currentMouseState.LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released))
            {
                projectileEngine.AddProjectile()
                // AddSalt(sprites);
            }*/
            inputManager.detectKeysPressed();
            foreach (var overlay in this.overlaySprites)
            {
                overlay.Update(gameTime, overlaySprites);
            }

           
            if (inputManager.RightCtrlActive())
            {
                playerChar.MoveRight();
            }
            else if (inputManager.LeftCtrlActive())
            {
                playerChar.MoveLeft();
            }
            else if (inputManager.UpCtrlActive())
            {
                playerChar.MoveUp();
            }
            else if (inputManager.DownCtrlActive())
            {
                playerChar.MoveDown();
            }

            inputManager.DetectMouseClicks();
            if (inputManager.LeftMouseClick())
            {
                Vector2 projOrigin = this.characterSprites.ToArray()[0].Position;
                Vector2 projTarget = inputManager.GetMousePosition();
                int shooterID = 1;
                projectileEngine.AddProjectile(projOrigin, projTarget, projectileTexture, shooterID);
            }

            // Això hauria de moure els projectils, calcular les colisions i notificar als characters si hi ha hagut dany.
            projectileEngine.UpdateProjectiles(gameTime, characterSprites);

            foreach (var character in characterSprites.ToArray())
            {
                character.Update(gameTime, characterSprites);
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
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

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
