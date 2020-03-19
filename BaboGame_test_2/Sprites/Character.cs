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
        // Constructors
        public Character(Texture2D texture)
            : base(texture)
        {
            isHit = false;
        }

        public Character(Dictionary<string, Animation> animations)
           : base(animations)
        {
            isHit = false;
        }

        // interfície pública per moure el character
        public void MoveLeft()
        {
            if (!isHit)
            {
                this.Velocity.X -= this.LinearVelocity;
            }
        }
        public void MoveRight()
        {
            if (!isHit)
            {
                this.Velocity.X += this.LinearVelocity;
            }
        }
        public void MoveUp()
        {
            if (!isHit)
            {
                this.Velocity.Y -= this.LinearVelocity;
            }
        }
        public void MoveDown()
        {
            if (!isHit)
            {
                this.Velocity.Y += this.LinearVelocity;
            }
        }

        // Identificadors per la colisió amb projectils
        // mètode públic per restringir l'accés al flag isHit (mantenir-lo privat)
        private bool isHit;
        public bool IsHit()
        {
            return this.isHit;
        }
        private Vector2 hitDirection;
        float _PainTimer = 0f;

        // Detectem colisions i actualitzem posicions, timers i flags del character
        public void Update(GameTime gameTime, List<Character> characterSprites)
        {
            if (isHit)
                _PainTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_PainTimer > 0.3f)
            {
                _PainTimer = 0f;
                isHit = false;
            }

            // si tenim un impacte, desplaçament per impacte
            if (isHit)
            {
                Velocity.X += LinearVelocity * hitDirection.X;
                Velocity.Y += LinearVelocity * hitDirection.Y;
            }
            
            // si detectem colisions amb altres jugadors, no incrementem posició
            if(!this.DetectCharCollisions(characterSprites))
            {
                Position += Velocity;
            }
            Velocity = Vector2.Zero;

            //Reprodueix l'animació
            SetAnimations();
            this._animationManager.Update(gameTime);

            //"Equació" per definir a quina capa es mostrarà el "sprite" perquè un personatge no li estigui trapitjant la cara al altre
            float LayerValue = this.Position.Y / 10000;
            if (LayerValue > 0.4)
                Layer = 0.4f;
            else
                Layer = LayerValue;
        }

        // detectem colisions amb altres jugadors
        public bool DetectCharCollisions(List<Character> charSprites)
        {
            bool collisionDetected = false;
            foreach (var character in charSprites)
            if (character != this)
            {
                collisionDetected = this.IsTouchingBottom(character) || this.IsTouchingLeft(character) 
                                  || this.IsTouchingRight(character) || this.IsTouchingTop(character);
            }
            return collisionDetected;
        }

        //Apartat de les animacions
        protected virtual void SetAnimations()
        {
            if (isHit)
            {
                float angle = VectorOps.Vector2ToDeg(hitDirection);
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

        public void NotifyHit(Vector2 hitDirection, int shooterID, float damage)
        {
            this.isHit = true;
            this.hitDirection = hitDirection;
        }
    }
}