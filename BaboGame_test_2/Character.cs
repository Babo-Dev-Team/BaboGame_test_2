using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

/*
 * Defineix el sprite del personatge que el jugador controlarà
 */

    public class Character : Sprite
    {
        //Defineix el sprite de la salt que disparar el llimac
        public SaltWeapon Salt;

        public Character(Texture2D texture)
            : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Defineix els estats del teclat i el ratolí
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            
            //Controls del moviment del personatge
            if (currentKey.IsKeyDown(Keys.W))
            {
                Position.Y -= LinearVelocity;
            }

            if (currentKey.IsKeyDown(Keys.S))
            {
                Position.Y += LinearVelocity;
            }

            if (currentKey.IsKeyDown(Keys.A))
            {
                Position.X -= LinearVelocity;
            }

            if (currentKey.IsKeyDown(Keys.D))
            {
                Position.X += LinearVelocity;
            }

            //Defineix la direcció de dispar del personatge
            Direction = new Vector2((Mouse.GetState().Position.X - this.Position.X) / (float)Math.Sqrt(Math.Pow(Mouse.GetState().Position.X - this.Position.X, 2) + Math.Pow(Mouse.GetState().Position.Y - this.Position.Y, 2)), (Mouse.GetState().Position.Y - this.Position.Y) / (float)Math.Sqrt(Math.Pow(Mouse.GetState().Position.X - this.Position.X, 2) + Math.Pow(Mouse.GetState().Position.Y - this.Position.Y, 2)));

            //Funció de disparar
            if ((currentMouseState.LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released))
            {
                AddSalt(sprites);
            }
        }
        
        //Funció per crear la sal que ha de disparar el personatge
        private void AddSalt(List<Sprite> sprites)
        {
            //Clonació del sprite de la sal
            var salt = Salt.Clone() as SaltWeapon;

            //Definir les característiques de la sal a disparar
            salt.Direction = this.Direction;
            salt.Position = this.Position;
            salt.Source = this.Position;
            salt.LinearVelocity = this.LinearVelocity * 2;
            salt.LifeSpan = 2f;
            salt.Parent = this;
            salt.Scale = 0.15f;
            salt.MousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            salt.Destination = new Vector2((Mouse.GetState().Position.X - this.Position.X) , (Mouse.GetState().Position.Y - this.Position.Y));
            
            //Afegeix la sal a disparar
            sprites.Add(salt);
        }


    }

