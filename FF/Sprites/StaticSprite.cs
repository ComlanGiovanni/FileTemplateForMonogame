using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Sprites
{
    public class StaticSprite
    {
        private Texture2D _texture;

        public Vector2 Position;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        //ctor tab

        public StaticSprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(_texture, Rectangle, Settings.DONT_AFFECT_COLOR_SPRITE);
        }

        public virtual void Update(GameTime gameTime)
        {
            //nothing because is static
        }
    }
}
