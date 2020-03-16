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

    public SightWeapon(Dictionary<string, Animation> animations)
           : base(animations)
    {

    }

    public override void Update(GameTime gameTime, List<Sprite> sprites)
    {
        //Defineix els estats del ratolí
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();

        //Defineix la posició de la mira segons la posició del ratolí
        Position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);

        //Crida i fa les animacions
        SetAnimation();
        _animationManager.Update(gameTime);
    }

    //Animació de pulsar el botó
    protected virtual void SetAnimation()
    {
        if ((currentMouseState.LeftButton == ButtonState.Pressed))
            _animationManager.Play(_animations["ON"]);
        else
            _animationManager.Play(_animations["OFF"]);
    }
}
