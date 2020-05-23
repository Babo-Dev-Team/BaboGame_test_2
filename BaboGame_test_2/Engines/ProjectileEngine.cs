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
        public void AddProjectile(Vector2 origin, Vector2 target, Texture2D projectileTexture, int shooterID, char projectileType)
        {
            projectileList.Add(new Projectile(origin, target, masterProjVelocity, shooterID, projectileTexture, masterProjScale, masterProjDamage, projectileType));
        }

        // afegim un projectil amb velocitat configurable
        public void AddProjectile(Vector2 origin, Vector2 target, float velocity, Texture2D projectileTexture, int shooterID, char projectileType)
        {
            projectileList.Add(new Projectile(origin, target, velocity, shooterID, projectileTexture, masterProjScale, masterProjDamage, projectileType));
        }

        // aquí estarà la gràcia. En comptes d'actualitzar-los un per un a cada objecte, agafarem
        // tota la llista i calcularem tots els moviments i colisions.
        // caldrà fer saber als characters que han colisionat que tenen dany + la direcció de l'impacte.
        public void UpdateProjectiles(GameTime gameTime, List<Character> characterList, List<ScenarioObjects> objectsList)
        {
            //Valorar el temps de vida de la sal a disparar i la eliminació d'aquest
            /* _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

             if (_timer > LifeSpan)
                 IsRemoved = true;
            */

            
            foreach (var projectile in this.projectileList)
            {
                if (projectile.ProjectileType == 'D') //Sal directe
                    DirectSaltUpdate(characterList, objectsList, projectile);
                else if (projectile.ProjectileType == 'S') //Slimed Salt
                    SlimedSaltUpdate(characterList,objectsList, projectile);
                else
                    NormalSaltUpdate(characterList, objectsList, projectile);

                if(!projectile.IsRemoved)
                {
                    projectile.Move();
                }
            }
        }

        // Mecànica de la sal normal
        private void NormalSaltUpdate(List<Character> characterList, List<ScenarioObjects> objectsList, Projectile projectile)
        {
            //Definir la posició final de la sal per ser eliminada
            if (((projectile.Target.X >= projectile.Position.X - 15) && (projectile.Target.X <= projectile.Position.X + 15)) &&
                ((projectile.Target.Y >= projectile.Position.Y - 15) && (projectile.Target.Y <= projectile.Position.Y + 15)))
            {
                projectile.KillProjectile();
            }

            //Definim la colisió entre la sal
            foreach (var projectileItem in projectileList)
            {
                if((projectile.DetectCollision(projectileItem))&&(projectileItem != projectile))
                {
                    projectile.KillProjectile();
                }
            }

            //Definir la colisió de la sal
            foreach (var character in characterList)
            {
                if (projectile.DetectCollision(character))
                {
                    if (projectile.ShooterID != character.IDcharacter)
                    {
                        // notificar el dany al personatge!!!
                        character.NotifyHit(projectile.Direction, projectile.ShooterID, projectile.Damage, projectile.LinearVelocity);
                        projectile.KillProjectile();
                    }
                }
            }

            foreach (var Object in objectsList)
            {
                if (projectile.DetectCollision(Object))
                {
                    projectile.KillProjectile();
                }
            }
        }

        // Mecànica de la sal directe
        private void DirectSaltUpdate(List<Character> characterList, List<ScenarioObjects> objectsList, Projectile projectile)
        {
            //Elimina la sal en un límit de distancia
            if (VectorOps.ModuloVector(projectile.Origin - projectile.Position) > 2000)
                projectile.KillProjectile();

            //Definim la colisió entre la sal
            foreach (var projectileItem in projectileList)
            {
                if((projectile.DetectCollision(projectileItem))&&(projectileItem != projectile))
                {
                    projectile.KillProjectile();
                }
            }

            //Definir la colisió de la sal
            foreach (var character in characterList)
            {
                if (projectile.DetectCollision(character))
                {
                    if (projectile.ShooterID != character.IDcharacter)
                    {
                        // notificar el dany al personatge!!!
                        character.NotifyHit(projectile.Direction, projectile.ShooterID, projectile.Damage, projectile.LinearVelocity);
                        projectile.KillProjectile();
                    }
                }
            }

            foreach (var Object in objectsList)
            {
                if (projectile.DetectCollision(Object))
                {
                    projectile.KillProjectile();
                }
            }
        }

        // Mecànica de la sal no neutoniana
        private void SlimedSaltUpdate(List<Character> characterList, List<ScenarioObjects> objectsList, Projectile projectile)
        {
            //Elimina la sal en un límit de distancia
            if (VectorOps.ModuloVector(projectile.Origin - projectile.Position) > 2000)
                projectile.KillProjectile();

            //Definim la colisió entre la sal
            foreach (var projectileItem in projectileList)
            {
                if((projectile.DetectCollision(projectileItem))&&(projectileItem != projectile))
                {
                    if(projectileItem.ProjectileType == 'S')
                    {
                        if (projectile.DetectBottomCollision(projectileItem))
                        {
                            projectile.Direction.Y = Math.Abs(projectile.Direction.Y);
                            projectile.HitCount++;
                        }

                        if (projectile.DetectTopCollision(projectileItem))
                        {
                            projectile.Direction.Y = -Math.Abs(projectile.Direction.Y);
                            projectile.HitCount++;
                        }

                        if (projectile.DetectRightCollision(projectileItem))
                        {
                            projectile.Direction.X = Math.Abs(projectile.Direction.X);
                            projectile.HitCount++;
                        }
                        if (projectile.DetectLeftCollision(projectileItem))
                        {
                            projectile.Direction.X = -Math.Abs(projectile.Direction.X);
                            projectile.HitCount++;
                        }
                    }
                    else
                        projectile.KillProjectile();

                }

            }

            //Definir la colisió de la sal
            foreach (var character in characterList)
            {
                if (projectile.DetectCollision(character))
                {
                    if ((projectile.ShooterID != character.IDcharacter)||(projectile.HitCount != 0))
                    {
                        // notificar el dany al personatge!!!
                        character.NotifyHit(projectile.Direction, projectile.ShooterID, projectile.Damage, projectile.LinearVelocity);
                        projectile.KillProjectile();
                    }
                }
            }

            foreach (var Object in objectsList)
            {
                if (projectile.DetectBottomCollision(Object))
                {
                    projectile.Direction.Y = Math.Abs(projectile.Direction.Y);
                    projectile.HitCount++;
                }

                if (projectile.DetectTopCollision(Object))
                {
                    projectile.Direction.Y = -Math.Abs(projectile.Direction.Y);
                    projectile.HitCount++;
                }

                if (projectile.DetectRightCollision(Object))
                {
                    projectile.Direction.X = Math.Abs(projectile.Direction.X);
                    projectile.HitCount++;
                }
                if (projectile.DetectLeftCollision(Object))
                {
                    projectile.Direction.X = -Math.Abs(projectile.Direction.X);
                    projectile.HitCount++;
                }
            }

            if (projectile.HitCount > 10)
                projectile.KillProjectile();
        }
    }

    public class Projectile : Sprite
    {
        // ATRIBUTS
        public Vector2 Target { get; }
        private Vector2 origin;
        private Vector2 trajectory;
        public int ShooterID { get; }
        public float Damage { get; }
        public char ProjectileType = 'N'; //N de Normal, D de directe i S de noNewtonian Slimed Salt
        public int HitCount = 0;
        
        
        
        // constrctor per inicialitzar el projectil
        public Projectile(Vector2 origin, Vector2 target, float velocity,int shooterID, Texture2D texture, float scale, float damage, char projectileType)
            : base(texture)
        {
            this.ShooterID = shooterID;
            this.origin = origin;
            this.Target = target;
            this.LinearVelocity = VectorOps.ModuloVector(new Vector2((origin.X - target.X),(origin.Y - target.Y)))/20;
            this._texture = texture;
            this.ProjectileType = projectileType;
            this.trajectory = this.Target - this.origin;
            this.Position = this.origin;
            this.Direction = VectorOps.UnitVector(target - origin);
            this.Scale = scale;
            this.Damage = damage;
            this.Layer = 0.01f;
            //IsSaltShoot = true;
        }

        public bool IsNear(Vector2 position, float threshold)
        {
            if (VectorOps.ModuloVector(new Vector2(this.Position.X - position.X, this.Position.Y - position.Y)) < threshold)
                return true;
            else
                return false;
        }

        // Movem el projectil de forma determinada per la direcció i velocitat linial
        public void Move()
        {
            if (LinearVelocity > 25f)
                LinearVelocity = 25f;
            this.Position += this.Direction * LinearVelocity;

            //Funcionament de canvi d'escala de la sal per donar la sensació d'un moviment parabòlic
            if (ProjectileType == 'N')
            {
                if ((Math.Abs(Position.X - origin.X) < Math.Abs(trajectory.X / 2)) && (Math.Abs(Position.Y - origin.Y) < Math.Abs(trajectory.Y / 2)))
                    this.Scale += 0.003f;
                else
                    this.Scale -= 0.003f;
            }
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
        public bool DetectBottomCollision(Sprite sprite)
        {
            if (sprite != this)
            {
                return this.IsTouchingBottom(sprite);
            }
            else
                return false;
        }
        public bool DetectTopCollision(Sprite sprite)
        {
            if (sprite != this)
            {
                return this.IsTouchingTop(sprite);
            }
            else
                return false;
        }
        public bool DetectRightCollision(Sprite sprite)
        {
            if (sprite != this)
            {
                return this.IsTouchingRight(sprite);
            }
            else
                return false;
        }
        public bool DetectLeftCollision(Sprite sprite)
        {
            if (sprite != this)
            {
                return this.IsTouchingLeft(sprite);
            }
            else
                return false;
        }
    }
}

