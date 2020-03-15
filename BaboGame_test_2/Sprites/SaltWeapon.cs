﻿using System;
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

    public class SaltWeapon : Sprite
    {
        //Variables per saber el camí de la sal
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
            //Valorar el temps de vida de la sal a disparar i la eliminació d'aquest
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > LifeSpan)
                IsRemoved = true;

            //Definir la posició final de la sal per ser eliminada
            if (((MousePosition.X >= Position.X - 10) && (MousePosition.X <= Position.X + 10)) && ((MousePosition.Y >= Position.Y - 10) && (MousePosition.Y <= Position.Y + 10)))
                IsRemoved = true;

            //Reprodueix el moviment de la sal
            Position += Direction * LinearVelocity;
            
            //Funcionament de canvi d'escala de la sal per donar la sensació d'un moviment parabòlic
            if ((Math.Abs(Position.X - Source.X) < Math.Abs(Destination.X / 2)) && (Math.Abs(Position.Y - Source.Y) < Math.Abs(Destination.Y / 2)))
                Scale += 0.003f;
            else
                Scale -= 0.003f;
        }
    }

