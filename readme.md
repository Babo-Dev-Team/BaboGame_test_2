# Projecte de BaboGame

Aquí s'explicarà el funcionament de cada component del projecte per tal de que els grup s'aclari més en el treball

## Funcionament del Monogame

El monogame es basa en un programa de disseny de videojocs en 3D el qual pot simular el 2D a través de l'ús d'un tipus de perspectiva en la càmera. El codi principal (Game1.cs) es compon de una funció "Initialize" que pot carregar tot allò que no tingui a veure amb el gràfics a l'inici, "LoadContent" que pot carregar tot alló que sí que te veure en els gràfics a l'inici, "UnloadContent" que elimina de la càrrega algún contingut d'un procés anterior sobrant i que només es pot fer un cop en el joc, "Update" que fa els canvis en cada fotograma i "Draw" que s'encarrega de dibuixar les imatges en pantalla.

### Funcions utilitzades

De moment tan sols hem utilitzat el "Initialize", el "LoadContent", el "Update" i el "Draw". S'ha d'anar en compte entre utilitzar el initialize i el LoadContent ja que si el initialize carrega alguna cosa que no fa ús de les imatges però que és necessari per un procés del LoadContent que si les necessita, el programa fallarà. Tot allò relacionat en carregar el contingut del joc a l'inici que tingui relació amb els sprites s'ha de posar al LoadContent, no es pot posar coses del mateix procés en els 2 llocs a la vegada.

El Update sempre ha de tenir el base.Update(gameTime) al final degut que la funció Update és un "protected override void", una funció que clona el codi de un "Update" ja existent. És a dir, la clase Game1.cs és una còpia a un Game ja existent que ja té el codi que fa funcionar l'actualització de contingut en Update i el dibuixat en Draw. El base.Update(gameTime) crida el contingut de l'altre codi per tal de que el Game1 faci els mateixos procesos que en Update que el Game que fa de plantilla.

En el cas del Draw, funciona de forma similar que el Update pero necessita unes línies de codi extres abans. El GraphicsDevice.Clear(Color) buida la pantalla i la pinta tota d'un color, es posa a l'inici del codi Draw. A part, tenim les funcions spriteBatch.Begin() i spriteBatch.End() que inicien i acaben el dibuixat de les imatges respectivament. Tot el codi del Draw s'ha de posar entre mig d'aquestes dues línies. 

Si ens fixem, el spriteBatch.Begin() té dos paràmetres: el SpriteSortMode.FrontToBack i el SamplerState.LinearWrap. El primer s'encarrega de dibuixar les imatges en un ordre determinat perquè es dibuixen a sobre o a sota d'altres, això es fa en el cas de que dues imatges tinguin el mateix valor de Layer (es comentarà més enrere). El segon seria un equivalent al tipus de dibuixat o el mode de mostreig que vols que faci el Draw, els acabats en Wrap són més "permisius" i sempre mostregen la imatge en pantalla (si no té el mode, els sprites poden començar a parpadejar).

## Funcionament del projecte

El nostre projecte està bastant esquematitzat per poder reutilitzar les funcions més facilment i no haver d'entrar en excés a les classes per tal de modificar algún valor. Per tant, el projecte està organitzat principalment per Sprites, Managers i Engines.

Els Sprites són les classes que faran d'imatges per pantalla, solen ser projectils, personatges, objectes per pantalla, blocs, babes, etc. Alguns d'aquests estan integrats al seu propi Engine i tots són clons de un Sprite base.

Els Managers són els encarregats de fer funcionar mecàniques no físiques en el projecte com pot ser la tria d'un projectil, la vida del personatge, el text en pantalla o les animacions.

Finalment, els Emgines s'encarreguen de determinar el comportament físic dels items que es veuen en pantalla com pot ser les col·lisions tan en projectils com en personatges o el seu propi moviment.

### Sprites

Tal com s'ha esmentat, existeix una clase que fa de sprite base. Aquest és la clase Sprite que té el comportament IClonable (copiable) i una funció perquè l'objecte es pugui clonar "public object Clone()".

#### Funcions del Sprite general

Les funcions, declaracions o procediments que té són les següents:

public Sprite (Texture2D texture) - la declaració se li ha d'entrar un objecte textura que és una imatge que vé enllaçada a la carpeta "Content" o contingut. Tan sols serveix per generar el sprite com una imatge estàtica. En els Sprites clons han de cridar la funció posant un ": base(texture)" a la declaració del Sprite clon per tal de que funcioni com una imatge estàtica.
 
public Sprite (Dictionary<string, Animation> animations) - la declaració se li ha d'entrar un objecte diccionari que el primer element sigui un string i el segon una clase Animations (creat per nosaltres). Es crida amb un ": base(animations)" i bàsicament recull una taula de Animacions amb un nom concret per cada animació.

public virtual void Draw(SpriteBatch spriteBatch) - la funció draw és el que dibuixa les imatges per pantalla, en el sprite el que fa es comprovar si s'utilitza una textura o una animació i fer la funció de dibuixat. Tots els sprites criden aquesta funció a la funció de Game1.Draw(). Es basa simplement en fer ús del procediment:
"spriteBatch.Draw(Texture2D textura_a_dibuixar, Vector2 posició_del_sprite_en_pantalla, Rectangle dimensions_del_fotograma, Color color_de_la_imatge(blanc(Color.White) és el que et posa la imatge per defecte), float angle_de_rotació (normalment va en radiants), Vector2 centre_de_la_imatge (per defecte, la cantonada superior esquerra és la posició del objecte. Aquí pots canviar la posició de la imatge relativa a aquella posició), float escalat_de_la_imatge, SpriteEffects efectes_extres_de_la_imatge (et permet voltejar la imatge), float Layer(capa on es situa la imatge en pintar-la en pantalla));"

    spriteBatch.Draw(_texture, Position, null, _color, _rotation, Origin, Scale, Effect, Layer);

