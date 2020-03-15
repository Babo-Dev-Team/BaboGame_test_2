using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


    public class SaltWeapon : Sprite
    {
        private float _timer;
        public Vector2 MousePosition;
        public Vector2 Source;
        public Vector2 Destination;

        public SaltWeapon(Texture2D texture)
            : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > LifeSpan)
                IsRemoved = true;
            if (((MousePosition.X >= Position.X - 5) && (MousePosition.X <= Position.X + 5)) && ((MousePosition.Y >= Position.Y - 5) && (MousePosition.Y <= Position.Y + 5)))
                IsRemoved = true;

            Position += Direction * LinearVelocity;
            
            if ((Math.Abs(Position.X - Source.X) < Math.Abs(Destination.X / 2)) && (Math.Abs(Position.Y - Source.Y) < Math.Abs(Destination.Y / 2)))
                Scale += 0.003f;
            else
                Scale -= 0.003f;
        }
    }

