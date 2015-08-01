using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessiahSandbox
{
    class CelestialBody
    {
        protected const double GRAVITATIONAL_CONSTANT = 6.674 * 0.00000000001;
        protected const int METERS_PER_PIXELS = 11811;
        protected const float GAME_TIME_SPEED = 1f;

        protected double _mass;
        protected double _gravity;
        protected Vector2 _position;
        protected double _eccentricity;
        protected double _semiMajorAxis;
        protected double _semiMinorAxis;
        protected double _apoapsis;
        protected double _periapsis;
        protected double _orbitalPeriod;
        protected double _ellipseArea;

        protected Vector2 _origin;
        protected Vector2 _focus1;
        protected Vector2 _focus2;
        protected Vector2 _center;
        protected Texture2D _texture;

        protected float _angle;
        protected double _fociDist;
        protected double _fociAngle;

        protected CelestialBody _centralBody;
        protected UnitConverter uc;
        protected string _info;

        protected float _size;
        protected Color _color;

        private MouseState _mouse;
        private float _circleTimer;

        protected List<OrbitTest> _test = new List<OrbitTest>();


        public double Mass
        {
            get { return this._mass; }
        }
        public Vector2 Position
        {
            get { return this._position; }
        }

        public double Gravity
        {
            get { return this._gravity; }
        }

        public void Update(GameTime _gameTime)
        {
            _mouse = Mouse.GetState();

            if (_centralBody != null)
            {
                _focus1 = _centralBody.Position;
                _focus2 = new Vector2(_centralBody.Position.X - ((float)(_apoapsis - _periapsis) / METERS_PER_PIXELS), _centralBody.Position.Y);
                _center = new Vector2((_focus1.X + _focus2.X) / 2, (_focus1.Y + _focus2.Y) / 2);

                double r = _semiMajorAxis * (1 - _eccentricity * _eccentricity) /
                                            (1 - _eccentricity * Math.Cos(_angle));
                _angle += MathHelper.ToRadians(GAME_TIME_SPEED / (float)_orbitalPeriod);

                double ct = Math.Cos(_angle);
                double st = Math.Sin(_angle);
                double cp = Math.Cos(_fociAngle);
                double sp = Math.Sin(_fociAngle);

                double x1 = r * ct - _fociDist / 2;
                double y1 = r * st;
                double x = x1 * cp - y1 * sp;
                double y = x1 * sp + y1 * cp;

                _position.X = _focus2.X + (float)x / METERS_PER_PIXELS;
                _position.Y = _focus1.Y + (float)y / METERS_PER_PIXELS;
                

                //Visual for orbit and speed.
                /*_circleTimer += (float)_gameTime.ElapsedGameTime.TotalSeconds;

                if (_circleTimer > 1)
                {
                    _circleTimer = 0;
                    _test.Add(new TestCircle(_position, _texture));
                }*/
            }
            

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (OrbitTest circle in _test)
            {
                circle.Draw(_spriteBatch);
            }
            _spriteBatch.Draw(_texture, _position, null, _color, 0, _origin, _size, SpriteEffects.None, 0.5f);

            //WIP
            /*if (_mouse.X > _position.X - _texture.Width * _size &&
                _mouse.Y > _position.Y - _texture.Height * _size &&
                _mouse.X < _position.X + _texture.Width * _size &&
                _mouse.Y < _position.Y + _texture.Height * _size)
            {
                _spriteBatch.DrawString(Globals.font1, _info, _position + new Vector2(20, -Globals.font1.MeasureString(_info).Y / 2), Color.Black);
            }*/
            
        }

        protected virtual void ShowInfo(SpriteBatch _spriteBatch)
        {

        }

    }
}
