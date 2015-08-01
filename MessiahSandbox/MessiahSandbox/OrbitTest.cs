using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessiahSandbox
{
    class OrbitTest
    {
        private Vector2 _position;
        private Texture2D _texture;
        private Vector2 _origin;

        public OrbitTest(Vector2 position, Texture2D texture)
        {
            _position = position;
            _texture = texture;
            _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public void Draw(SpriteBatch _spritebatch)
        {
            _spritebatch.Draw(_texture, _position, null, Color.Red, 0, _origin, 0.007f, SpriteEffects.None, 0.02f);
        }
    }
}
