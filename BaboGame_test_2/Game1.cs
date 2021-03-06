﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;
using System.Timers;

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

        Texture2D backgroundImage;


        private List<Character> characterSprites;           // Personatges (inclòs el jugador)
        private List<Projectile> projectileSprites;         // Projectils, creats per projectileEngine
        private List<Sprite> overlaySprites;                // Sprites de la UI, de moment només la mira
        private List<Slime> slimeSprites;                   // Babes, creats per SlimeEngine
        private List<ScenarioObjects> scenarioSprites;      // Sprites per objectes sòlids que estiguin per pantalla

        ProjectileEngine projectileEngine;
        ProjectileManager projectileManager;
        Dictionary<string, Texture2D> projectileTexture;
        SlimeEngine slimeEngine;
        HeartManager heartManager;                          // Mecanismes de la vida
        InputManager inputManager = new InputManager(Keys.W, Keys.S, Keys.A, Keys.D); // El passem ja inicialitzat als objectes
        KeyboardState _previousState;

        CharacterEngine characterEngine;
        Character playerChar;                               // Punter cap al character controlat pel jugador
        Character playerChar2;                               // Punter cap al character de provas-------------------------Babo prova
        Character playerChar3;                               // Punter cap al character de provas-------------------------Limax prova
        Texture2D slimeTexture;                             // Textura per instanciar les babes
        //Temporització de les babes
        private static Timer timer;
        int SlimeTime = 0;
        Random EnemyShoot = new Random(); //-------------------------------------Babo prova

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
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
            slimeSprites = new List<Slime>();
            slimeEngine = new SlimeEngine(slimeSprites);
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
     
            Dictionary<string, Animation> slugHealth = new Dictionary<string, Animation>()
            {
                {"3/4 heart", new Animation(Content.Load<Texture2D>("Slug_status/heart-3_4"), 1) },
                {"2/4 heart", new Animation(Content.Load<Texture2D>("Slug_status/heart-2_4"), 1) },
                {"1/4 heart", new Animation(Content.Load<Texture2D>("Slug_status/heart-1_4"), 1) },
                {"Empty heart", new Animation(Content.Load<Texture2D>("Slug_status/heart-empty"), 1) },
                {"Babo down hit", new Animation(Content.Load<Texture2D>("Babo/Babo down hit"), 1) },
                {"Heart", new Animation(Content.Load<Texture2D>("Slug_status/Heart"), 1) },
            };



            Dictionary<string, Animation> sightAnimation = new Dictionary<string, Animation>()
            {
                {"ON", new Animation(Content.Load<Texture2D>("Sight/Sight_on"), 1) },
                {"OFF", new Animation(Content.Load<Texture2D>("Sight/Sight_off"), 1) },
            };

            var slugTexture = Content.Load<Texture2D>("Babo/Babo down0 s0");
            var sightTexture = Content.Load<Texture2D>("Sight/Sight_off");
            var scenarioTexture = Content.Load<Texture2D>("Scenario/Block");
            //scenarioTexture = Content.Load<Texture2D>("Scenario/Beer");

            var projectileMenuTexture = Content.Load<Texture2D>("Slug_status/SaltMenu");
            projectileTexture = new Dictionary<string, Texture2D>()
            {
                {"Normal", Content.Load<Texture2D>("Projectile/Salt")},
                {"Direct", Content.Load<Texture2D>("Projectile/DirectSalt")},
                {"Slimed", Content.Load<Texture2D>("Projectile/NoNewtonianSlimedSalt")},
            };
            
            slimeTexture = Content.Load<Texture2D>("Projectile/slime2");

            characterSprites = new List<Character>();

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

            scenarioSprites = new List<ScenarioObjects>()
            {
                new ScenarioObjects(scenarioTexture)
                {
                    Position = new Vector2(400,100),
                    Scale = 0.2f,
                    SolidObject = true,
                    HitBoxScale = 1f,
                    HasConducitivity = true,
                    Charge = 'P',
                },

                new ScenarioObjects(scenarioTexture)
                {
                    Position = new Vector2(400,500),
                    Scale = 0.2f,
                    SolidObject = true,
                    HitBoxScale = 1f,
                    HasConducitivity = true,
                    Charge = 'N',
                },
            };

            characterEngine = new CharacterEngine(characterSprites,Content);
            characterEngine.AddKnownCharacter("Kaler",new Vector2(100,100), 0.20f, 20, 1,Color.White);
            characterEngine.AddKnownCharacter("Babo",new Vector2(300,300), 0.20f, 40, 2,Color.White);
            characterEngine.AddKnownCharacter("Limax", new Vector2(500, 500), 0.20f, 20, 3, Color.White);


            heartManager = new HeartManager(overlaySprites);
            heartManager.CreateHeart(1, 5, 20, slugHealth,new Vector2(100,300));
            heartManager.CreateHeart(2, 10, 39, slugHealth,new Vector2(100,400)); //--------------------- Babo prova
            heartManager.CreateHeart(3, 5, 20, slugHealth, new Vector2(500, 100)); //--------------------- Babo prova

            projectileSprites = new List<Projectile>();
            projectileEngine = new ProjectileEngine(projectileSprites);
            projectileManager = new ProjectileManager(projectileTexture, projectileEngine);
            projectileManager.CreateSaltMenu(projectileMenuTexture, overlaySprites, 1, 0.1f);

            // punter que apunta al personatge controlat pel jugador
            playerChar = characterSprites.ToArray()[0];
            playerChar2 = characterSprites.ToArray()[1]; //------------------------- Babo prova
            playerChar3 = characterSprites.ToArray()[2]; //------------------------- Babo prova
            _font = Content.Load<SpriteFont>("Font");

            //timer
            timer = new Timer(60);
            timer.AutoReset = true;
            timer.Enabled = true;
            debugger = new Debugger(characterSprites,projectileSprites,overlaySprites,slimeSprites, timer.Interval,graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight,_font);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        bool Slug2Direction = false; //--------------------------------------- Babo prova
        bool Slug2Direction2 = false;

        bool Slug3Direction = false; //--------------------------------------- Limax prova
        bool Slug3Direction2 = false;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
               Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.F11) && (_previousState.IsKeyUp(Keys.F11)))
                graphics.ToggleFullScreen();

            

            // Detectem inputs al teclat
            inputManager.detectKeysPressed();
            _previousState = Keyboard.GetState();

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
            
            
            //Actualitzem moviment del llimac de prova ---------------------Babo prova
            playerChar2.Direction = VectorOps.UnitVector(playerChar.Position - playerChar2.Position);

            if (!Slug2Direction)
                playerChar2.MoveRight();
            else
                playerChar2.MoveLeft();
            if (Slug2Direction2)
                playerChar2.MoveUp();
            else
                playerChar2.MoveDown();

            if ((playerChar2.Position.X > graphics.PreferredBackBufferWidth))
                Slug2Direction = true;
            else if (playerChar2.Position.X < 0)
                Slug2Direction = false;
               
            if (playerChar2.Position.Y > graphics.PreferredBackBufferHeight)
                Slug2Direction2 = true;
            else if (playerChar2.Position.Y < 0)
                Slug2Direction2 = false;

            //Actualitzem moviment del llimac de prova ---------------------Limax prova
            playerChar3.Direction = VectorOps.UnitVector(playerChar.Position - playerChar3.Position);

            if (!Slug3Direction)
                playerChar3.MoveRight();
            else
                playerChar3.MoveLeft();
            if (Slug3Direction2)
                playerChar3.MoveUp();
            else
                playerChar3.MoveDown();

            if ((playerChar3.Position.X > graphics.PreferredBackBufferWidth))
                Slug3Direction = true;
            else if (playerChar3.Position.X < 0)
                Slug3Direction = false;

            if (playerChar3.Position.Y > graphics.PreferredBackBufferHeight)
                Slug3Direction2 = true;
            else if (playerChar3.Position.Y < 0)
                Slug3Direction2 = false;

            // llançem projectils segons els inputs del jugador
            inputManager.DetectMouseClicks();
            projectileManager.Update(gameTime, inputManager.GetMouseWheelValue(), overlaySprites,characterSprites);
            if (inputManager.LeftMouseClick())
            {
                Vector2 projOrigin = playerChar.Position;
                Vector2 projTarget = inputManager.GetMousePosition();
                int shooterID = 1; // caldrà gestionar els ID's des del server
                projectileManager.AddProjectile(projOrigin, projTarget, shooterID);
            }

            //if (EnemyShoot.Next(0,32) == 0) //--------------------------- Babo prova
             //projectileEngine.AddProjectile(playerChar2.Position, playerChar.Position, projectileTexture["Slimed"], 2,'S');

            //Això actualitzaria els objectes del escenari
            foreach (var ScenarioObj in scenarioSprites)
            {
                ScenarioObj.Update(gameTime);
            }

            characterEngine.Update(gameTime,slimeSprites,scenarioSprites);
            // Això hauria de moure els projectils, calcular les colisions i notificar als characters si hi ha hagut dany.
            projectileEngine.UpdateProjectiles(gameTime, characterSprites, scenarioSprites);

            // Generem les babes amb una certa espera per no sobrecarregar i les instanciem al update del personatge
            timer.Elapsed += OnTimedEvent;

            foreach (var character in characterSprites.ToArray())
            {
                character.Update(gameTime, characterSprites);
                heartManager.UpdateHealth(character.IDcharacter, character.Health);
                if ((SlimeTime > 80) && (slimeSprites.Count < 400))
                {
                    slimeSprites.Add(
                       new Slime(new Vector2(character.Position.X, character.Position.Y + 20), character.IDcharacter, slimeTexture, 0.15f)
                       {
                           timer = 0,
                       }
                       );
                    character.isSlip = false;
                }
            }

            if ((SlimeTime > 80))
            {              
                foreach (var slime in slimeSprites)
                {
                    slime.timer++;
                }
                SlimeTime = 0;
            }

            //Això hauria de fer reaccionar les babes a projectils, characters i objectes de l'escenari
            slimeEngine.UpdateSlime(gameTime, characterSprites, projectileSprites, scenarioSprites);

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

            for (int i = 0; i < slimeSprites.Count; i++)
            {
                if (slimeSprites[i].IsRemoved)
                {
                    slimeSprites.RemoveAt(i);
                    i--;
                }
            }
        }
        
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            SlimeTime++;
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

            //Fons
            backgroundImage = Content.Load<Texture2D>("Scenario/Scenario1");
            float backGroundScale = Math.Max(((float)graphics.PreferredBackBufferWidth/(float)backgroundImage.Width),((float)graphics.PreferredBackBufferHeight/(float)backgroundImage.Height));
            spriteBatch.Draw(backgroundImage, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0,0), backGroundScale, SpriteEffects.None, 0f);

            debugger.DrawText(spriteBatch);

            foreach (var sprite in scenarioSprites)
            {
                sprite.Draw(spriteBatch);
            }
            foreach (var sprite in slimeSprites)
            {
                sprite.Draw(spriteBatch);
            }
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
                if(overlay.Visible)
                    overlay.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
