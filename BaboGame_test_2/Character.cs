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
    public Vector2D _CharPosition;

	public Character(Texture2D _texture)
	{
        _CharTexture = _texture;
	}

    public void Update()
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {

    }


}
