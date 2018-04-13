using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Sprites
{
    public class MmtStaticSprite
    {
        public Texture2D _texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Color Colour = Color.Yellow;
        public float Speed;
        private float _timer = 0f;
        private Vector2? _startPosition = null;
        private float? _startSpeed;
        private bool _isPlaying;
        public int SpeedInIncrementSpan = 10;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public MmtStaticSprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(_texture, Position, Colour);
        }

        public void Update(GameTime gameTime, List<MmtStaticSprite> mmtSprite)//Vitrual can be ovveride
        {
            foreach (var sprite in mmtSprite)
            {
                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite))
                    this.Velocity.X = -this.Velocity.X;
                if (this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                    this.Velocity.X = -this.Velocity.X;
                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
                if (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
            }

            if (_startPosition == null)
            {
                _startPosition = Position;
                _startSpeed = Speed;

                Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                _isPlaying = true;

            if (!_isPlaying)
                return;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > SpeedInIncrementSpan)
            {
                Speed++;
                _timer = 0;
            }

            if (Position.Y <= 0 || (Position.Y + _texture.Height) >= Settings.SCREEN_HEIGHT)
            {
                Velocity.Y = -Velocity.Y;
            }

            if (Position.X <= 0)
            {
                Velocity.X = -Velocity.X;
            }

            if ((Position.X + _texture.Width) >= Settings.SCREEN_WIDTH)
            {
                Velocity.X = -Velocity.X;
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

        protected bool IsTouchingLeft(MmtStaticSprite sprite)
        {
            return Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
                   Rectangle.Left < sprite.Rectangle.Left &&
                   Rectangle.Bottom > sprite.Rectangle.Top &&
                   Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(MmtStaticSprite sprite)
        {
            return Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
                   Rectangle.Right > sprite.Rectangle.Right &&
                   Rectangle.Bottom > sprite.Rectangle.Top &&
                   Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(MmtStaticSprite sprite)
        {
            return Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
                   Rectangle.Top < sprite.Rectangle.Top &&
                   Rectangle.Right > sprite.Rectangle.Left &&
                   Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(MmtStaticSprite sprite)
        {
            return Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
                   Rectangle.Bottom > sprite.Rectangle.Bottom &&
                   Rectangle.Right > sprite.Rectangle.Left &&
                   Rectangle.Left < sprite.Rectangle.Right;
        }
    }
}
