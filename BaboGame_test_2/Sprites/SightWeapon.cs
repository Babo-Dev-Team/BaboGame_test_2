using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class SightWeapon : Sprite
{
	public SightWeapon(Texture2D texture)
        : base(texture)
	{

	}

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();

        Position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
    }
}
