using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BaboGame_test_2
{
    public class Debugger
    {
        private SpriteFont _font;
        private List<Character> _CharacterSprite;
        private List<Projectile> _ProjectileSprite;
        private List<Sprite> _overlaySprite;

        public Debugger (List<Character> CharacterSprite, List<Projectile> ProjectileSprite, List<Sprite> overlaySprite, SpriteFont font)
        {
            _font = font;
            _CharacterSprite = CharacterSprite;
            _ProjectileSprite = ProjectileSprite;
            _overlaySprite = overlaySprite;
        }

        public void DrawText(SpriteBatch spriteBatch)
        {
            var fontY = 10;
            int i = 0;
            foreach (var Character in _CharacterSprite)
            {
                spriteBatch.DrawString(_font, string.Format("Direction: {0}", VectorOps.Vector2ToDeg(Character.Direction)), new Vector2(10, fontY += 20), Color.Black);
            }
        }
    }
}
