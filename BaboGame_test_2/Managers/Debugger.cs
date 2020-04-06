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
        private List<Slime> _slimeSprites;
        private double _timer1;
        private int heightscreen, widthscreen;

        public Debugger (List<Character> CharacterSprite, List<Projectile> ProjectileSprite, List<Sprite> overlaySprite, List<Slime> slimeSprite,double timer1,int widthscreen,int heightscreen, SpriteFont font)
        {
            _font = font;
            _CharacterSprite = CharacterSprite;
            _ProjectileSprite = ProjectileSprite;
            _overlaySprite = overlaySprite;
            _slimeSprites = slimeSprite;
            _timer1 = timer1;
            this.heightscreen = heightscreen;
            this.widthscreen = widthscreen;
        }

        public void DrawText(SpriteBatch spriteBatch)
        {
            var fontY = 10;

            foreach (var Character in _CharacterSprite)
            {
                spriteBatch.DrawString(_font, string.Format("Direction: {0}    Velocity: {1}    Slipping:{2}", VectorOps.Vector2ToDeg(Character.Direction), Character.VelocityInform, Character.isSlip), new Vector2(10, fontY += 20), Color.Black);
                
            }

            spriteBatch.DrawString(_font, string.Format("Mouse Position: {0}", new Vector2(Mouse.GetState().X,Mouse.GetState().Y)), new Vector2(10, fontY += 20), Color.Black);
            spriteBatch.DrawString(_font, string.Format("SlimeTimer: {0}", _timer1), new Vector2(10, fontY += 20), Color.Black);
            spriteBatch.DrawString(_font, string.Format("Screen Dimensions: {0}", new Vector2(widthscreen, heightscreen)), new Vector2(10, fontY += 20), Color.Black);
        
    }
    }
}
