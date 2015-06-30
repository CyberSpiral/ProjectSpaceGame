using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessiahSandbox
{
    class Planet
    {
        private const double GRAVITATIONAL_CONSTANT = 6.674 * 0.00000000001;//Math.Pow(10, -11);
        private const int METERS_PER_PIXELS = 160000000;
        private const int GAME_TIME_SPEED = 10000;

        private string _name;
        private string _type;
        private Color _color;

        private double _mass;
        private double _radius;
        private double _gravity;
        private double _density;
        private double _escapeVelocity;
        private double _eccentricity;
        private double _semiMajorAxis;
        private double _semiMinorAxis;
        private double _periapsis;
        private double _apoapsis;
        private double _orbitalPeriod;
        private double _orbitalVelocity;

        private double _distanceFromSun;
        private Vector2 _position;
        private Vector2 _origin;

        private Texture2D _texture;

        Star _star;
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
        /// The semi-major axis of the planet's orbit in Astronomical Units.
        /// </param>
        /// <param name="star"></param>
        public Planet(string name, double mass, double radius, double eccentricity, double semiMajorAxis, Star star) {
            double pi = Math.PI;
            uc = new UnitConverter();

            _name = name;
            _mass = mass;
            _radius = radius;
            _eccentricity = eccentricity;
            _semiMajorAxis = semiMajorAxis;
            _star = star;

            _gravity = _mass * uc.TO_EARTH_MASS / Math.Pow(_radius * uc.TO_EARTH_RADIUS, 2);
            _density = _gravity / (_radius * uc.TO_EARTH_RADIUS) * uc.FROM_EARTH_DENSITY;
            _escapeVelocity = Math.Sqrt(_mass * uc.TO_EARTH_MASS / (_radius * uc.TO_EARTH_RADIUS)) * 11.2;

            _semiMinorAxis = _semiMajorAxis * Math.Sqrt(1 - Math.Pow(_eccentricity, 2));
            _periapsis = _semiMajorAxis * (1 - _eccentricity);
            _apoapsis = _semiMajorAxis * (1 + _eccentricity);
            _orbitalPeriod = Math.Sqrt(Math.Pow(_semiMajorAxis, 3) / (_star.Mass * uc.TO_SOLAR_MASS));
            _orbitalVelocity = Math.Sqrt(_star.Mass * uc.TO_SOLAR_MASS / _semiMajorAxis) * 29.78;

            _distanceFromSun = _apoapsis;
            _position = new Vector2((int)(960 + _distanceFromSun / METERS_PER_PIXELS), 540);
            _origin = Vector2.Zero;

            Console.WriteLine(
                "Name: {0}\n" +
                "Mass (Earth Mass): {1}-Class\n" +
                "Radius (Earth Radius): {2}\n" +
                "Diameter (km): {3}\n" +
                "Density (g/cm^3): {4}\n" +
                "Gravity (G): {5}\n" +
                "Escape Velocity (km/s): {6}\n" +
                "Eccentricity: {7}\n" +
                "Semi-Major Axis (AU): {8}\n" +
                "Semi-Minor Axis (AU): {9}\n" +
                "Periapsis (AU): {10}\n" +
                "Apoapsis (AU): {11}\n" +
                "Orbital Period (Years): {12}\n" +
                "Orbital velocity: {13}\n",
                _name, _mass * uc.TO_EARTH_MASS, _radius * uc.TO_EARTH_RADIUS, _radius * 2 * uc.TO_KILOMETER, _density * uc.TO_GRAM_PER_CUBIC_CENTIMETER,
                _gravity, _escapeVelocity, _eccentricity, _semiMajorAxis * uc.TO_AU, _semiMinorAxis * uc.TO_AU, _periapsis * uc.TO_AU, _apoapsis * uc.TO_AU,
                _orbitalPeriod, _orbitalVelocity);

            Console.WriteLine("CURRENT SPEED = " + CurrentSpeed());
        }

        public void Update()
        {
            Vector2 directionTowardsSun = _position - _star.Position;
            float rotation = (float)Math.Atan2(directionTowardsSun.Y, directionTowardsSun.X) + MathHelper.ToRadians(90);
            Vector2 direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            direction.Normalize();

            _position -= direction * (float)(CurrentSpeed() / METERS_PER_PIXELS) * GAME_TIME_SPEED;
            _distanceFromSun = Vector2.Distance(_position, _star.Position) * METERS_PER_PIXELS;
            Console.WriteLine("CURRENT SPEED = " + CurrentSpeed());
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, _position, null, Color.Blue, 0, _origin, 1, SpriteEffects.None, 0.5f);
            
        }

        private double CurrentSpeed()
        {
            double standard_gravitational_parameter = GRAVITATIONAL_CONSTANT * _star.Mass;
            //return Math.Sqrt(standard_gravitational_parameter / _semiMajorAxis);
            return Math.Sqrt(standard_gravitational_parameter * (2 / _distanceFromSun  - 1 / _semiMajorAxis));
        }

        public Texture2D Texture
        {
            set { _texture = value;
            _origin = new Vector2(_texture.Width / 2, _texture.Height / 2); }
        }

    }
}
