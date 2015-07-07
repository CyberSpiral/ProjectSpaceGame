using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessiahSandbox
{
    class Satellite
    {
        private const double GRAVITATIONAL_CONSTANT = 6.674 * 0.00000000001;//Math.Pow(10, -11);
        private const int METERS_PER_PIXELS = 320000000;
        private const float GAME_TIME_SPEED = 0.25f;

        private string _name;
        private Color _color;

        private double _mass;
        private double _radius;
        private double _eccentricity;
        private double _semiMajorAxis;
        private double _semiMinorAxis;
        private double _periapsis;
        private double _apoapsis;
        private double _orbitalPeriod;
        private double _orbitalVelocity;

        private Vector2 _position;
        private Vector2 _origin;
        private Vector2 _focus1;
        private Vector2 _focus2;
        private Vector2 _center;

        private float _angle;
        private double _fociDist;
        private double _fociAngle;
        

        private Texture2D _texture;

        Planet _planet;
        UnitConverter uc;

        /// <summary>
        /// Creates a planet with realistic properties based on masss, radius, eccentricity and its semi-major axis and star.
        /// </summary>
        /// <param name="name">
        /// Name of the planet.
        /// </param>
        /// <param name="mass">
        /// Mass of the planet in kilograms.
        /// </param>
        /// <param name="radius">
        /// Radius of the planet in meters.
        /// </param>
        /// <param name="eccentricity">
        /// Eccentricity of the planet's orbit.
        /// </param>
        /// <param name="semiMajorAxis">
        /// The semi-major axis of the planet's orbit in meters.
        /// </param>
        /// <param name="star"></param>
        public Satellite(string name, double mass, double radius, double eccentricity, double semiMajorAxis, Planet planet) {
            uc = new UnitConverter();

            _name = name;
            _mass = mass;
            _radius = radius;
            _eccentricity = eccentricity;
            _semiMajorAxis = semiMajorAxis;
            _planet = planet;

            _semiMinorAxis = _semiMajorAxis * Math.Sqrt(1 - Math.Pow(_eccentricity, 2));
            _periapsis = _semiMajorAxis * (1 - _eccentricity);
            _apoapsis = _semiMajorAxis * (1 + _eccentricity);
            _orbitalPeriod = Math.Sqrt(Math.Pow(_semiMajorAxis * uc.TO_AU, 3) / (_planet.Mass * uc.TO_SOLAR_MASS));
            _orbitalVelocity = Math.Sqrt(_planet.Mass * uc.TO_SOLAR_MASS / _semiMajorAxis) * 29.78;

            _origin = Vector2.Zero;

            _angle = 0;

            _focus1 = _planet.Position;
            _focus2 = new Vector2(_planet.Position.X - ((float)(_apoapsis - _periapsis) / METERS_PER_PIXELS), _planet.Position.Y); ;

            _fociDist = Vector2.Distance(_focus2, _focus1);
            _fociAngle = MathHelper.ToRadians(180) + Math.Atan2(_focus2.Y - _focus1.Y, _focus2.X - _focus1.X);
            _center = new Vector2((_focus1.X + _focus2.X) / 2, (_focus1.Y + _focus2.Y) / 2);

            _position.X = _focus1.X - (float)_fociDist + (float)(_center.X + _semiMajorAxis * 10 * Math.Cos(_fociAngle)) / METERS_PER_PIXELS;
            _position.Y = _focus1.Y + (float)(_center.Y + _semiMinorAxis * Math.Sin(_fociAngle)) / METERS_PER_PIXELS;

        }

        public void Update()
        {
            _focus1 = _planet.Position;
            double r = _semiMajorAxis * 10 * (1 - _eccentricity * _eccentricity) /
                                        (1 - _eccentricity * Math.Cos(_angle));
            _angle += MathHelper.ToRadians(GAME_TIME_SPEED / (float)_orbitalPeriod);

            double ct = Math.Cos(_angle);
            double st = Math.Sin(_angle);
            double cp = Math.Cos(_fociAngle);
            double sp = Math.Sin(_fociAngle);

            double x1 = r * ct - _fociDist / 2;
            double y1 = r * st;
            double x = _center.X + x1 * cp - y1 * sp;
            double y = _center.Y + x1 * sp + y1 * cp;

            _position.X = _focus1.X - (float)_fociDist + (float)x / METERS_PER_PIXELS;
            _position.Y = _focus1.Y + (float)y / METERS_PER_PIXELS;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, _position, null, Color.Black, 0, _origin, 0.5f, SpriteEffects.None, 0.5f);
            //_spriteBatch.Draw(_texture, _center, null, Color.White, 0, _origin, 1, SpriteEffects.None, 0.5f);
            //_spriteBatch.Draw(_texture, _focus2, null, Color.Red, 0, _origin, 1, SpriteEffects.None, 0.5f);

        }

        public Texture2D Texture
        {
            set
            {
                _texture = value;
                _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            }
        }
    }
}
