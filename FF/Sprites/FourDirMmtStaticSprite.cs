using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FF.Sprites
{
    public class FourDirMmtStaticSprite : TwoDirMmtStaticSprite
    {
        public FourDirMmtStaticSprite(Texture2D texture) : base(texture)
        {
        }
        
        public new void Update(GameTime gameTime)
        {
            Move();

            Velocity = Models.Settings.THE_ORIGIN;
        }

        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;
            //if u want diagonal mouvement
            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Velocity.Y = Speed;

            //if up and right is press a the sema time speed is *2 :( ?

            Position += Velocity;

            Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Models.Settings.SCREEN_WIDTH - Rectangle.Width, Models.Settings.SCREEN_HEIGHT - Rectangle.Height));
            /*   CAN BE
             * float XR = MathHelper.Clamp(Position.X, 0, Models.Settings.SCREEN_WIDTH - _texture.Width);//or Rectangle
             * float XY = MathHelper.Clamp(Position.Y, 0, Models.Settings.SCREEN_HEIGHT - _texture.Height);
             * Position = new Vector2(XR, XY);
             */
        }
    }
}
