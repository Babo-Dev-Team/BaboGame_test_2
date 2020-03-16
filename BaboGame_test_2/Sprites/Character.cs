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
    public float WeaponDirection = 0f;

        public Character(Texture2D texture)
            : base(texture)
        {

        }

        public Character(Dictionary<string, Animation> animations)
           : base(animations)
        {

        }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            //Condicions per fer parar el personatge en cas de col·lisió
            foreach (var sprite in sprites)
            {
                if ((sprite == this) || (!sprite.SolidObject))
                    continue;

                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite))||
                    (this.Velocity.X < 0 && this.IsTouchingRight(sprite)))
                    this.Velocity.X = 0;

                if((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                    (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite)))
                    this.Velocity.Y = 0;
            }

            Position += Velocity;
            Velocity = Vector2.Zero;

            //Defineix la direcció de dispar del personatge
            Direction = new Vector2((Mouse.GetState().Position.X - this.Position.X) / (float)Math.Sqrt(Math.Pow(Mouse.GetState().Position.X - this.Position.X, 2) + Math.Pow(Mouse.GetState().Position.Y - this.Position.Y, 2)), (Mouse.GetState().Position.Y - this.Position.Y) / (float)Math.Sqrt(Math.Pow(Mouse.GetState().Position.X - this.Position.X, 2) + Math.Pow(Mouse.GetState().Position.Y - this.Position.Y, 2)));

            //Reprodueix l'animació
            SetAnimations();
            _animationManager.Update(gameTime);

            //Funció de disparar
            if ((currentMouseState.LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released))
            {
                AddSalt(sprites);
            }

            //"Equació" per definir a quina capa es mostrarà el "sprite" perquè un personatge no li estigui trapitjant la cara al altre
            float LayerValue = this.Position.Y / 10000;
            if (LayerValue > 0.4)
                Layer = 0.4f;
            else
                Layer = LayerValue;
        }

        //Funció per moure le personatge
        private void Move()
        {
            //Defineix els estats del teclat i el ratolí
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            //Controls del moviment del personatge
            if (currentKey.IsKeyDown(Input.Up))
            {
                Velocity.Y -= LinearVelocity;
            }

            if (currentKey.IsKeyDown(Input.Down))
            {
                Velocity.Y += LinearVelocity;
            }

            if (currentKey.IsKeyDown(Input.Left))
            {
                Velocity.X -= LinearVelocity;
            }

            if (currentKey.IsKeyDown(Input.Right))
            {
                Velocity.X += LinearVelocity;
            }
        }

        //Apartat de les animacions
        protected virtual void SetAnimations()
        {

            //Detecció del angle de dispar amb la corresponent animació (probablement s'haurà de fer de forma més eficient) -- Angle entre animacions: 18 graus || pi/10 radiants -- Desfasament: 9 graus || pi/20 radiant
            if ((Direction.X > Math.Cos(9)) && (Direction.Y > Math.Sin(-Math.PI/20)) && (Direction.Y < Math.Sin(Math.PI/20)))
                _animationManager.Play(_animations["Babo right0"]);
            else if ((Direction.X < Math.Cos(Math.PI/20)) && (Direction.X > Math.Cos(3*Math.PI/20)) && (Direction.Y > Math.Sin(Math.PI/20)) && (Direction.Y < Math.Sin(3*Math.PI/20)))
                _animationManager.Play(_animations["Babo right-22_5"]);
            else if ((Direction.X < Math.Cos(3*Math.PI / 20)) && (Direction.X > Math.Cos(Math.PI / 4)) && (Direction.Y > Math.Sin(3 * Math.PI / 20)) && (Direction.Y < Math.Sin(Math.PI / 4)))
                _animationManager.Play(_animations["Babo right-45"]);
            else if ((Direction.X < Math.Cos(Math.PI / 4)) && (Direction.X > Math.Cos(7 * Math.PI / 20)) && (Direction.Y > Math.Sin(Math.PI / 4)) && (Direction.Y < Math.Sin(7 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo down45"]);
            else if ((Direction.X < Math.Cos(7 * Math.PI / 20)) && (Direction.X > Math.Cos(9 * Math.PI / 20)) && (Direction.Y > Math.Sin(7 * Math.PI / 20)) && (Direction.Y < Math.Sin(9 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo down22_5"]);
            else if ((Direction.X < Math.Cos(9 * Math.PI / 20)) && (Direction.X > Math.Cos(11 * Math.PI / 20)) && (Direction.Y > Math.Sin(9 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo down0"]);
            else if ((Direction.X < Math.Cos(11 * Math.PI / 20)) && (Direction.X > Math.Cos(13 * Math.PI / 20)) && (Direction.Y > Math.Sin(13 * Math.PI / 20)) && (Direction.Y < Math.Sin(11 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo down-22_5"]);
            else if ((Direction.X < Math.Cos(13 * Math.PI / 20)) && (Direction.X > Math.Cos(3 * Math.PI / 4)) && (Direction.Y > Math.Sin(3 * Math.PI / 4)) && (Direction.Y < Math.Sin(13 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo down-45"]);
            else if ((Direction.X < Math.Cos(3 * Math.PI / 4)) && (Direction.X > Math.Cos(17 * Math.PI / 20)) && (Direction.Y > Math.Sin(17 * Math.PI / 20)) && (Direction.Y < Math.Sin(3 * Math.PI / 4)))
                _animationManager.Play(_animations["Babo left45"]);
            else if ((Direction.X < Math.Cos(17 * Math.PI / 20)) && (Direction.X > Math.Cos(19 * Math.PI / 20)) && (Direction.Y > Math.Sin(19 * Math.PI / 20)) && (Direction.Y < Math.Sin(17 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo left22_5"]);
            else if ((Direction.X < Math.Cos(19 * Math.PI / 20)) && (Direction.Y > Math.Sin(21 * Math.PI / 20)) && (Direction.Y < Math.Sin(19 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo left0"]);
            else if ((Direction.X < Math.Cos(23 * Math.PI / 20)) && (Direction.X > Math.Cos(21 * Math.PI / 20)) && (Direction.Y > Math.Sin(23 * Math.PI / 20)) && (Direction.Y < Math.Sin(21 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo left-22_5"]);
            else if ((Direction.X < Math.Cos(5 * Math.PI / 4)) && (Direction.X > Math.Cos(23 * Math.PI / 20)) && (Direction.Y > Math.Sin(5 * Math.PI / 4)) && (Direction.Y < Math.Sin(23 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo left-45"]);
            else if ((Direction.X < Math.Cos(27 * Math.PI / 20)) && (Direction.X > Math.Cos(5 * Math.PI / 4)) && (Direction.Y > Math.Sin(27 * Math.PI / 20)) && (Direction.Y < Math.Sin(5 * Math.PI / 4)))
                _animationManager.Play(_animations["Babo up45"]);
            else if ((Direction.X < Math.Cos(29 * Math.PI / 20)) && (Direction.X > Math.Cos(27 * Math.PI / 20)) && (Direction.Y > Math.Sin(29 * Math.PI / 20)) && (Direction.Y < Math.Sin(27 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo up22_5"]);
            else if ((Direction.X < Math.Cos(31 * Math.PI / 20)) && (Direction.X > Math.Cos(29 * Math.PI / 20)) && (Direction.Y < Math.Sin(29 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo up0"]);
            else if ((Direction.X < Math.Cos(33 * Math.PI / 20)) && (Direction.X > Math.Cos(31 * Math.PI / 20)) && (Direction.Y > Math.Sin(31 * Math.PI / 20)) && (Direction.Y < Math.Sin(33 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo up-22_5"]);
            else if ((Direction.X < Math.Cos(35 * Math.PI / 20)) && (Direction.X > Math.Cos(33 * Math.PI / 20)) && (Direction.Y > Math.Sin(33 * Math.PI / 20)) && (Direction.Y < Math.Sin(35 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo up-45"]);
            else if ((Direction.X < Math.Cos(37 * Math.PI / 20)) && (Direction.X > Math.Cos(35 * Math.PI / 20)) && (Direction.Y > Math.Sin(35 * Math.PI / 20)) && (Direction.Y < Math.Sin(37 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo right45"]);
            else if ((Direction.X < Math.Cos(39 * Math.PI / 20)) && (Direction.X > Math.Cos(37 * Math.PI / 20)) && (Direction.Y > Math.Sin(37 * Math.PI / 20)) && (Direction.Y < Math.Sin(39 * Math.PI / 20)))
                _animationManager.Play(_animations["Babo right22_5"]);
            else
                _animationManager.Play(_animations["Babo down0"]);

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
            salt.SolidObject = false;
            salt.Layer = 0.41f;
            
            //Afegeix la sal a disparar
            sprites.Add(salt);
        }


    }

