using FF.Managers;
using FF.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Sprites
{
    public class MmtAnimatedSprite
    {
        public AnimationManager _animationManger;
        protected List<Animation> _animations;

        public Vector2 _position;
        private Vector2? _startPosition = null;
        public Vector2 Velocity;

        public float FrameSpeed;
        public float Speed;
        private float _timer = 0f;
        private float? _startSpeed;

        private bool _isPlaying;
        public int SpeedInIncrementSpan = 10;

        public Color Colour = Settings.DONT_AFFECT_COLOR_SPRITE;
        public TextDraw Score;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManger != null)
                    _animationManger.Position = _position;
                }
        }
        
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _animations.First().FrameWidth, _animations.First().FrameHeight);
            }
        }

        public MmtAnimatedSprite(List<Animation> animations)
        {
            _animations = animations;
            _animationManger = new AnimationManager(_animations.First());
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _animationManger.Draw(spriteBatch);
        }

        public virtual void Update(GameTime gameTime, List<MmtAnimatedSprite> sprites)
        {
            SetAnimations();
            _animationManger.Update(gameTime);
            if (_startPosition == null)
            {
                _startPosition = Position;
                _startSpeed = Speed;

                Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                _isPlaying = true;

            if (!_isPlaying)
                return;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > SpeedInIncrementSpan)
            {
                Speed++;
                _timer = 0;
            }

            if (Position.Y <= 0 || Position.Y + _animations.First().FrameHeight >= Settings.SCREEN_HEIGHT)
            {
                Velocity.Y = -Velocity.Y;
                Score.Score1++;
            }

            if (Position.X <= 0)
            {
                Velocity.X = -Velocity.X;
                Score.Score1++;
            }

            if (Position.X + _animations.First().FrameWidth >= Settings.SCREEN_WIDTH)
            {
                Velocity.X = -Velocity.X;
                Score.Score1++;
            }

            Position += Velocity * Speed;
        }

        public void Restart()
        {
            var direction = Game1.Random.Next(0, 4);

            switch (direction)
            {
                case 0:
                    Velocity = new Vector2(1, 1);
                    break;
                case 1:
                    Velocity = new Vector2(1, -1);
                    break;
                case 2:
                    Velocity = new Vector2(-1, -1);
                    break;
                case 3:
                    Velocity = new Vector2(-1, 1);
                    break;
            }

            Position = (Vector2)_startPosition;
            Speed = (float)_startSpeed;

            _timer = 0;
            _isPlaying = false;
        }

        protected virtual void SetAnimations()
        {
            _animationManger.Play(_animations.First());
        }
    }
}