El AnimationManager crida la mateixa funció de dibuixat però incloient-li el Rectangle dels fotogrames.

public Rectangle Rectangle - aquest procediment crea un objecte amb les dimensions de les col·lisions del personatge. L'objecte Rectangle té els paràmetres següents:
Rectangle(int Cantonada_Esquerra_del_rectangle, int Cantonada_Superior_del_rectangle, int Dimensions_del_eix_X, int Dimensions_del_eix_Y)

    new Rectangle((int)(Position.X - Origin.X * Scale), (int)(Position.Y - Origin.Y * Scale), (int) (_texture.Width * Scale * HitBoxScale), (int)(_texture.Height * Scale * HitBoxScale));

#### Regió de col·lisió del Sprite general

A la clase Sprite.cs també hi ha una regió on tracta les funcions de col·lisió anomenat Collision. Són procediments booleanes on detecta si un objecte està tocant una cantonada del rectangle creat anteriorment.

Les condicions per tots els casos és la següent:
1 - Si segueixes anant cap a la mateixa direcció, la teva cantonada estarà més desplaçada cap aquell sentit que la cantonada oposada d'un altre objecte?
2 - La cantonada oposada de l'altre objecte està més desplaçada cap aquella direcció que la teva cantonada oposada.
Aquests tracten del mateix eix i es basen en veure si una cantonada està realment entre mig d'un altre objecte.
3 i 4 - En l'altre eix, els objectes s'arribarien a creuar per com estan posicionats? O pel contrari, en aquest eix estàn massa llunys i els objectes pasen pel costat.

Aquestes condicions es pregunten per totes les cantonades existents del rectangle per comprovar si hi hauria col·lisió. Són els següents: IsTouchingLeft, IsTouchingRight, IsTouchingTop i IsTouchingBottom.

#### Variables utilitzades en el Sprite general

        //Variables globals d'un "Sprite"
        public float Scale = 1f;
        public float Layer = 0f;
        public SpriteEffects Effect = SpriteEffects.None;
        public Color _color = Color.White;
        public bool Visible = true;

        //Variables entorn a la detecció de tecles del teclat i el ratolí; i les seves entrades
        protected KeyboardState currentKey;
        protected KeyboardState previousKey;
        protected MouseState currentMouseState;
        protected MouseState previousMouseState;
        //public InputKeys Input;

        //Variables entorn a les animacions
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Texture2D _texture;
        protected Vector2 _position;

        //Variables vector per la posició, el centre i la direcció del personatge
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }
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
        public int IDcharacter = 0;
        public bool IsSaltShoot = false;

        //Variables relacionades amb la vida
        public bool IsHealth = false;
        public float heart_health = 0f;
        public int hearthPosition;

        //Variables relacionades amb el menú de la salt
        public bool IsMenuSalt = false;
        public int MenuPos = 0;
        
#### Ús en el Game1

L'aplicació típica dels sprites en el codi principal sol ser la següent (pot canviar en un futur):
1 - Es crea una llista de Sprites que coninguin un tipus determinat de Sprite com a variable global

        private List<Character> characterSprites;
        private List<Projectile> projectileSprites;   
        private List<Sprite> overlaySprites;           
        private List<Slime> slimeSprites;                   
        private List<ScenarioObjects> scenarioSprites;
        
2 - Es creen o declaren les textures o els diccionaris d'Animacions en el LoadContent
  
        var sightTexture = Content.Load<Texture2D>("Sight/Sight_off");
        Dictionary<string, Animation> sightAnimation = new Dictionary<string, Animation>()
            {
                {"ON", new Animation(Content.Load<Texture2D>("Sight/Sight_on"), 1) },
                {"OFF", new Animation(Content.Load<Texture2D>("Sight/Sight_off"), 1) },
            };
            
3 - Es crea els items en la llista (en Update si és un objecte que s'ha de crear en un moment precís, un projectil per exemple; en LoadContent si és un objecte que ha d'estar desde el primer moment, la mira, els llimacs, etc).

      // La mira necessita que li passem inputManager per obtenir la posició del ratolí
      overlaySprites = new List<Sprite>()
      {
          new SightWeapon(sightAnimation, inputManager)
          {
              Position = new Vector2(100,100),
              Scale = 0.2f,
              SolidObject = false,
              Layer = 1f,
          },
      };
      
4 - Se li aplica els canvis que li pertoqui aquell sprite en específic en Update cridant una funció de Update que ja tingui el objecte.

    foreach (var overlay in this.overlaySprites)
    {
        overlay.Update(gameTime, overlaySprites);
    }
    
El bucle foreach agafa un objecte de la llista en cada tornada i el relaciona amb una variable. Després fa les funcions que li pertoquin a l'objecte de la llista.

5 - Es dibuixa el Sprite amb la funció Draw, si no es crida la funció el sprite no es veurà en pantalla però pot seguir estant allà. Si no crides la funció, el objecte serà invisible.

    foreach (var overlay in overlaySprites)
    {
        if(overlay.Visible)
            overlay.Draw(spriteBatch);
    }

S'ha aprofitat aquesta condició per fer el menú dels projectils que està contingut a la llista d'objectes "overlay" (sobre la pantalla). Aquest no té un sprite definit com a propi així que és una llista de Sprites original, per això les variables dels items que el conté solen estar en la clase Sprite.

### Managers
### Engines
