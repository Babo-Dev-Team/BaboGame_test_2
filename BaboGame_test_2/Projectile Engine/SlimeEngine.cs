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
    //La idea d'aquesta classe és extreure tota la lògica de les babes.
    //No s'ha considerat com a projectil ja que la única interacció que fan amb ells és si el Slime és el objectiu
    //Obligaria a fer massa condicions separades
    class SlimeEngine
    {
        private List<Slime> slimeList;

        public SlimeEngine(List<Slime> slimeList)
        {
            this.slimeList = slimeList;
        }

        public void AddSlime(Vector2 origin, int shooterID, Texture2D texture, float scale)
        {
            slimeList.Add(new Slime(origin,shooterID,texture,scale));
        }

        public void UpdateSlime(GameTime gameTime, List<Character> characterList, List<Projectile> projectileList)
        {
                foreach (var slime in slimeList)
                {
                    foreach (var projectile in projectileList)
                    {
                        if (slime.DetectCollision(projectile) && (slime.IsNear(projectile.target)) && (!slime.IsSalted))
                            slime.IsSalted = true;
                    }

                    foreach (var character in characterList)
                    {
                    if (slime.DetectCollision(character) && (slime.shooterID != character.IDcharacter))
                        character.NotifySlip();
                            
                    }

                    if (slime.IsSalted)
                        slime._color = Color.Silver;

                if ((slime.timer > 20) && (!slime.IsSalted))
                    slime.KillSlime();
                else if (slime.timer > 200)
                    slime.KillSlime();

                slime.Layer = 0.001f - slime.timer * 0.00001f; 

                }
                
                
        }
    }

    public class Slime : Sprite
    {
        // ATRIBUTS
        public int timer=0;
        private Vector2 origin;
        public int shooterID { get; }
        public bool IsSalted;
        private float radiumSlime = 10f;

        // constrctor per inicialitzar la baba
        public Slime(Vector2 origin, int shooterID, Texture2D texture, float scale)
            : base(texture)
        {
            this.shooterID = shooterID;
            this.origin = origin;
            this.LinearVelocity = 0f;
            this._texture = texture;
            this.Position = this.origin;
            this.Scale = scale;
            this.radiumSlime = texture.Width * scale / 2;
            this.Layer = 0f;
            this.SolidObject = false;
            this.HitBoxScale = 0.8f;
            //IsSaltShoot = true;
        }

        public void KillSlime()
        {
            this.IsRemoved = true;
        }

        public bool DetectCollision(Sprite sprite)
        {
            if (sprite != this)
            {
                return this.IsTouchingBottom(sprite) || this.IsTouchingLeft(sprite) || this.IsTouchingRight(sprite) || this.IsTouchingTop(sprite);
            }
            else
                return false;
        }

        public bool IsNear(Vector2 position)
        {
            if (VectorOps.ModuloVector(new Vector2(this.Position.X - position.X,this.Position.Y - position.Y)) < radiumSlime)
                return true;
            else
                return false;
        }
    }
}
