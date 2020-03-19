using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

/*
 * Classe que defineix el funcionament d'una sola animació
 */


    public class Animation
    {
        //Variables entorn a les dimensions de l'animació
        public int FrameHeight { get { return Texture.Height; } }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public int FrameCount { get; private set; }

        //Variables entorn a la reproducció de la imatge
        //public int CurrentFrame { get; set; }
        public float FrameSpeed { get; set; }
        public bool IsLooping { get; set; }
        
        //Imatge de l'animació
        public Texture2D Texture { get; private set; }

        //Funció principal
        public Animation (Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            IsLooping = true;

            FrameSpeed = 0.2f;
        }
    }

