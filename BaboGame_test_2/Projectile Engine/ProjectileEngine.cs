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
    // la idea d'aquesta classe és abstreure tota la lògica per crear, moure i col·lisionar els projectils.
    // d'aquesta manera, podem implementar una classe similar al server i comparar el resultat de les colisions.
    class ProjectileEngine
    {
        private List<Sprite> projectileList;
        private Texture2D projectileTexture;
        private float masterProjVelocity;

        // constructor de la classe, li passem la llista buida per a que la vagi omplint de projectils
        // i la funcio draw de game1.cs els pugui renderitzar
        public ProjectileEngine(List<Sprite> projectileList)
        {
            this.projectileList = projectileList;
        }
        
        // definir la textura dels projectils. Separat del constructor per si ens interessa canviar-la al llarg de la partida
        public void SetProjectileTexture(Texture2D texture)
        {
            projectileTexture = texture;
        }

        // afegim un projectil a la llista, inicialitzant-lo amb posicio origen i final i una velocitat de moment estandard
        public void AddProjectile(Vector2 origin, Vector2 target, float velocity, Texture2D projectileTexture)
        {
            projectileList.Add(new Projectile(origin, target, velocity, projectileTexture));
        }
        
        // aquí estarà la gràcia. En comptes d'actualitzar-los un per un a cada objecte, agafarem
        // tota la llista i calcularem tots els moviments i colisions.
        // caldrà fer saber als characters que han colisionat que tenen dany + la direcció de l'impacte.
        public void UpdateProjectiles(GameTime gameTime, List<Sprite> characterList)
        {
            // moviment i colisions

            //Valorar el temps de vida de la sal a disparar i la eliminació d'aquest
            /* _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

             if (_timer > LifeSpan)
                 IsRemoved = true;

             //Definir la posició final de la sal per ser eliminada
             if (((target.X >= Position.X - 10) && (target.X <= Position.X + 10)) && ((target.Y >= Position.Y - 10) && (target.Y <= Position.Y + 10)))
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
             if ((Math.Abs(Position.X - origin.X) < Math.Abs(trajectory.X / 2)) && (Math.Abs(Position.Y - origin.Y) < Math.Abs(trajectory.Y / 2)))
                 Scale += 0.003f;
             else
                 Scale -= 0.003f;

         }*/
        }

    }


    public class Projectile : Sprite
    {
        // tots els atributs privats per matenir una interfícia pública neta i senzilla d'entendre
        private float timer;
        private Vector2 target;
        private Vector2 origin;
        private Vector2 trajectory;
        private float velocity;
        private Vector2 currentPosition;

        // constrctor per inicialitzar el projectil
        public Projectile(Vector2 origin, Vector2 target, float velocity, Texture2D texture)
            : base(texture)
        {
            this.origin = origin;
            this.target = target;
            this.velocity = velocity;
            this._texture = texture;
            this.trajectory = this.target - this.origin;
            this.currentPosition = this.origin;
            //IsSaltShoot = true;
        }

        public void UpdateCurrentPosition(Vector2 newPosition)
        {
            this.currentPosition = newPosition;
        }

        public void KillProjectile()
        {
            this.IsRemoved = true;
        }
    }
}

