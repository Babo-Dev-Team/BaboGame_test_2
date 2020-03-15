using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

/*
 * Codi que defineix el funcionament de la mira per saber la posició del ratolí
 * Aquest seguirà la posició del ratolí en tot moment
 */
public class SightWeapon : Sprite
{
	public SightWeapon(Texture2D texture)
        : base(texture)
	{

	}

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        //Defineix els estats del ratolí
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();

        //Defineix la posició de la mira segons la posició del ratolí
        Position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
    }
}
