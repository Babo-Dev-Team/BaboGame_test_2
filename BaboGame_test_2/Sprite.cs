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
        protected float _rotation;

        //Variables entorn a la detecció de tecles de teclat i el ratolí
        protected KeyboardState currentKey;
        protected KeyboardState previousKey;
        protected MouseState currentMouseState;
        protected MouseState previousMouseState;

        //Variables vector per la posició, el centre i la direcció del personatge
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Direction;

        //Variables físiques del sprite
        public float RotationVelocity = 4f;
        public float LinearVelocity = 8f;
        public float Scale = 1f;
        public float Layer = 0f;
        public SpriteEffects Effect = SpriteEffects.None;

        //Variable per relacionar sprites
        public Sprite Parent;

        //Variables de temps de vida i eliminació de sprites
        public float LifeSpan = 0f;
        public bool IsRemoved = false;
        
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
            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, Scale, Effect, Layer);
        }

        //Funció per clonar els sprites
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

