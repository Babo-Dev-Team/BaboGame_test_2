using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

    /*
     * Funció per definir el funcionament de la sal a disparar
     */
namespace BaboGame_test_2
{
    public class SaltWeapon : Sprite
    {
        //Variables per saber el camí de la sal
        private float _timer;
        public Vector2 Destination;
        public Vector2 Source;
        public Vector2 Trajectory;

        public SaltWeapon(Texture2D texture)
            : base(texture)
        {
            IsSaltShoot = true;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Valorar el temps de vida de la sal a disparar i la eliminació d'aquest

            // OJO!! AQUEST TIMER ESTÂ INICIALITZAT????
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > LifeSpan)
                IsRemoved = true;

            //Definir la posició final de la sal per ser eliminada
            if (((Destination.X >= Position.X - 10) && (Destination.X <= Position.X + 10)) && ((Destination.Y >= Position.Y - 10) && (Destination.Y <= Position.Y + 10)))
                IsRemoved = true;

            //Definir la colisió de la sal
            foreach (var sprite in sprites)
            {
                if (this.IsTouchingBottom(sprite) || this.IsTouchingLeft(sprite) || this.IsTouchingRight(sprite) || this.IsTouchingTop(sprite))
                {
                    if ((sprite.SolidObject || sprite.IsSaltShoot) && (this.IDcharacter != sprite.IDcharacter))
                        this.IsRemoved = true;
                }
            }

            //Reprodueix el moviment de la sal
            Position += Direction * LinearVelocity;

            //Funcionament de canvi d'escala de la sal per donar la sensació d'un moviment parabòlic
            if ((Math.Abs(Position.X - Source.X) < Math.Abs(Trajectory.X / 2)) && (Math.Abs(Position.Y - Source.Y) < Math.Abs(Trajectory.Y / 2)))
                Scale += 0.003f;
            else
                Scale -= 0.003f;
        }
    }

}

