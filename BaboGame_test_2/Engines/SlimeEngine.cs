﻿using System;
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

        public void UpdateSlime(GameTime gameTime, List<Character> characterList, List<Projectile> projectileList, List<ScenarioObjects> scenarioList)
        {
            foreach (var slime in slimeList)
            {
                //Salinització de les babes
                foreach (var projectile in projectileList)
                {
                    if ((slime.IsNear(projectile.Position)) && (slime.IsNear(projectile.Target)) && (!slime.IsSalted) && (projectile.ProjectileType == 'N') && (projectile.IsNear(projectile.Position, 5))) //slime.DetectCollision(projectile) 
                        slime.IsSalted = true;
                }

                //if (slime.IsSalted)
                //slime._color = Color.Silver;

                //Destrucció de les babes cada un cert temps
                if ((slime.timer > 20) && (!slime.IsSalted))
                    slime.KillSlime();
                else if (slime.timer > 200)
                    slime.KillSlime();

                slime.Layer = 0.001f - slime.timer * 0.00001f;


                //Conducció de les babes
                bool sourceConnect = false;
                foreach (var scenObject in scenarioList)
                {
                    if ((scenObject.HasConducitivity) && (slime.DetectCollision(scenObject)) && (slime.IsSalted))
                    {
                        if (scenObject.Charge == 'N')
                        {
                            slime.NegativeCurrent = 0;
                            sourceConnect = true;
                        }
                        else if (scenObject.Charge == 'P')
                        {
                            slime.PositiveCurrent = 0;
                            sourceConnect = true;
                        }
                        else
                            sourceConnect = false;
                    }
                    else
                        sourceConnect = false;
                }

                if ((slime.IsSalted) && ((slime.NegativeCurrent < 8)|| (slime.PositiveCurrent < 8)))
                { 
                    foreach (var neighborSlime in slimeList)
                    {
                        if ((neighborSlime != slime) && (slime.DetectCollision(neighborSlime)) && (neighborSlime.IsSalted))
                        {

                            if (neighborSlime.PositiveCurrent > slime.PositiveCurrent)
                                neighborSlime.PositiveCurrent = slime.PositiveCurrent + 1;

                            if (neighborSlime.NegativeCurrent > slime.NegativeCurrent)
                                neighborSlime.NegativeCurrent = slime.NegativeCurrent + 1;
                        }
                    }
                }

                if ((slime.PositiveCurrent < 8) && (slime.NegativeCurrent < 8))
                    slime._color = Color.Violet;
                else if ((slime.PositiveCurrent < 8))
                    slime._color = Color.Red;
                else if ((slime.NegativeCurrent < 8))
                    slime._color = Color.Blue;
                else if (slime.IsSalted)
                    slime._color = Color.Silver;
                else
                    slime._color = Color.White;

            }          
        }
    }

    public class Slime : Sprite
    {
        // ATRIBUTS
        public int timer=0;
        private Vector2 origin;
        public int ShooterID { get; }
        public bool IsSalted;
        private float radiumSlime = 200f;
        public int PositiveCurrent = 16;
        public int NegativeCurrent = 16;

        // constrctor per inicialitzar la baba
        public Slime(Vector2 origin, int shooterID, Texture2D texture, float scale)
            : base(texture)
        {
            this.ShooterID = shooterID;
            this.origin = origin;
            this.LinearVelocity = 0f;
            this._texture = texture;
            this.Position = this.origin;
            this.Scale = scale;
            this.radiumSlime = texture.Width * scale * 1f;
            this.Layer = 0f;
            this.SolidObject = false;
            this.HitBoxScale = 1f;
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
