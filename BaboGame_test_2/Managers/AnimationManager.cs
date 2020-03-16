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


    public class AnimationManager
    {
        //Variables entor a l'animació
        private Animation _animation;
        private float _timer;
        public Vector2 Position { get; set; }

        //Variables entorn el dibuixat de la imatge i relació amb el sprite
        public float _rotation = 0f;
        public Vector2 AOrigin;
        public SpriteEffects Aeffects = SpriteEffects.None;
        public float ALayer = 0f;
        public float Ascale = 1f;
        public float AHitBoxScale = 1f;
        public Color Acolor = Color.White;

        //Funció principal
        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        //Dibuix de l'animació
        public void Draw(SpriteBatch spriteBatch)
        {
        AOrigin = new Vector2(_animation.FrameWidth /2, _animation.FrameHeight /2);
        spriteBatch.Draw(_animation.Texture,
                        Position,
                        new Rectangle(_animation.CurrentFrame * _animation.FrameWidth,
                                        0,
                                        _animation.FrameWidth,
                                        _animation.FrameHeight),
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

            _animation.CurrentFrame = 0;

            _timer = 0;
        }

        //Parada de l'animació
        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;
        }

        //Actualització de cada frame en una animació
        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;

                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }

        //Hitbox de l'animació per ser utilitzat per la classe "Sprite"
        public Rectangle AnimationRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)(_animation.FrameWidth* Ascale * AHitBoxScale), (int)(_animation.FrameHeight * Ascale * AHitBoxScale));
    }
    }

