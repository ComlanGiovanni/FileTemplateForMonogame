﻿using System.Collections.Generic;
using System.Linq;
using FF.Managers;
using FF.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FF.Sprites
{
    public class MmtAnimatedSprite
    {
        public AnimationManager _animationManger;

        protected List<Animation> _animations;

        //public Texture2D _texture;

        public Texture2D _spriteTexture;

        public Vector2 _position;

        public float FrameSpeed;

        public float Speed;

        public Vector2 Velocity;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _animations.First().FrameWidth, _animations.First().FrameHeight);
            }
        }

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
            Move();
            SetAnimations();

            _animationManger.Update(gameTime);
        }

        protected virtual void SetAnimations()
        {
            _animationManger.Play(_animations.First());
        }
        
        private void Move()
        {
            Velocity.Y = Speed;
            Velocity.X = Speed;

            Position += Velocity;

            if (Position.X <= 0)
                Velocity.X *= -1;
            else if ((Position.X + 128) >= 1000)
                Velocity.X *= -1;
            else if (Position.Y <= 0)
                Velocity.Y *= -1;
            else if ((Position.Y + 128) >= 1000)
                Velocity.Y *= -1;

            //Velocity = Models.Settings.THE_ORIGIN;

            //Position.Y <= 0 || || (Position.Y >= 1000 - 128)
            //Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Models.Settings.SCREEN_WIDTH - Rectangle.Width, Models.Settings.SCREEN_HEIGHT - Rectangle.Height));
            //Position += Velocity;
            /*
            //up
            Velocity.Y = -Speed;
            //Down
            Velocity.Y = Speed;
            //Left
            Velocity.X = -Speed;
            //Right
            Velocity.X = Speed;
            */
        }
        
    }
}
