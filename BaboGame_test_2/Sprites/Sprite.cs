using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

/*
 * Codi que defineix les característiques bàsiques d'un "sprite"
 * Aquest codi permet ser clonat per elaborar futurs "sprites" sense haver
 * de crear tot des del començament
 */


    public class Sprite : ICloneable //Aquesta funció es reproduirà per tots els sprites; per tant, ha de ser clonable
    {
        //Variables globals d'un "Sprite"
        protected Texture2D _texture;
        public float Scale = 1f;
        public float Layer = 0f;
        public SpriteEffects Effect = SpriteEffects.None;
        public Color _color = Color.White;

        //Variables entorn a la detecció de tecles del teclat i el ratolí; i les seves entrades
        protected KeyboardState currentKey;
        protected KeyboardState previousKey;
        protected MouseState currentMouseState;
        protected MouseState previousMouseState;
        public Input Input;

        //Variables vector per la posició, el centre i la direcció del personatge
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Direction;
        public Vector2 Velocity;
    
        //Variables físiques del sprite
        public float RotationVelocity = 4f;
        public float LinearVelocity = 8f;
        protected float _rotation;
        

        //Variable per relacionar sprites
        public Sprite Parent;

        //Variables de temps de vida i eliminació de sprites
        public float LifeSpan = 0f;
        public bool IsRemoved = false;

        //Variables relacionades amb la col·lisió
        public float HitBoxScale = 1f;
        public bool SolidObject = true;
        
        //Funció principal per definir el sprite generat
        public Sprite(Texture2D texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        //Funció per dibuixar el sprite en pantalla
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, _color, _rotation, Origin, Scale, Effect, Layer);
        }

        //Funció per clonar els sprites
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    

        //Objecte que farà de "Hitbox" del sprite
        public Rectangle Rectangle
        {
            get
            {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)(_texture.Width*Scale*HitBoxScale), (int)(_texture.Height*Scale*HitBoxScale));
            }
        }

    //Definició de col·lisió del sprite
    #region Collision
    protected bool IsTouchingLeft(Sprite sprite)
    {
        return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Left &&
                this.Rectangle.Bottom > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Bottom;
    }

    protected bool IsTouchingRight(Sprite sprite)
    {
        return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
                this.Rectangle.Right > sprite.Rectangle.Right &&
                this.Rectangle.Bottom > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Bottom;
    }

    protected bool IsTouchingTop(Sprite sprite)
    {
        return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
                this.Rectangle.Top < sprite.Rectangle.Top &&
                this.Rectangle.Right > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Right;
    }

    protected bool IsTouchingBottom(Sprite sprite)
    {
        return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
                this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
                this.Rectangle.Right > sprite.Rectangle.Left &&
                this.Rectangle.Left < sprite.Rectangle.Right;
    }
    #endregion
}

