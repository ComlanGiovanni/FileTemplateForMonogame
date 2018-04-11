using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FF.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FF.Sprites
{
    public class TwoDirMmtAnimSprite : FourDirMmtAnimatedSprite
    {

        //iddle smae sprite but a red zelda
        public TwoDirMmtAnimSprite(Dictionary<string, Animation> animations) : base(animations)
        {
        }

        public new Rectangle Rectangle
        {
            get
            {
                //return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
                return new Rectangle((int)Position.X, (int)Position.Y, 40, 60);
                //_texture == null because we never initilized the texture

            }
        }

        public void Update(GameTime gameTime, List<TwoDirMmtAnimSprite> sprites)
        {
            Move();
            _animationManger.Update(gameTime);
        }

        //Method
        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Velocity.X = Speed;
                _animationManger.Play(_animations["WalkRight"]);
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Velocity.X = -Speed;
                _animationManger.Play(_animations["WalkLeft"]);
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
