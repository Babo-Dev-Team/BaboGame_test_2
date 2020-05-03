using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BaboGame_test_2
{

    /*
     * Defineix el sprite del personatge que el jugador controlarà
     */
    public class CharacterEngine
    {
        List<Character> characterList;

        //Funcíó per trobar el contingut de les imatges
        ContentManager Content;
        Dictionary<string, Animation> BaboAnimations;
        Dictionary<string, Animation> LimaxAnimations;

        public CharacterEngine(List<Character> characterList, ContentManager Content)
        {
            this.characterList = characterList;

            //Imatges dels personatges
            BaboAnimations = new Dictionary<string, Animation>()
            {
                {"Slug down0", new Animation(Content.Load<Texture2D>("Babo/Babo down0"), 6) },
                {"Slug up0", new Animation(Content.Load<Texture2D>("Babo/Babo up0"), 6) },
                {"Slug down0 bck", new Animation(Content.Load<Texture2D>("Babo/Babo down0 s0"), 1) },
                {"Slug up0 bck", new Animation(Content.Load<Texture2D>("Babo/Babo up0 s0"), 1) },
                {"Slug right0", new Animation(Content.Load<Texture2D>("Babo/Babo right0"), 6) },
                {"Slug left0", new Animation(Content.Load<Texture2D>("Babo/Babo left0"), 6) },
                {"Slug down22_5", new Animation(Content.Load<Texture2D>("Babo/Babo down22_5"), 6) },
                {"Slug up22_5", new Animation(Content.Load<Texture2D>("Babo/Babo up22_5"), 6) },
                {"Slug right22_5", new Animation(Content.Load<Texture2D>("Babo/Babo right22_5"), 6) },
                {"Slug left22_5", new Animation(Content.Load<Texture2D>("Babo/Babo left22_5"), 6) },
                {"Slug down45", new Animation(Content.Load<Texture2D>("Babo/Babo down45"), 6) },
                {"Slug up45", new Animation(Content.Load<Texture2D>("Babo/Babo up45"), 6) },
                {"Slug right45", new Animation(Content.Load<Texture2D>("Babo/Babo right45"), 6) },
                {"Slug left45", new Animation(Content.Load<Texture2D>("Babo/Babo left45"), 6) },
                {"Slug down-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo down-22_5"), 6) },
                {"Slug up-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo up-22_5"), 6) },
                {"Slug right-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo right-22_5"), 6) },
                {"Slug left-22_5", new Animation(Content.Load<Texture2D>("Babo/Babo left-22_5"), 6) },
                {"Slug down-45", new Animation(Content.Load<Texture2D>("Babo/Babo down-45"), 6) },
                {"Slug up-45", new Animation(Content.Load<Texture2D>("Babo/Babo up-45"), 6) },
                {"Slug right-45", new Animation(Content.Load<Texture2D>("Babo/Babo right-45"), 6) },
                {"Slug left-45", new Animation(Content.Load<Texture2D>("Babo/Babo left-45"), 6) },
                {"Slug up hit", new Animation(Content.Load<Texture2D>("Babo/Babo up hit"), 1) },
                {"Slug down hit", new Animation(Content.Load<Texture2D>("Babo/Babo down hit"), 1) },
                {"Slug right hit", new Animation(Content.Load<Texture2D>("Babo/Babo right hit"), 1) },
                {"Slug left hit", new Animation(Content.Load<Texture2D>("Babo/Babo left hit"), 1) },

            };
            LimaxAnimations = new Dictionary<string, Animation>()
            {
                {"Slug down0", new Animation(Content.Load<Texture2D>("Limax/Limax down0"), 6) },
                {"Slug up0", new Animation(Content.Load<Texture2D>("Limax/Limax up0"), 6) },
                {"Slug down0 bck", new Animation(Content.Load<Texture2D>("Limax/Limax down0 s0"), 1) },
                {"Slug up0 bck", new Animation(Content.Load<Texture2D>("Limax/Limax up0 s0"), 1) },
                {"Slug right0", new Animation(Content.Load<Texture2D>("Limax/Limax right0"), 6) },
                {"Slug left0", new Animation(Content.Load<Texture2D>("Limax/Limax left0"), 6) },
                {"Slug down22_5", new Animation(Content.Load<Texture2D>("Limax/Limax down22_5"), 6) },
                {"Slug up22_5", new Animation(Content.Load<Texture2D>("Limax/Limax up22_5"), 6) },
                {"Slug right22_5", new Animation(Content.Load<Texture2D>("Limax/Limax right22_5"), 6) },
                {"Slug left22_5", new Animation(Content.Load<Texture2D>("Limax/Limax left22_5"), 6) },
                {"Slug down45", new Animation(Content.Load<Texture2D>("Limax/Limax down45"), 6) },
                {"Slug up45", new Animation(Content.Load<Texture2D>("Limax/Limax up45"), 6) },
                {"Slug right45", new Animation(Content.Load<Texture2D>("Limax/Limax right45"), 6) },
                {"Slug left45", new Animation(Content.Load<Texture2D>("Limax/Limax left45"), 6) },
                {"Slug down-22_5", new Animation(Content.Load<Texture2D>("Limax/Limax down-22_5"), 6) },
                {"Slug up-22_5", new Animation(Content.Load<Texture2D>("Limax/Limax up-22_5"), 6) },
                {"Slug right-22_5", new Animation(Content.Load<Texture2D>("Limax/Limax right-22_5"), 6) },
                {"Slug left-22_5", new Animation(Content.Load<Texture2D>("Limax/Limax left-22_5"), 6) },
                {"Slug down-45", new Animation(Content.Load<Texture2D>("Limax/Limax down-45"), 6) },
                {"Slug up-45", new Animation(Content.Load<Texture2D>("Limax/Limax up-45"), 6) },
                {"Slug right-45", new Animation(Content.Load<Texture2D>("Limax/Limax right-45"), 6) },
                {"Slug left-45", new Animation(Content.Load<Texture2D>("Limax/Limax left-45"), 6) },
                {"Slug up hit", new Animation(Content.Load<Texture2D>("Limax/Limax up hit"), 1) },
                {"Slug down hit", new Animation(Content.Load<Texture2D>("Limax/Limax down hit"), 1) },
                {"Slug right hit", new Animation(Content.Load<Texture2D>("Limax/Limax right hit"), 1) },
                {"Slug left hit", new Animation(Content.Load<Texture2D>("Limax/Limax left hit"), 1) },

            };
        }

        public void Update(GameTime gameTime, List<Slime> slimeList, List<ScenarioObjects> scenarioList)
        {
            foreach(var character in characterList)
            {
                // si tenim un impacte, desplaçament per impacte
                if (character.isHit)
                {
                    character.Velocity = Vector2.Zero; //Per compensar la cancelació de la velocitat la posaré abans d'agafar la direcció del cop
                    character.Velocity.X += character.hitImpulse * character.hitDirection.X/5;
                    character.Velocity.Y += character.hitImpulse * character.hitDirection.Y/5;
                }
                else
                {
                    character.SlugSlipUpdate();
                    character.UpdateFriction();
                    character.UpdateForce();
                    character.UpdateVelocity();
                }
            

                character.UpdateCollision(scenarioList);
                character.UpdateCharCollision(characterList);
                character.Position += character.Velocity;


                character.VelocityInform = character.Velocity;
                //Velocity = Vector2.Zero; -- Ara la velocitat es conservarà
                character.Acceleration = Vector2.Zero;

                //Fem que els llimacs rellisquin amb les babes dels contrincants
                foreach(var slime in slimeList)
                {
                    if (character.DetectCollisions(slime) && (slime.ShooterID != character.IDcharacter))
                        character.isSlip = true;
                }
            }
        }

        public void AddCharacter(Dictionary<string, Animation> slugAnimations, Vector2 Position, float Scale, float HitBoxScaleW, float HitBoxScaleH, int Health, int IDCharacter, Color color)
        {
            characterList.Add(new Character(slugAnimations,Position,Scale,HitBoxScaleW,HitBoxScaleH,Health,IDCharacter,color));
        }

        public void AddCharacter(Dictionary<string, Animation> slugAnimations, Vector2 Position, float Scale, float HitBoxScale, int Health, int IDCharacter, Color color)
        {
            characterList.Add(new Character(slugAnimations, Position, Scale, HitBoxScale, Health, IDCharacter, color));
        }

        //Cerca llimacs ja coneguts
        public void AddKnownCharacter(string slugName, Vector2 Position, float Scale, int Health, int IDCharacter, Color color)
        {
            if(slugName == "Babo")
                characterList.Add(new Character(BaboAnimations, Position, Scale, 0.6f, Health, IDCharacter, color));
            else if (slugName == "Limax")
                characterList.Add(new Character(LimaxAnimations, Position, Scale, 0.5f, Health, IDCharacter, color));
        }


    }

    public class Character : Sprite
    {
        public int Health = 20;

        //Nous valors, farem les físiques dels llimacs més físicament realistes per millorar les mecàniques dels Babos
        public float Weight = 10; 
        public Vector2 Acceleration = new Vector2(0,0); 
        public float LinearAcceleration = 2f;
        public Vector2 Force = new Vector2(0,0);
        private float Friction = 1f;
        private float Velocity_Threshold = 12f;

        // Constructors
        public Character(Texture2D texture)
            : base(texture)
        {
            isHit = false;
        }

        public Character(Dictionary<string, Animation> animations)
           : base(animations)
        {
            isHit = false;
        }

        public Character(Dictionary<string, Animation> animations, Vector2 _Position, float _Scale, float _HitBoxScaleW, float _HitBoxScaleH, int _Health, int _IDcharacter, Color _Color)
           : base(animations)
        {
            Position = _Position;
            Scale = _Scale;
            HitBoxScaleW = _HitBoxScaleW;
            HitBoxScaleH = _HitBoxScaleH;
            Health= _Health;
            IDcharacter = _IDcharacter;
            _color = _Color;
            isHit = false;
        }

        public Character(Dictionary<string, Animation> animations, Vector2 _Position, float _Scale, float _HitBoxScale, int _Health, int _IDcharacter, Color _Color)
           : base(animations)
        {
            Position = _Position;
            Scale = _Scale;
            HitBoxScale = _HitBoxScale;
            Health = _Health;
            IDcharacter = _IDcharacter;
            _color = _Color;
            isHit = false;
        }

        // interfície pública per moure el character
        public void MoveLeft()
        {
            if (!isHit)
            {
                //this.Velocity.X -= this.LinearVelocity;
                if(!isSlip)
                    this.Acceleration.X -= this.LinearAcceleration;
                else
                    this.Acceleration.X -= this.LinearAcceleration/4;
            }
        }
        public void MoveRight()
        {
            if (!isHit)
            {
                //this.Velocity.X += this.LinearVelocity;
                if(!isSlip)
                    this.Acceleration.X += this.LinearAcceleration;
                else
                    this.Acceleration.X += this.LinearAcceleration/4;
            }
        }
        public void MoveUp()
        {
            if (!isHit)
            {
                //this.Velocity.Y -= this.LinearVelocity;
                if(!isSlip)
                    this.Acceleration.Y -= this.LinearAcceleration;
                else
                    this.Acceleration.Y -= this.LinearAcceleration/4;
            }
        }
        public void MoveDown()
        {
            if (!isHit)
            {
                //this.Velocity.Y += this.LinearVelocity;
                if(!isSlip)
                    this.Acceleration.Y += this.LinearAcceleration;
                else
                    this.Acceleration.Y += this.LinearAcceleration/4;
            }
        }

        // Identificadors per la colisió amb projectils
        // mètode públic per restringir l'accés al flag isHit (mantenir-lo privat)
        public bool isHit;
        public bool IsHit()
        {
            return this.isHit;
        }
        // Identificar la relliscada amb les babes del contrincant
        public bool isSlip;
        //private Vector2 _previousLinearVelocity;
        //private Vector2 _previousDirection;

        public Vector2 hitDirection;
        public float hitImpulse;
        float _PainTimer = 0f;
        public Vector2 VelocityInform;

        // Detectem colisions i actualitzem posicions, timers i flags del character
        public void Update(GameTime gameTime, List<Character> characterSprites)
        {
            if (isHit)
                _PainTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_PainTimer > 0.3f)
            {
                _PainTimer = 0f;
                isHit = false;
            }

            //Reprodueix l'animació
            SetAnimations();
            float Framespeed = 0.2f *4/ (4 + VectorOps.ModuloVector(VelocityInform));

            this._animationManager.Update(gameTime, Framespeed);

            //"Equació" per definir a quina capa es mostrarà el "sprite" perquè un personatge no li estigui trapitjant la cara al altre
            float LayerValue = this.Position.Y / 10000;
            if (LayerValue > 0.4)
                Layer = 0.4f;
            else
                Layer = LayerValue + 0.01f;
        }

       
        // detectem colisions amb altres jugadors
        public bool DetectCharCollisions(List<Character> charSprites)
        {
            bool collisionDetected = false;
            foreach (var character in charSprites)
            if (character != this)
            {
                collisionDetected = this.IsTouchingBottom(character) || this.IsTouchingLeft(character) 
                                  || this.IsTouchingRight(character) || this.IsTouchingTop(character);
            }
            return collisionDetected;
        }

        //------------------------------------------- Funcions per les noves mecàniques -----------------------------------------------
        //Calcula el vector de força
        public void UpdateForce()
        {
            Force = Acceleration*Weight;
        }
        //Actualitza la velocitat segons l'acceleració actual
        public void UpdateVelocity()
        {
            Velocity += Acceleration;
            if(VectorOps.ModuloVector(Velocity) > Velocity_Threshold)
                Velocity = VectorOps.UnitVector(Velocity)*Velocity_Threshold;
        }
        //Aplica els efectes del fregament en el moviment
        public void UpdateFriction()
        {
            float VelocityModulo = VectorOps.ModuloVector(Velocity);
            VelocityModulo -= Friction;
            if (VelocityModulo > 0)
                Velocity = VectorOps.UnitVector(Velocity)*VelocityModulo;
            else
                Velocity = Vector2.Zero;
        }
        //Modifica el fregament segons si el llimac rellisca o no
        public void SlugSlipUpdate()
        {
            if(isSlip)
                Friction = 0.01f;
            else
                Friction = 1f;
        }

        //Actualitza la collisió
        public void UpdateCharCollision(List<Character> charSprites)
        {
            foreach (var character in charSprites)
            {
                if(character != this)
                {
                    if(this.IsTouchingBottom(character) || this.IsTouchingTop(character))
                    {
                        Velocity.Y = character.Force.Y/character.Weight;
                        
                    }
                    if (this.IsTouchingLeft(character) || this.IsTouchingRight(character))
                    {
                        Velocity.X = character.Force.X/character.Weight;
                        
                    }
                }
            }
        }
        
        //Detectar la col·lisió amb altres objectes
        public bool DetectCollisions(Sprite sprite)
        {
            bool collisionDetected = this.IsTouchingBottom(sprite) || this.IsTouchingLeft(sprite)
                                      || this.IsTouchingRight(sprite) || this.IsTouchingTop(sprite);
                
            return collisionDetected;
        }
        
        //Actualitza la collisió dels objectes
        public void UpdateCollision(List<ScenarioObjects> scenarioObjects)
        {
            foreach (var objectItem in scenarioObjects)
            {
                if(this.IsTouchingBottom(objectItem) || this.IsTouchingTop(objectItem))
                {
                    this.Velocity.Y = - this.Force.Y/this.Weight;
                        
                }
                if (this.IsTouchingLeft(objectItem) || this.IsTouchingRight(objectItem))
                {
                    this.Velocity.X = - this.Force.X/this.Weight;
                        
                }            
            }
        }

        //Apartat de les animacions
        protected virtual void SetAnimations()
        {

            if (isHit)
            {
                float angle = VectorOps.Vector2ToDeg(Direction);
                //Animació de ser colpejat per la salt
                if (angle < 315 && angle > 225)
                    _animationManager.Play(_animations["Slug up hit"]);
                else if (angle >= 315 || angle < 45)
                    _animationManager.Play(_animations["Slug right hit"]);
                else if (angle <= 225 && angle > 135)
                    _animationManager.Play(_animations["Slug left hit"]);
                else
                    _animationManager.Play(_animations["Slug down hit"]);
            }
            else
            {
                // Detecció del angle de dispar amb la corresponent animació (probablement s'haurà de fer de forma més eficient) 
                // Angle entre animacions: 18 graus || pi/10 radiants -- Desfasament: 9 graus || pi/20 radiant
                
                float angle = VectorOps.Vector2ToDeg(Direction);
                if ((angle <= 9 && angle >= 0) || (angle <= 360 && angle > 351))
                    _animationManager.Play(_animations["Slug right0"]);
                else if (angle <= 27 && angle > 9)
                    _animationManager.Play(_animations["Slug right-22_5"]);
                else if (angle <= 45 && angle > 27)
                    _animationManager.Play(_animations["Slug right-45"]);
                else if (angle <= 63 && angle > 45)
                    _animationManager.Play(_animations["Slug down45"]);
                else if (angle <= 81 && angle > 63)
                    _animationManager.Play(_animations["Slug down22_5"]);
                else if (angle <= 99 && angle > 81)
                    _animationManager.Play(_animations["Slug down0"]);
                else if (angle <= 117 && angle > 99)
                    _animationManager.Play(_animations["Slug down-22_5"]);
                else if (angle <= 135 && angle > 117)
                    _animationManager.Play(_animations["Slug down-45"]);
                else if (angle <= 153 && angle > 135)
                    _animationManager.Play(_animations["Slug left45"]);
                else if (angle <= 171 && angle > 153)
                    _animationManager.Play(_animations["Slug left22_5"]);
                else if (angle <= 189 && angle > 171)
                    _animationManager.Play(_animations["Slug left0"]);
                else if (angle <= 207 && angle > 189)
                    _animationManager.Play(_animations["Slug left-22_5"]);
                else if (angle <= 225 && angle > 207)
                    _animationManager.Play(_animations["Slug left-45"]);
                else if (angle <= 243 && angle > 225)
                    _animationManager.Play(_animations["Slug up45"]);
                else if (angle <= 261 && angle > 243)
                    _animationManager.Play(_animations["Slug up22_5"]);
                else if (angle <= 279 && angle > 261)
                    _animationManager.Play(_animations["Slug up0"]);
                else if (angle <= 297 && angle > 279)
                    _animationManager.Play(_animations["Slug up-22_5"]);
                else if (angle <= 315 && angle > 297)
                    _animationManager.Play(_animations["Slug up-45"]);
                else if (angle <= 333 && angle > 315)
                    _animationManager.Play(_animations["Slug right45"]);
                else if (angle <= 351 && angle > 333)
                    _animationManager.Play(_animations["Slug right22_5"]);
                else
                    _animationManager.Play(_animations["Slug down0"]);
            }
        }

        public void NotifyHit(Vector2 hitDirection, int shooterID, float damage, float hitImpulse)
        {
            this.isHit = true;
            this.Health -= 1;
            this.hitDirection = hitDirection;
            this.hitImpulse = hitImpulse;
        }

    }
}