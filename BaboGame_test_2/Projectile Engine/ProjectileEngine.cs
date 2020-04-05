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
        private List<Projectile> projectileList;

        // constants per defecte dels projectils
        private const float masterProjVelocity = 15;
        private const float masterProjScale = 0.1f;
        private const float masterProjDamage = 10;

        // constructor de la classe, li passem la llista buida per a que la vagi omplint de projectils
        // i la funcio draw de game1.cs els pugui renderitzar
        public ProjectileEngine(List<Projectile> projectileList)
        {
            this.projectileList = projectileList;
        }

        // afegim un projectil a la llista, inicialitzant-lo amb posicio origen i final i una velocitat de moment estàndard
        public void AddProjectile(Vector2 origin, Vector2 target, Texture2D projectileTexture, int shooterID)
        {
            projectileList.Add(new Projectile(origin, target, masterProjVelocity, shooterID, projectileTexture, masterProjScale, masterProjDamage));
        }

        // afegim un projectil amb velocitat configurable
        public void AddProjectile(Vector2 origin, Vector2 target, float velocity, Texture2D projectileTexture, int shooterID)
        {
            projectileList.Add(new Projectile(origin, target, velocity, shooterID, projectileTexture, masterProjScale, masterProjDamage));
        }

        // aquí estarà la gràcia. En comptes d'actualitzar-los un per un a cada objecte, agafarem
        // tota la llista i calcularem tots els moviments i colisions.
        // caldrà fer saber als characters que han colisionat que tenen dany + la direcció de l'impacte.
        public void UpdateProjectiles(GameTime gameTime, List<Character> characterList)
        {
            //Valorar el temps de vida de la sal a disparar i la eliminació d'aquest
            /* _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

             if (_timer > LifeSpan)
                 IsRemoved = true;
            */

            
            foreach (var projectile in this.projectileList)
            {
                //Definir la posició final de la sal per ser eliminada
                if (((projectile.target.X >= projectile.Position.X - 10) && (projectile.target.X <= projectile.Position.X + 10)) &&
                    ((projectile.target.Y >= projectile.Position.Y - 10) && (projectile.target.Y <= projectile.Position.Y + 10)))
                {
                    projectile.KillProjectile();
                }

                //Definir la colisió de la sal
                foreach (var character in characterList)
                {
                    if (projectile.DetectCollision(character))
                    {
                        if (projectile.shooterID != character.IDcharacter)
                        {
                            // notificar el dany al personatge!!!
                            character.NotifyHit(projectile.Direction, projectile.shooterID, projectile.damage,projectile.LinearVelocity);
                            projectile.KillProjectile();
                        }
                    }
                }

                if(!projectile.IsRemoved)
                {
                    projectile.Move();
                }
            }
        }
    }

    public class Projectile : Sprite
    {
        // ATRIBUTS
        private float timer;
        public Vector2 target { get; }
        private Vector2 origin;
        private Vector2 trajectory;
        public int shooterID { get; }
        public float damage { get; }
        
        
        
        // constrctor per inicialitzar el projectil
        public Projectile(Vector2 origin, Vector2 target, float velocity,int shooterID, Texture2D texture, float scale, float damage)
            : base(texture)
        {
            this.shooterID = shooterID;
            this.origin = origin;
            this.target = target;
            this.LinearVelocity = VectorOps.ModuloVector(new Vector2((origin.X - target.X),(origin.Y - target.Y)))/20;
            this._texture = texture;
            this.trajectory = this.target - this.origin;
            this.Position = this.origin;
            this.Direction = VectorOps.UnitVector(target - origin);
            this.Scale = scale;
            this.damage = damage;
            this.Layer = 0.01f;
            //IsSaltShoot = true;
        }

        // Movem el projectil de forma determinada per la direcció i velocitat linial
        public void Move()
        {
            this.Position += this.Direction * LinearVelocity;

            //Funcionament de canvi d'escala de la sal per donar la sensació d'un moviment parabòlic
            if ((Math.Abs(Position.X - origin.X) < Math.Abs(trajectory.X / 2)) && (Math.Abs(Position.Y - origin.Y) < Math.Abs(trajectory.Y / 2)))
                this.Scale += 0.003f;
            else
                this.Scale -= 0.003f;
        }

        // Marquem el projectil per la seva eliminació
        public void KillProjectile()
        {
            this.IsRemoved = true;
        }

        // Detecció de colisió genèrica
        public bool DetectCollision(Sprite sprite)
        {
            if(sprite != this)
            {
                return this.IsTouchingBottom(sprite) || this.IsTouchingLeft(sprite) || this.IsTouchingRight(sprite) || this.IsTouchingTop(sprite);
            }
            else
                return false;
        }
    }
}

