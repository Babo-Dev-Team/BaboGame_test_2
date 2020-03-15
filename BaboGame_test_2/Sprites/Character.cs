using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


    public class Character : Sprite
    {

        public SaltWeapon Salt;

        public Character(Texture2D texture)
            : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (currentKey.IsKeyDown(Keys.W)) //Albert8
            {
                Position.Y -= LinearVelocity;
            }

            if (currentKey.IsKeyDown(Keys.S)) //Albert9
            {
                Position.Y += LinearVelocity;
            }

            if (currentKey.IsKeyDown(Keys.A)) //Albert10
            {
                Position.X -= LinearVelocity;
            }

            if (currentKey.IsKeyDown(Keys.D)) //Albert11
            {
                Position.X += LinearVelocity;
            }

            Direction = new Vector2((Mouse.GetState().Position.X - this.Position.X) / (float)Math.Sqrt(Math.Pow(Mouse.GetState().Position.X - this.Position.X, 2) + Math.Pow(Mouse.GetState().Position.Y - this.Position.Y, 2)), (Mouse.GetState().Position.Y - this.Position.Y) / (float)Math.Sqrt(Math.Pow(Mouse.GetState().Position.X - this.Position.X, 2) + Math.Pow(Mouse.GetState().Position.Y - this.Position.Y, 2)));

            //Disparar
            if ((currentMouseState.LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released))
            {
                AddSalt(sprites);
            }
        }

        private void AddSalt(List<Sprite> sprites)
        {
            var salt = Salt.Clone() as SaltWeapon;
            salt.Direction = this.Direction;
            salt.Position = this.Position;
            salt.Source = this.Position;
            salt.LinearVelocity = this.LinearVelocity * 2;
            salt.LifeSpan = 2f;
            salt.Parent = this;
            salt.Scale = 0.15f;
            salt.MousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            salt.Destination = new Vector2((Mouse.GetState().Position.X - this.Position.X) , (Mouse.GetState().Position.Y - this.Position.Y));
            sprites.Add(salt);
        }


    }

