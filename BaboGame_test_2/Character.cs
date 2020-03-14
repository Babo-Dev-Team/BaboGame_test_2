using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class Character
{
    private Texture2D _CharTexture;
    public Vector2 _CharPosition;
    public Vector2 _CharOrigin;
    public float CharSpeed = 3f;
    public float CharRotation = 0f;
    public float CharScale = 0.5f;
    public float CharLayer = 0f;
    public SpriteEffects CharEffects = SpriteEffects.None;
    GameTime gameTime;

	public Character(Texture2D _texture)
	{
        _CharTexture = _texture;
	}

    public void Update()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.W)) //Albert8
            {
                _CharPosition.Y -= CharSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S)) //Albert9
            {
                _CharPosition.Y += CharSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A)) //Albert10
            {
                _CharPosition.X -= CharSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D)) //Albert11
            {
                _CharPosition.X += CharSpeed;
            }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_CharTexture,_CharPosition,null, Color.White, CharRotation, _CharOrigin, CharScale, CharEffects, CharLayer);
    }


}
