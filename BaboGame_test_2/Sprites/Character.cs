using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BaboGame_test_2
{

    /*
     * Defineix el sprite del personatge que el jugador controlarà
     */

    public class Character : Sprite
    {
        //Defineix el sprite de la salt que disparar el llimac i la direcció
        public SaltWeapon Salt;
        public float WeaponDirection = 0f;
        //Identificadors per la colisió
        public bool SaltPain = false;
        public Vector2 SaltHitDirection;
        float _PainTimer = 0f;

        public Character(Texture2D texture)
            : base(texture)
        {

        }

        public Character(Dictionary<string, Animation> animations)
           : base(animations)
        {

        }

        public void Update(GameTime gameTime, List<Character> sprites)
        {
            if (SaltPain)
                _PainTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_PainTimer > 0.3f)
            {
                _PainTimer = 0f;
                SaltPain = false;
            }

            //Move();

            //Condicions per fer parar el personatge en cas de col·lisió
            foreach (var sprite in sprites)
            {
                if ((sprite == this))
                    continue;

                //Detecta si ha sigut tocat per sal
                if (this.IsTouchingBottom(sprite) || this.IsTouchingLeft(sprite) || this.IsTouchingRight(sprite) || this.IsTouchingTop(sprite))
                {
                    if ((sprite.IDcharacter != IDcharacter) && (sprite.IDcharacter != 0) && (sprite.IsSaltShoot))
                    {
                        SaltPain = true;
                        _PainTimer = 0f;
                        SaltHitDirection = sprite.Direction;
                        continue;
                    }
                }

                if (!sprite.SolidObject)
                    continue;

                //Col·lisió física amb objectes
                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                        (this.Velocity.X < 0 && this.IsTouchingRight(sprite)))
                    this.Velocity.X = 0;

                if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                       (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite)))
                    this.Velocity.Y = 0;
            }

            Position += Velocity;
            Velocity = Vector2.Zero;

            //Defineix la direcció de dispar del personatge
            Direction = new Vector2((Mouse.GetState().Position.X - this.Position.X), (Mouse.GetState().Position.Y - this.Position.Y));
            Direction = VectorOps.UnitVector(Direction);

            //Reprodueix l'animació
            SetAnimations();
            this._animationManager.Update(gameTime);

            //Funció de disparar
            if ((currentMouseState.LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released))
            {
                //AddSalt(sprites);
            }

            //"Equació" per definir a quina capa es mostrarà el "sprite" perquè un personatge no li estigui trapitjant la cara al altre
            float LayerValue = this.Position.Y / 10000;
            if (LayerValue > 0.4)
                Layer = 0.4f;
            else
                Layer = LayerValue;
        }

        public void MoveLeft()
        {       
            this.Velocity.X -= this.LinearVelocity;
            //this.Position += Velocity;
            //Velocity = Vector2.Zero;
        }
        public void MoveRight()
        {
            this.Velocity.X += this.LinearVelocity;
            //this.Position += Velocity;
            //Velocity = Vector2.Zero;
        }
        public void MoveUp()
        {
            this.Velocity.Y -= this.LinearVelocity;
            //this.Position += Velocity;
            //Velocity = Vector2.Zero;
        }
        public void MoveDown()
        {
            this.Velocity.Y += this.LinearVelocity;
            //this.Position += Velocity;
            //Velocity = Vector2.Zero;
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
            /*if (currentKey.IsKeyDown(Input.Up))
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
            */
            if (SaltPain)
            {

                Velocity.X += LinearVelocity * SaltHitDirection.X;
                Velocity.Y += LinearVelocity * SaltHitDirection.Y;

            }
        }

        //Apartat de les animacions
        protected virtual void SetAnimations()
        {
            if (SaltPain)
            {
                float angle = VectorOps.Vector2ToDeg(SaltHitDirection);
                //Animació de ser colpejat per la salt
                if (angle < 305 && angle > 235)
                    _animationManager.Play(_animations["Babo up hit"]);
                else
                    _animationManager.Play(_animations["Babo down hit"]);
            }
            else
            {
                //Detecció del angle de dispar amb la corresponent animació (probablement s'haurà de fer de forma més eficient) -- Angle entre animacions: 18 graus || pi/10 radiants -- Desfasament: 9 graus || pi/20 radiant
                float angle = VectorOps.Vector2ToDeg(Direction);
                if ((angle <= 9 && angle >= 0) || (angle <= 360 && angle > 351))
                    _animationManager.Play(_animations["Babo right0"]);
                else if (angle <= 27 && angle > 9)
                    _animationManager.Play(_animations["Babo right-22_5"]);
                else if (angle <= 45 && angle > 27)
                    _animationManager.Play(_animations["Babo right-45"]);
                else if (angle <= 63 && angle > 45)
                    _animationManager.Play(_animations["Babo down45"]);
                else if (angle <= 81 && angle > 63)
                    _animationManager.Play(_animations["Babo down22_5"]);
                else if (angle <= 99 && angle > 81)
                    _animationManager.Play(_animations["Babo down0"]);
                else if (angle <= 117 && angle > 99)
                    _animationManager.Play(_animations["Babo down-22_5"]);
                else if (angle <= 135 && angle > 117)
                    _animationManager.Play(_animations["Babo down-45"]);
                else if (angle <= 153 && angle > 135)
                    _animationManager.Play(_animations["Babo left45"]);
                else if (angle <= 171 && angle > 153)
                    _animationManager.Play(_animations["Babo left22_5"]);
                else if (angle <= 189 && angle > 171)
                    _animationManager.Play(_animations["Babo left0"]);
                else if (angle <= 207 && angle > 189)
                    _animationManager.Play(_animations["Babo left-22_5"]);
                else if (angle <= 225 && angle > 207)
                    _animationManager.Play(_animations["Babo left-45"]);
                else if (angle <= 243 && angle > 225)
                    _animationManager.Play(_animations["Babo up45"]);
                else if (angle <= 261 && angle > 243)
                    _animationManager.Play(_animations["Babo up22_5"]);
                else if (angle <= 279 && angle > 261)
                    _animationManager.Play(_animations["Babo up0"]);
                else if (angle <= 297 && angle > 279)
                    _animationManager.Play(_animations["Babo up-22_5"]);
                else if (angle <= 315 && angle > 297)
                    _animationManager.Play(_animations["Babo up-45"]);
                else if (angle <= 333 && angle > 315)
                    _animationManager.Play(_animations["Babo right45"]);
                else if (angle <= 351 && angle > 333)
                    _animationManager.Play(_animations["Babo right22_5"]);
                else
                    _animationManager.Play(_animations["Babo down0"]);
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
            salt.Destination = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            salt.Trajectory = new Vector2((Mouse.GetState().Position.X - this.Position.X), (Mouse.GetState().Position.Y - this.Position.Y));
            salt.SolidObject = false;
            salt.Layer = 0.41f;
            salt.IDcharacter = this.IDcharacter;

            //Afegeix la sal a disparar
            sprites.Add(salt);
        }
    }
}