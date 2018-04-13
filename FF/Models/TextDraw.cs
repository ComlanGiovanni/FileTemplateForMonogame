using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Models
{
    public class TextDraw
    {
        public int Score1;
        //can draw the velocity the position too
        public SpriteFont _font;

        public Vector2 positionS1 = new Vector2(800, 10);
        //public Vector2 positionS1 = new Vector2((int)(MiddlePoint.X - textSize.X), (int)(MiddlePoint.Y - textSize.Y));

        public TextDraw(SpriteFont font)
        {
            _font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, Score1.ToString(), positionS1, Color.Red);
        }
    }
}
