using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS4U_Monogame_5
{


    public class Ghost
    {
        private List<Texture2D> _textures;
        private Vector2 _speed;
        private Rectangle _location;
        private int _textureIndex;
        private SpriteEffects _direction;

        private float _animationSpeed;
        private float _seconds;

        private double _speedMult;

        private Random _generator;

        private float _opacity;

        public Rectangle Rect
        {
            get { return _location; }
        }

        public Ghost(List<Texture2D> textures, Rectangle location)
        {
            _textures = textures;
            _textureIndex = 0;
            _speed = Vector2.Zero;
            _location = location;

            _animationSpeed = 0.2f;
            _seconds = 0;

            _speedMult = 0f;

            _generator = new Random();

            _opacity = 1f;
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            _speed = Vector2.Zero;

            _speedMult = _generator.NextDouble();

            int randSpeed;

            if (_speedMult < 0.2f)
            {
                randSpeed = 1;
            }
            else if (_speedMult < 0.6f)
            {
                randSpeed = 2;
            }
            else
                randSpeed = 3;

            if (mouseState.Position.X < _location.X)
            {
                _direction = SpriteEffects.FlipHorizontally;
                _speed.X = -randSpeed;
            }
            else if (mouseState.Position.X > _location.X)
            {
                _direction = SpriteEffects.None;
                _speed.X = randSpeed;
            }

            if (mouseState.Position.Y < _location.Y)
            {
                _speed.Y = -randSpeed;
            }
            else if (mouseState.Position.Y > _location.Y)
            {
                _speed.Y = randSpeed;
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                _speed = Vector2.Zero;
                _textureIndex = 0;
                _seconds = 0f;

                _opacity = 0.3f;

            }
            else if (_speed != Vector2.Zero) // Ghost is moving
            {
                _opacity = 1f;

                _seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_seconds > _animationSpeed)
                {
                    _seconds = 0;
                    _textureIndex++;
                    if (_textureIndex >= _textures.Count)
                        _textureIndex = 1;

                }
            }

            _location.Offset(_speed);
        }




        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures[_textureIndex], _location, null, Color.White * _opacity, 0f, Vector2.Zero, _direction, 0f);
        }

        public bool Contains(Point player)
        {
            return _location.Contains(player);
        }

        public bool Intersects(Rectangle player)
        {
            return _location.Intersects(player);

        }
    }

}
