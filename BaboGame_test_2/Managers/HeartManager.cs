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
    class HeartManager
    {
        private Dictionary<string, Animation> _animation;
        private List<Sprite> overlaySprites;

        public HeartManager(List<Sprite> overlaySprites)
        {
            this.overlaySprites = overlaySprites;
        }

        public void CreateHeart(int heartIDgiven, int heartNumber, int health, Dictionary<string, Animation> _animation, Vector2 position)
        {
            float h = health;
            float currentHealth=1;
            this._animation = _animation;
            for (int i = 0; i < heartNumber; i++)
            {
                if ((h - 4 * i) / 4 >= 1)
                {
                    currentHealth = 1f;
                }
                else if ((h - 4 * i) / 4 < 0)
                {
                    currentHealth = 0f;
                }
                else
                    currentHealth = (h - 4 * i) / 4;

                overlaySprites.Add(
                    new Heart(_animation)
                    {
                        Position = new Vector2(position.X + i * 30, position.Y),
                        hearthPosition = i,
                        heart_health = currentHealth,
                        IDcharacter = heartIDgiven,
                    }
                    );
            }

        }

        public void UpdateHealth(int id, int health)
        {
            float h = health;
            int heartNumber = 0;
            float currentHealth = 0f;
            foreach (var sprite in overlaySprites)
            {
                if ((sprite.IsHealth) && (sprite.IDcharacter == id))
                    heartNumber++;
            }
            for (int i = 0; i < heartNumber; i++)
            {
                if ((h - 4 * i) / 4 >= 1)
                {
                    currentHealth = 1f;
                }
                else if ((h - 4 * i) / 4 < 0)
                {
                    currentHealth = 0f;
                }
                else
                    currentHealth = (h - 4*i)/4;

                foreach (var sprite in overlaySprites)
                {
                    if ((sprite.IsHealth) && (sprite.IDcharacter == id)&&(sprite.hearthPosition==i))
                        sprite.heart_health = currentHealth;
                        
                }
            }
        }
    }
}