using FF.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FF.Sprites;
using System.Collections.Generic;
using System;

namespace FF
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<StaticSprite> _staticSprites;
        private List<TwoDirMmtStaticSprite> _twoDirSprites;
        private List<FourDirMmtStaticSprite> _fourDirSprite;
        private List<StaticAnimatedSprite.StaticAnimatedSprite> _staticAnimSprites; //list of list ?
        private List<FourDirMmtAnimatedSprite> _fourDirAnmSprite;
        private List<TwoDirMmtAnimSprite> _twoDirAnimSprite;
        private List<MmtAnimatedSprite> _mmtAnimatedSprite;
        private List<MmtStaticSprite> _mmtStaticSprite;

        public static Random Random;
        public TextDraw _score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = Models.Settings.SCREEN_HEIGHT,
                PreferredBackBufferWidth = Models.Settings.SCREEN_WIDTH
            };
            graphics.ApplyChanges();

            this.IsMouseVisible = Models.Settings.IS_MOUSE_VISIBLE;
            this.Window.IsBorderless = Models.Settings.IS_BORDERLESS;

            this.Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - ((graphics.PreferredBackBufferHeight + 40) / 2));//40 :^)

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Random = new Random();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _score = new TextDraw(Content.Load<SpriteFont>("Fonts/ComisSans48"));//C:/Windows/Fonts,

            var flameSprite = new List<Animation>(){{new Animation(Content.Load<Texture2D>("SpriteSheet/flame_sprite"), 6, 0.1f) },};
            var ghost = new List<Animation>(){{new Animation(Content.Load<Texture2D>("SpriteSheet/ghost"), 12, 0.1f) },};
            var pixelGhost = new List<Animation>(){{new Animation(Content.Load<Texture2D>("SpriteSheet/pixelGhost"), 2, 1f) },};
            var sonic = new List<Animation>(){{new Animation(Content.Load<Texture2D>("SpriteSheet/sonic"), 4, 0.1f) },};

            var cBall = new List<Animation>(){{new Animation(Content.Load<Texture2D>("SpriteSheet/colored-balls"), 5, 0.1f) },};

            var playerTexture = (Content.Load<Texture2D>("Sprite/bl"));
            var playerpng = (Content.Load<Texture2D>("Sprite/png"));

            var ballTexture = Content.Load<Texture2D>("Sprite/Ball");

            var flappyBirdGround = (Content.Load<Texture2D>("Sprite/FlappyBirdGround"));
            var backGround = (Content.Load<Texture2D>("Sprite/background1"));

            var animations = new Dictionary<string, Animation>()
            {
                {"WalkUp", new Animation(Content.Load<Texture2D>("SpriteSheet/link/WalkingUp"), 3, 0.1f)},
                {"WalkDown", new Animation(Content.Load<Texture2D>("SpriteSheet/link/WalkingDown"), 3, 0.1f)},
                {"WalkLeft", new Animation(Content.Load<Texture2D>("SpriteSheet/link/WalkingLeft"), 3, 0.1f)},
                {"WalkRight", new Animation(Content.Load<Texture2D>("SpriteSheet/link/WalkingRight"), 3, 0.1f)},
                //{"Just_Iddle", new Animation(Content.Load<Texture2D>("SpriteSheet/link/Just_Iddle"), 3, 10f)},
            };

            var animations2Dir = new Dictionary<string, Animation>()
            {
                {"WalkRight", new Animation(Content.Load<Texture2D>("SpriteSheet/other/WalkingRight"), 3, 0.1f)},
                {"WalkLeft", new Animation(Content.Load<Texture2D>("SpriteSheet/other/WalkingLeft"), 3, 0.1f)},
            };

            _fourDirSprite = new List<FourDirMmtStaticSprite>()
            {
                new FourDirMmtStaticSprite(playerpng)
                {
                    Input = new Input()
                    {
                        Left = Keys.Q,
                        Right = Keys.D,
                        Up = Keys.Z,
                        Down = Keys.S,
                    },

                    Position = new Vector2(0, 0),
                    Colour = Models.Settings.DONT_AFFECT_COLOR_SPRITE,
                    Speed = 10f,
                }
            };

            _staticAnimSprites = new List<StaticAnimatedSprite.StaticAnimatedSprite>()
            {
                new StaticAnimatedSprite.StaticAnimatedSprite(flameSprite){Position = new Vector2(0, 0),},
                new StaticAnimatedSprite.StaticAnimatedSprite(ghost){Position = new Vector2(500, 100),},
                new StaticAnimatedSprite.StaticAnimatedSprite(sonic){Position = new Vector2(500, 300),},
                new StaticAnimatedSprite.StaticAnimatedSprite(pixelGhost){Position = new Vector2(700, 100),},

            };

            _mmtAnimatedSprite = new List<MmtAnimatedSprite>()
            {
                new MmtAnimatedSprite(cBall){Position = new Vector2(650, 800), Speed = 10f, Score = _score},
                new MmtAnimatedSprite(cBall){Position = new Vector2(49, 140), Speed = 5f, Score = _score},
                new MmtAnimatedSprite(cBall){Position = new Vector2(139, 512), Speed = 2f, Score = _score},
            };

            _staticSprites = new List<StaticSprite>()
            {
                new StaticSprite(backGround)
                 {
                     Position = new Vector2(0,500),
                 },

                new StaticSprite(flappyBirdGround)
                 {
                     Position = new Vector2(0,900),
                 },
            };
            
            _twoDirSprites = new List<TwoDirMmtStaticSprite>()
            {
                new TwoDirMmtStaticSprite(playerTexture)
               {
                    Input = new Input()
                   {
                        Left = Keys.Left,
                       Right = Keys.Right,
                    },
                    Position = new Vector2(0, 400),
                    Colour = Color.Blue,
                    Speed = 10f,
                },
                new TwoDirMmtStaticSprite(playerTexture)
                {
                   Input = new Input()
                    {
                       Left = Keys.NumPad4,
                        Right = Keys.NumPad6,
                    },
                    Position = new Vector2(0, 0),
                    Colour = Color.Red,
                    Speed = 10f,
                },
            };

            _fourDirAnmSprite = new List<FourDirMmtAnimatedSprite>()
            {
                //Second Sprite, u can just create a new color prioprietes instead of create aanother anitmation
                new FourDirMmtAnimatedSprite(animations)
                {
                    Position = new Vector2(900, 0),
                    Speed = 5f,

                    Input = new Input()
                    {
                        Up = Keys.T,
                        Down = Keys.G,
                        Left = Keys.F,
                        Right = Keys.H,
                    },
                }
            };

            _twoDirAnimSprite = new List<TwoDirMmtAnimSprite>()
            {
                new TwoDirMmtAnimSprite(animations2Dir)
                {
                    Position = new Vector2(0, 0),
                    Speed = 4f,

                    Input = new Input()
                    {
                        Left = Keys.C,
                        Right = Keys.V,
                    },
                }
            };

            _mmtStaticSprite = new List<MmtStaticSprite>
            {
                new MmtStaticSprite(ballTexture)
                {
                    Position = new Vector2(250,800),
                    Speed = 10f,
                },

                new MmtStaticSprite(ballTexture)
                {
                    Position = new Vector2(500,800),
                    Speed = 5f,
                },

                new MmtStaticSprite(ballTexture)
                {
                    Position = new Vector2(750,800),
                    Speed = 9f,
                },

                new MmtStaticSprite(ballTexture)
                {
                    Position = new Vector2(250,250),
                    Speed = 9f,
                    Colour = Color.Red,
                },

                new MmtStaticSprite(ballTexture)
                {
                    Position = new Vector2(500,250),
                    Speed = 9f,
                    Colour = Color.Red,
                },

                new MmtStaticSprite(ballTexture)
                {
                    Position = new Vector2(750,250),
                    Speed = 9f,
                    Colour = Color.Red,
                },

                new MmtStaticSprite(ballTexture)
                {
                    Position = new Vector2(250,500),
                    Speed = 9f,
                    Colour = Color.Pink,
                },

                new MmtStaticSprite(ballTexture)
                {
                    Position = new Vector2(500,500),
                    Speed = 9f,
                    Colour = Color.Pink,
                },

                new MmtStaticSprite(ballTexture)
                {
                    Position = new Vector2(750,500),
                    Speed = 9f,
                    Colour = Color.Pink,
                },
            };
        }

        protected override void UnloadContent()
        {
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            foreach (var sprite in _fourDirAnmSprite)
                sprite.Update(gameTime, _fourDirAnmSprite);

            foreach (var sprite in _fourDirSprite)
                sprite.Update(gameTime);

            foreach (var sprite in _staticAnimSprites)
                sprite.Update(gameTime, _staticAnimSprites);
             
            foreach (var sprite in _twoDirSprites)
                sprite.Update(gameTime);

            foreach (var sprite in _twoDirAnimSprite)
                sprite.Update(gameTime, _twoDirAnimSprite);

            foreach (var sprite in _twoDirAnimSprite)
                sprite.Update(gameTime, _twoDirAnimSprite);
            
            foreach (var sprite in _mmtAnimatedSprite)
                sprite.Update(gameTime, _mmtAnimatedSprite);
                
            foreach (var sprite in _mmtStaticSprite)
                sprite.Update(gameTime, _mmtStaticSprite);


            //No update for static sprite

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            
            foreach (var sprite in _staticAnimSprites)
                sprite.Draw(spriteBatch);

            foreach (var sprite in _twoDirSprites)
                sprite.Draw(spriteBatch);

            foreach (var sprite in _staticSprites)
                sprite.Draw(spriteBatch);

            foreach (var sprite in _fourDirSprite)
                sprite.Draw(spriteBatch);

            foreach (var sprite in _fourDirAnmSprite)
                sprite.Draw(spriteBatch);

            foreach (var sprite in _twoDirAnimSprite)
                sprite.Draw(spriteBatch);
            
            foreach (var sprite in _mmtAnimatedSprite)
                sprite.Draw(spriteBatch);
                
            foreach (var sprite in _mmtStaticSprite)
                sprite.Draw(spriteBatch);

            _score.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
