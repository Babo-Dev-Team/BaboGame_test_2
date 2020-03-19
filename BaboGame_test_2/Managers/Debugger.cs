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
    public class Debugger
    {
        private SpriteFont _font;
        public Debugger (List<Character> CharacterSprite, List<Projectile> ProjectileSprite, List<Sprite> overlaySprite)
        {
            _font = Content.Load<SpriteFont>("Font");
        }
    }
}
