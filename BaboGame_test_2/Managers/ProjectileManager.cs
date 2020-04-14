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
    class ProjectileManager
    {
        char ProjectileType = 'N';
        private Dictionary<string, Texture2D> textures;
        private ProjectileEngine projectileEngine;
        public int WheelValue = 0;
        public int typePosition = 0;
        public int ProjectileNum = 3;
        public char[] TypeVector = { 'N', 'D', 'S' };
        float SaltMenuTimer = 0;
        bool visible = false;


        public ProjectileManager(Dictionary<string, Texture2D> _textures, ProjectileEngine _projectileEngine)
        {
            textures = _textures;
            projectileEngine = _projectileEngine;

        }
        public void Update(GameTime gameTime, int WheelValue, List<Sprite> overlaySprites, List<Character> characterSprites)
        {

            //Considera si ha girat la rodeta del ratolí o no
            if(WheelValue >= this.WheelValue + 120)
            {
                this.WheelValue += 120;
                typePosition++;
                SaltMenuTimer = 0f;
                visible = true;
            }
            if (WheelValue <= this.WheelValue - 120)
            {
                this.WheelValue -= 120;
                typePosition--;
                SaltMenuTimer = 0f;
                visible = true;
            }

            //Canviar el projectil seleccionat
            if (typePosition >= ProjectileNum)
                typePosition = 0;
            if (typePosition < 0)
                typePosition = ProjectileNum - 1;
            ProjectileType = TypeVector[typePosition];

            //Posa la posició del menú, i el fa interactiu
            foreach (var overlay in overlaySprites)
            {
                if (overlay.IsMenuSalt)
                {
                    foreach (var character in characterSprites)
                    {
                        if(overlay.IDcharacter == character.IDcharacter)
                        {
                            //Segueix la posició del personatge
                            if(overlay.MenuPos == 0)
                                overlay.Position = new Vector2(character.Position.X, character.Position.Y - 80);
                            else
                            {
                                overlay.Position = new Vector2(character.Position.X + 40*overlay.MenuPos - 80, character.Position.Y - 80);

                                //Canvia la mida del item segons si està seleccionat o no
                                if (overlay.MenuPos == typePosition + 1)
                                    overlay.Scale = 0.18f;
                                else
                                    overlay.Scale = 0.1f;
                            }

                            //Fa visibles o invisibles els items segons si es vol deixar visible o no en aquell moment (limita el mètode Draw)
                            if (visible)
                                overlay.Visible = true;
                            else
                                overlay.Visible = false;
                        }
                    }
                }

            }

            //Amaga el menú després d'un cert temps
            if (visible)
                SaltMenuTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (SaltMenuTimer > 3f)
            {
                SaltMenuTimer = 0f;
                visible = false;
            }
        }

        public void AddProjectile (Vector2 projectileOrigin, Vector2 projectileTarget, int shooterID)
        {
            if (ProjectileType == 'D')
                projectileEngine.AddProjectile(projectileOrigin, projectileTarget, textures["Direct"], shooterID, 'D');
            else if (ProjectileType == 'S')
                projectileEngine.AddProjectile(projectileOrigin, projectileTarget, textures["Slimed"], shooterID, 'S');
            else
                projectileEngine.AddProjectile(projectileOrigin, projectileTarget, textures["Normal"], shooterID, 'N');
        }

        public void CreateSaltMenu(Texture2D _menuTexture, List<Sprite> overlaySprites, int IDcharacter, float scale)
        {
            overlaySprites.Add(new ProjectileMenu(_menuTexture, IDcharacter, scale,0));
            overlaySprites.Add(new ProjectileMenu(this.textures["Normal"], IDcharacter, scale,1));
            overlaySprites.Add(new ProjectileMenu(this.textures["Direct"], IDcharacter, scale,2));
            overlaySprites.Add(new ProjectileMenu(this.textures["Slimed"], IDcharacter, scale,3));
        }
    }

    public class ProjectileMenu : Sprite
    {
        public ProjectileMenu(Texture2D texture, int IDcharacter, float scale, int MenuPos) : base(texture)
        {
            Layer = 1;
            if (MenuPos == 0)
                Layer = 0.99f;
            this.IDcharacter = IDcharacter;
            SolidObject = false;
            this.Scale = scale;
            this.IsMenuSalt = true;
            this.MenuPos = MenuPos;
            this.Visible = false;

        }

        public override void Update(GameTime gameTime, List<Sprite> overlatSprites)
        {

        }
    }
}
