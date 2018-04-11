using FF.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FF.Sprites
{
    public class TwoDirMmtStaticSprite
    {
        protected Texture2D _texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Color Colour;
        public float Speed;
        public Input Input;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public TwoDirMmtStaticSprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(_texture, Position, Colour);
        }

        public void Update(GameTime gameTime)
        {
            Move();
        }
        
        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;

            Position += Velocity;
            Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Models.Settings.SCREEN_WIDTH - Rectangle.Width, Models.Settings.SCREEN_HEIGHT - Rectangle.Height));
            Velocity = Models.Settings.THE_ORIGIN;
        }
    }
}
