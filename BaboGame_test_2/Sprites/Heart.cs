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
    class Heart : Sprite
    {

        public Heart(Dictionary<string, Animation> animations)
               : base(animations)
        {
            
            SolidObject = false;
            IsHealth = true;

        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Defineix els estats del ratolí
            //previousMouseState = currentMouseState;
            //currentMouseState = Mouse.GetState();

            //Defineix la posició de la mira segons la posició del ratolí
            //Position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);

            Scale = 0.10f;
            Layer = 0.99f;
            //Crida i fa les animacions
            SetAnimation();
            _animationManager.Update(gameTime, 0.2f);
        }

        protected virtual void SetAnimation()
        {
            if (heart_health <= 0)
                _animationManager.Play(_animations["Empty heart"]);
            else if((heart_health <= 0.25)&&(heart_health > 0))
                _animationManager.Play(_animations["1/4 heart"]);
            else if ((heart_health <= 0.5) && (heart_health > 0.25))
                _animationManager.Play(_animations["2/4 heart"]);
            else if ((heart_health <= 0.75) && (heart_health > 0.5))
                _animationManager.Play(_animations["3/4 heart"]);
            else
                _animationManager.Play(_animations["Heart"]);
        }
    }
}
