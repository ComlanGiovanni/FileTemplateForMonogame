using FF.Managers;
using FF.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace FF.Sprites
{
    public class FourDirMmtAnimatedSprite
    {
        protected AnimationManager _animationManger;

        protected Dictionary<string, Animation> _animations;

        protected Vector2 _position;

        //protected Texture2D _texture;

        public Input Input;

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

        public float Speed;

        public Vector2 Velocity;

        public Rectangle Rectangle
        {
            get
            {
                //return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
                return new Rectangle((int)Position.X, (int)Position.Y, _animations.First().Value.FrameWidth, _animations.First().Value.FrameHeight);
                //_texture == null because we never initilized the texture

            }
        }

        public FourDirMmtAnimatedSprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManger = new AnimationManager(_animations.First().Value);
        }

        //For the Draw we need the  to draw
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _animationManger.Draw(spriteBatch);
        }

        public virtual void Update(GameTime gameTime, List<FourDirMmtAnimatedSprite> sprites)
        {
            Move();
            
           // SetAnimations();
            _animationManger.Update(gameTime);
        }

        //Method
        private void Move()
        {
            //no Diagonal Mouv
            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                Velocity.Y = -Speed;
                _animationManger.Play(_animations["WalkUp"]);
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                Velocity.Y = Speed;
                _animationManger.Play(_animations["WalkDown"]);
            }else if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Velocity.X = -Speed;
                _animationManger.Play(_animations["WalkLeft"]);
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Velocity.X = Speed;
                _animationManger.Play(_animations["WalkRight"]);
            }
            else
                _animationManger.Stop();
            //_animationManger.Play(_animations["Just_Iddle"]); //Iddle for 4 direction with a previous keys

            Position += Velocity;
            Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Models.Settings.SCREEN_WIDTH - Rectangle.Width, Models.Settings.SCREEN_HEIGHT - Rectangle.Height));
            Velocity = Models.Settings.THE_ORIGIN;
        }
    }
}
