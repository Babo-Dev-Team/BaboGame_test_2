using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

/*
 * Classe que desenvolupa el funcionament d'una animació al mateix moment
 */

namespace BaboGame_test_2
{
    public class AnimationManager
    {
        //Variables entor a l'animació
        private Animation _animation;
        private float _timer;
        public Vector2 Position { get; set; }
        private int currentFrame;

        //Variables entorn el dibuixat de la imatge i relació amb el sprite
        public float _rotation = 0f;
        public Vector2 AOrigin;
        public SpriteEffects Aeffects = SpriteEffects.None;
        public float ALayer = 0f;
        public float Ascale = 1f;
        public float AHitBoxScale = 1f;
        public Color Acolor = Color.White;
        private Rectangle frameRectangle;

        //Funció principal
        public AnimationManager(Animation animation)
        {
            _animation = animation;
            this._timer = 0;
            this.currentFrame = 0;
        }

        //Dibuix de l'animació
        public void Draw(SpriteBatch spriteBatch)
        {
            AOrigin = new Vector2(_animation.FrameWidth / 2, _animation.FrameHeight / 2);
            spriteBatch.Draw(_animation.Texture,
                        Position,
                        frameRectangle,
                        Acolor,
                        _rotation,
                        AOrigin,
                        Ascale,
                        Aeffects,
                        ALayer);
        }

        //Reproducció de l'animació
        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;

            if(this.currentFrame > _animation.FrameCount)
            {
               this.currentFrame = 0;
            }

            frameRectangle = new Rectangle(this.currentFrame * _animation.FrameWidth,
                                        0,
                                        _animation.FrameWidth,
                                        _animation.FrameHeight);
        }

        //Parada de l'animació
        public void Stop()
        {
            _timer = 0f;

            this.currentFrame = 0;
        }

        //Actualització de cada frame en una animació
        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;

                this.currentFrame++;

                if (this.currentFrame >= _animation.FrameCount)
                    this.currentFrame = 0;

                this.frameRectangle.X = currentFrame * _animation.FrameWidth;
            }
        }

        //Hitbox de l'animació per ser utilitzat per la classe "Sprite"
        public Rectangle AnimationRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)(_animation.FrameWidth * Ascale * AHitBoxScale), (int)(_animation.FrameHeight * Ascale * AHitBoxScale));
        }
    }
}
