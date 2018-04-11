using FF.Managers;
using FF.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace FF.StaticAnimatedSprite
{
    public class StaticAnimatedSprite
    {
        public AnimationManager _animationManger;

        protected List<Animation> _animations;

        public Texture2D _texture;

        public Texture2D _spriteTexture;

        public Vector2 _position;

        public float FrameSpeed;

        public float Speed;

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
        
        public StaticAnimatedSprite(List<Animation> animations)
        {
            _animations = animations;
            _animationManger = new AnimationManager(_animations.First());
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _animationManger.Draw(spriteBatch);
        }

        public virtual void Update(GameTime gameTime, List<StaticAnimatedSprite> sprites)
        {
            SetAnimations();

            _animationManger.Update(gameTime);
        }

        protected virtual void SetAnimations()
        {
            _animationManger.Play(_animations.First());
        }
    }
}
